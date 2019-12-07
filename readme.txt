10.17.2019:
----------
* HelloMongoDB:
https://docs.mongodb.com/manual/tutorial/
https://docs.mongodb.com/manual/tutorial/install-mongodb-on-ubuntu/
https://www.tutorialspoint.com/mongodb/

* MONGODB INSTALL (Ubuntu 18.04 LTS Bionic VM):
1. Delete any obsolete/conflicting versions of MongoDB:
   - apt-get remove mogodb mongodb-org
     dpkg -l|grep -i mongo 
       <= LISTS ALL CURRENTLY INSTALLED "MONGODB" PACKAGES
     apt-get purge mongodb-org mongodb-org-mongos mongodb-org-server  mongodb-org-shell  mongodb-org-tools
     apt-get autoremove
       <= DELETE EVERYTHING LISTED
     cd /var/log; rm -rf mongodb
     cd /var/lib; rm -rf mongodb
       <= MANUALLY DELETE ANY REMAINING ARTIFACTS

2. Install/Update MongoDB from .deb
   - wget -qO - https://www.mongodb.org/static/pgp/server-4.2.asc | sudo apt-key add -
     <= Get certificate for this repo

   - vi /etc/apt/sources.list.d/mongodb.list 
deb https://repo.mongodb.org/apt/ubuntu bionic/mongodb-org/4.2 multiverse
     apt-get update => OK
     <= Add repo for "apt-get"

   - apt-get install mongodb-org => OK
     apt list --installed|grep -i mongo
mongodb-org/bionic,now 4.2.0 amd64 [installed]
mongodb-org-mongos/bionic,now 4.2.0 amd64 [installed,automatic]
mongodb-org-server/bionic,now 4.2.0 amd64 [installed,automatic]
mongodb-org-shell/bionic,now 4.2.0 amd64 [installed,automatic]
mongodb-org-tools/bionic,now 4.2.0 amd64 [installed,automatic]
     <= install package and verify installation

3. Start/verify service:
   - service mongod start => OK
     ls -l /var/log/mongodb/mongod.log
-rw------- 1 mongodb mongodb 6832 Oct 17 13:27 mongod.log => OK
     less /var/log/mongodb/mongod.log
2019-10-17T13:27:40.298-0700 I  CONTROL  [main] Automatically disabling TLS 1.0, to force-enable TLS 1.0 specify --sslDisabledProtocols 'none'
2019-10-17T13:27:40.411-0700 I  CONTROL  [initandlisten] MongoDB starting : pid=4012 port=27017 dbpath=/var/lib/mongodb 64-bit host=ubuntu18
2019-10-17T13:27:40.411-0700 I  CONTROL  [initandlisten] db version v4.2.0
...
2019-10-17T13:27:40.411-0700 I  CONTROL  [initandlisten] options: { config: "/etc/mongod.conf", net: { bindIp: "127.0.0.1", port: 27017 }, processManagement: { timeZoneInfo: "/usr/share/zoneinfo" }, storage: { dbPath: "/var/lib/mongodb", journal: { enabled: true } }, systemLog: { destination: "file", logAppend: true, path: "/var/log/mongodb/mongod.log" } }
       <= OK: UP/RUNNING ON PORT 27017, LOCALHOST ONLY

   - mongo
> db._adminCommand( {getCmdLineOpts: 1})
{
        "argv" : [ "/usr/bin/mongod", "--config", "/etc/mongod.conf" ],
        "parsed" : {
                "config" : "/etc/mongod.conf",
                "net" : { "bindIp" : "127.0.0.1", "port" : 27017 },
                "processManagement" : { "timeZoneInfo" : "/usr/share/zoneinfo" },
                "storage" : { "dbPath" : "/var/lib/mongodb", "journal" : { "enabled" : true } },
                "systemLog" : { "destination" : "file", "logAppend" : true, "path" : "/var/log/mongodb/mongod.log"
                }
        },
        "ok" : 1
}
     
