#!/bin/bash
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to start up
echo "Waiting for SQL Server to start..."
sleep 20

# Check if DB exists
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "SQL_Server_Password123!" -C -Q "IF DB_ID('RealEstateDB') IS NULL BEGIN RESTORE DATABASE RealEstateDB FROM DISK = N'/init/RealEstateDB.bak' WITH MOVE 'RealEstateDB' TO '/var/opt/mssql/data/RealEstateDB.mdf', MOVE 'RealEstateDB_log' TO '/var/opt/mssql/data/RealEstateDB_log.ldf', REPLACE END"

# Wait to keep container alive
wait