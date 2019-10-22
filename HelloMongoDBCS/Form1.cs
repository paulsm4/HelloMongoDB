using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

/**
 * REFERENCES:\
 *   https://docs.mongodb.com/manual/
 *   https://www.tutorialspoint.com/mongodb/
 *   https://www.c-sharpcorner.com/article/getting-started-with-mongodb-mongodb-with-c-sharp/
 *   https://docs.mongodb.com/ecosystem/drivers/csharp/
 */
namespace HelloMongoDBCS
{
    public partial class Form1 : Form
    {
        public const string DB_NAME = "HelloMongoDB";
        public const string COLLECTION_NAME = "HelloMongoCollection";

        private bool isConnected = false;
        private MongoClient mongoClient;
        private IMongoDatabase db;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isConnected)
                {
                    WriteStatus("Connecting to MongoDB on host " + edtHost.Text + ":" + edtPort.Text + "...");

                    // Connect to MongoDB
                    string connectionString = "mongodb://" + edtHost.Text + ":" + edtPort.Text;
                    mongoClient = new MongoClient(connectionString);
                    db = mongoClient.GetDatabase(DB_NAME);

                    // Update UI
                    isConnected = true;
                    btnListRecords.Enabled = true;
                    btnAddRecords.Enabled = true;
                    btnPurgeRecords.Enabled = true;
                    btnConnect.Text = "&Disconnect";
                }
                else
                {
                    WriteStatus("Disconnection from MongoDB...");

                    // There doesn't seem to be an explicit "Close" in the .Net API...
                    mongoClient = null;
                    isConnected = false;
                    btnListRecords.Enabled = false;
                    btnAddRecords.Enabled = false;
                    btnPurgeRecords.Enabled = false;
                    btnConnect.Text = "&Connect";
                }
            } catch (Exception ex)
            {
                WriteStatus("ERROR: " + ex);
            }
        }

        private void btnListRecords_Click(object sender, EventArgs e)
        {
            WriteStatus("Listing current records in  " + COLLECTION_NAME+ "...");
            try
            {

                IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(COLLECTION_NAME);
                var cursor = collection.Find(new BsonDocument()).ToCursor();
                int i = 0;
                foreach (var document in cursor.ToEnumerable())
                {
                    using (var stringWriter = new StringWriter())
                    using (var jsonWriter = new JsonWriter(stringWriter))
                    {
                        BsonSerializationContext context = BsonSerializationContext.CreateRoot(jsonWriter);
                        collection.DocumentSerializer.Serialize(context, document);
                        string json = stringWriter.ToString();
                        WriteStatus("doc[" + i++ + ": " + json + "...");
                    }

                }    
            }
            catch (Exception ex)
            {
                WriteStatus("ERROR: " + ex);
            }
        }

        private void btnAddRecords_Click(object sender, EventArgs e)
        {
            WriteStatus("Adding records to  " + COLLECTION_NAME + "...");
            try
            {

                var documents = Enumerable.Range(0, 5).Select(i => new BsonDocument("counter", i));
                IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(COLLECTION_NAME);
                collection.InsertMany(documents);
            }
            catch (Exception ex)
            {
                WriteStatus("ERROR: " + ex);
            }
        }

        private void btnPurgeRecords_Click(object sender, EventArgs e)
        {
            WriteStatus("Purging records from  " + COLLECTION_NAME + "...");
            try
            {

                IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(COLLECTION_NAME);
                collection.DeleteMany(FilterDefinition<BsonDocument>.Empty);
            }
            catch (Exception ex)
            {
                WriteStatus("ERROR: " + ex);
            }
        }

        public void WriteStatus(string msg)
        {
            string prefix = DateTime.Now.ToString("MM/dd/yy HH:mm:ss");
            edtStatus.AppendText(prefix + " " + msg + "\n");
        }

    }
}