> db.test.save( { a: 1 } )
WriteResult({ "nInserted" : 1 })

> db.test.find()
{ "_id" : ObjectId("5da8ebb6b198c5cf6ffa6122"), "a" : 1 }
  <= DB Completely functional

4. INITIAL PROBLEMS:
   - service mongod start
     less /var/log/mongodb/mongod.log:
2019-10-17T10:40:20.795-0700 F  CONTROL  [initandlisten] ** IMPORTANT: UPGRADE PROBLEM: Found an invalid featureCompatibilityVersion document 
  (ERROR: BadValue:Invalid value for version, found 3.6, expected '4.2' or '4.0'. 
  Contents of featureCompatibilityVersion document in admin.system.version: 
    { _id: "featureCompatibilityVersion", version: "3.6" }. 
  See http://dochub.mongodb.org/core/4.0-feature-compatibility.).
  If the current featureCompatibilityVersion is below 4.0, see the documentation
    on upgrading at http://dochub.mongodb.org/core/4.0-upgrade-fcv.
  <= SOLUTION: Completed removed/reinstalled MongoDB 4.2 from scratch (per above)

===================================================================================================

* Tutorial:
https://www.tutorialspoint.com/mongodb/

   - Terminology: RDBMS vs. MongoDB:
     RDBMS       MongoDB
     -----       -------
     Database    Database
     Table       Collection
     Tuple/Row   Document
     column      Field
     Table Join  Embedded Documents
     Primary Key Primary Key 
                 <= Default key _id provided by mongodb itself)

   - Good use cases for MongoDB:
     - Big Data
     - Content Management and Delivery
     - Mobile and Social Infrastructure
     - User Data Management
     - Data Hub

   - Basic commands:
     - mongodb
         db.help()  // List commands
         db.stats() // Show statistics for current database
         show dbs   // Show all databases for this server
         use test   // Switch to database "test"
         show collections  // Show collections in current database
         db.test.find()  // Query all records in current DB, collection "test"

   - Considerations for designing a schema:
     - Design your schema according to user requirements.
     - Combine objects into one document if you will use them together. Otherwise separate them (but make sure there should not be need of joins).
     - Duplicate the data (but limited) because disk space is cheap as compare to compute time.
     - Do joins while write, not on read.
     - Optimize your schema for most frequent use cases.
     - Do complex aggregation in the schema.

     EXAMPLE:
     - RDBMS: 
         post: has a unique post_id and text.  Can have zero or more commands; must have one or more tags
           tag_list
           comments
     - MongoDB:
      {
         _id: POST_ID
         title: TITLE_OF_POST, 
         description: POST_DESCRIPTION,
         by: POST_BY,
         url: URL_OF_POST,
         tags: [TAG1, TAG2, TAG3],
         likes: TOTAL_LIKES, 
         comments: [ 
            { user:'COMMENT_BY', message: TEXT, dateCreated: DATE_TIME, like: LIKES },
            { user:'COMMENT_BY', message: TEXT, dateCreated: DATE_TIME, like: LIKES }
         ]
      }

   - Create DB:
       use mydb => switched to db mydb
       db => mydb
       show dbs => "mydb" will *NOT* appear until we write at least one document to it
       db.movie.insert({"name":"tutorials point"}) => WriteResult({ "nInserted" : 1 })
       show dbs => Now see "mydb" in list:
admin   0.000GB
config  0.000GB
local   0.000GB
mydb    0.000GB
test    0.000GB

   - Drop DB:
       use mydb
       db.dropDatabase()

   - The createCollection() Method:

   - The insert() Method:
     EXAMPLE:
