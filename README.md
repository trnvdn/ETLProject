# ETLProject

This is a simple project to parse CSV data of a cab trip file and transfer the data into a database. Configured indexes and the bulk insert operation allow you to process large volumes of records.
The project provides the opportunity to:
- Download a CSV table,
- Save that table,
- Parse table to the list of models,
- Find duplicates and add them to a separate CSV table.

Then you can enter the received data into the database and receive records and statistics

P.S: After removing duplicates i got 29889 rows of data

To start you need:
1) Create a database in MSSQLServer and get a connection string to it.
2) In the config.js file, change the value of the "DbConnectionString" property to your connection string.
3) Execute migration of models into database. Just run "UpdateDatabase" in your PackageManagerConsole (You need to select ETL_Lib as a default project).

If you need to use much larger volumes of writes, you need to rewrite all operations from an asynchronous to a parallel programming approach, since it can best utilize modern multi-core processors by distributing the load across threads.

The database is implemented using entity migration, but if you need a query to create a table, here it is:
```
CREATE TABLE CabTrips (
    TripID UNIQUEIDENTIFIER NOT NULL,
    PickupDateTime DATETIME2 NOT NULL,
    DropoffDateTime DATETIME2 NOT NULL,
    PassengerCount INT NOT NULL,
    TripDistance FLOAT NOT NULL,
    StoreAndFwdFlag NVARCHAR(MAX) NOT NULL,
    PULocationID INT NOT NULL,
    DOLocationID INT NOT NULL,
    FareAmount FLOAT NOT NULL,
    TipAmount FLOAT NOT NULL,
    CONSTRAINT PK_CabTrips PRIMARY KEY CLUSTERED (TripID)
);
```

Query for indexes:
```
CREATE INDEX idx_PULocationID ON CabTrips (PULocationID);
CREATE INDEX idx_DropoffDateTime ON CabTrips (DropoffDateTime);
CREATE INDEX idx_TripDistance ON CabTrips (TripDistance);
```