db.mycol.insert({
   _id: ObjectId(7df78ad8902c),
   title: 'MongoDB Overview', 
   description: 'MongoDB is no sql database',
   by: 'tutorials point',
   url: 'http://www.tutorialspoint.com',
   tags: ['mongodb', 'database', 'NoSQL'],
   likes: 100
})
  <= MongoDB will create the collection (if new) and then insert this document into it.
     If we didn't specify an ID, MongoDB would create one.

    - The  find() Method:
        db => mydb
        db.movie.find() => { "_id" : ObjectId("5da8f85eb198c5cf6ffa6123"), "name" : "tutorials point" }
        db.movie.find().pretty => "prettifies" the JSON (here, no difference)
        db.movie.find({$and: [{name: "tutorials point" }]}) => { "_id" : ObjectId("5da8f85eb198c5cf6ffa6123"), "name" : "tutorials point" }
        <= Can use "and", "or", $gt, $lt, $lte, $gte, $ne

    - MongoDB Update(), Save() Methods:
      <= Update the values in the existing document(s);
         Save() replaces the existing document with the new document passed in the save() method. 

    - ensureIndex() Method
        db.mycol.ensureIndex({"title":1})
        <= Creates an index on "title" field
        db.mycol.ensureIndex({"title":1,"description":-1})
        <= Creates a composite index in "title" and "description" fields

    - What is a Covered Query?
      As per the official MongoDB documentation, a covered query is a query in which:
      - All the fields in the query are part of an index.
      - All the fields returned in the query are in the same index.

===================================================================================================
* MongoDB from C#
https://www.c-sharpcorner.com/UploadFile/pmfawas/mongodb-with-C-Sharp/
https://docs.mongodb.com/ecosystem/drivers/csharp/
http://mongodb.github.io/mongo-csharp-driver/2.8/getting_started/quick_tour/

1. Enable remote connections:
    - sudo vi /etc/mongod.conf
# network interfaces
net:
  port: 27017
  bindIp: 0.0.0.0  <-- Change this to 0.0.0.0
...
#security:
  <-- Check if "authorization: enabled" (here, it's blank...)

    - service mongod stop;service mongod start
      <= Restart Mongod 

    - ufw status
Status: inactive
      <= Firewall not active: all ports should be open on this VM...

2. Create project:
    - MSVS > Create Project >
     Folder= D:\paul\proj\HelloMongoDB, Name= HelloMongoDBCS, [OK]

    - Tools > Nuget > Browse >
        Search= MongoDB > 
        Select MongoDB.Driver
        <= This will install MongoDB.{Bson, .Driver, .Driver.Core, etc.}
           It will *NOT* install MongoDb.Driver.GridFS (optional, if objects over 64MB need to be stored)
        "Accept" Nuget license

    - PROBLEM:
An error occurred while downloading package 'sharpcompress.0.23.0 : ' from source 'https://www.nuget.org/api/v2/curated-feeds/microsoftdotnet/'.			0	
      <= This led to a series of problems; unable to install either v2.9.2 or v2.10.0-beta1 directly from the NuGet GUI
      WORKAROUND:
      1. Update NuGet:
           MSVS > Tools > Extensions and Updates > Updates >
           <= If there's an update for NuGet shown, install it
      2. Create a local repo:
           MSVS > Tools > Options > Package Manager Settings > Package Sources > (+) Add >
             Name= Local, Source= d:\temp
      3. Manually download and copy *.nuget files to "Local" repo
           https://www.nuget.org/packages/MongoDB.Driver/2.9.2
           https://www.nuget.org/packages/MongoDB.Driver.Core/2.9.2
           https://www.nuget.org/packages/MongoDB.Bson/2.9.2
           https://www.nuget.org/packages/sharpcompress/0.23.0
      4. Restart MSVS, try installing MongoDB.Driver v2.9.2 from NuGet
           <= Should work at this point..

v2.9.2:                      2.10.0-beta1:           
------                       ------------
Crc32C.NET.1.0.5             Crc32C.NET" version="1.0.5.0"
DnsClient.1.2.0              DnsClient" version="1.2.0"
MongoDB.Bson.2.9.2           MongoDB.Bson" version="2.10.0-beta1
MongoDB.Driver.2.9.2         MongoDB.Driver" version="2.10.0-beta1
MongoDB.Driver.Core.2.9.2    MongoDB.Driver.Core" version="2.10.0-beta1"
*                            MongoDB.Libmongocrypt" version="1.0.0-beta01"
sharecompress.0.23.0         SharpCompress" version="0.23.0"       
Snappy.NET.1.1.1.8           "Snappy.NET" version="1.1.1.8"
System.Buffers.4.4.0         "System.Buffers" version="4.4.0"

3. C# Code (Form1.cs):

     // Members
     public const string DB_NAME = "HelloMongoDB";
     public const string COLLECTION_NAME = "HelloMongoCollection";
     private MongoClient mongoClient
     private IMongoDatabase db
     
     // Connect to MongoDB
     string connectionString = "mongodb://" + edtHost.Text + ":" + edtPort.Text;
     mongoClient = new MongoClient(connectionString);
     db = mongoClient.GetDatabase(DB_NAME);
     
     // Add documents
     var documents = Enumerable.Range(0, 5).Select(i => new BsonDocument("counter", i));
     MongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(COLLECTION_NAME);
     collection.InsertMany(documents);
     
     // List documents
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

===================================================================================================
* Wortschatz:
  - Sharding: a method for distributing data across multiple machines.
              MongoDB uses sharding to support deployments with very large data sets and high throughput operations.
  - Chunk: MongoDB partitions sharded data into chunks. 
           Each chunk has an inclusive lower and exclusive upper range based on the shard key.
  - Collection: a grouping of MongoDB documents; equivalent of an RDBMS table
                MongoDB automatically creates a collection the first time a document is added into a new DB
  - Database: a MongoDB database is a physical container for collections.
              A single MongoDB server typically has multiple databases.
  - Document: a record in a MongoDB collection and the basic unit of data in MongoDB. 
              Documents are analogous to JSON objects but exist in the database in a more type-rich format known as BSON.
  - Field: a name-value pair in a document; analogous to columns in relational databases. 
           A document has zero or more fields.
  - Field Path: path to a field in the document. 
                To specify a field path, use a string that prefixes the field name with a dollar sign ($).
  - GridFS: a convention for storing large files in a MongoDB database.
            Maximum BSON object size is 64MB.
  - Map-Reduce: a data processing and aggregation paradigm consisting of a “map” phase that selects data and a “reduce” phase that transforms the data. 
                In MongoDB, you can run arbitrary aggregations over data using map-reduce.
  - mongo: the MongoDB shell.
  - mongod: the MongoDB database server.
  - mongos: the routing and load balancing process that acts an interface between an application and a MongoDB sharded cluster.
  - operator: a keyword beginning with a $ used to express an update, complex query, or data transformation. 
              For example, $gt is the query language’s “greater than” operator
  - oplog: a capped collection that stores an ordered history of logical writes to a MongoDB database. 
           The oplog is the basic mechanism enabling replication in MongoDB.
  - primary key: A record’s unique immutable identifier.
  - query: a read request. 
           MongoDB uses a JSON-like query language that includes a variety of query operators with names that begin with a $ character. 
           In the mongo shell, you can issue queries using the db.collection.find() and db.collection.findOne() methods.
  - projection: a document given to a query that specifies which fields MongoDB returns in the result set.
  - query shape: a combination of query predicate, sort, and projection.
  - replication: a feature allowing multiple database servers to share the same data, thereby ensuring redundancy and facilitating load balancing.
  - resource: a database, collection, set of collections, or cluster. A privilege permits actions on a specified resource.
  - shard: a single mongod instance or replica set that stores some portion of a sharded cluster’s total data set. 
           In production, all shards should be replica sets.
  - split: the division between chunks in a sharded cluster.
  - tag: a label applied to a replica set member and used by clients to issue data-center-aware operations (MongoDB 3.2 and earlier).
  - zone: a grouping of documents based on ranges of shard key values for a given sharded collection (New in version 3.4).
          Each shard in the sharded cluster can associate with one or more zones.
          Zones supersede functionality described by tags in MongoDB 3.2.
