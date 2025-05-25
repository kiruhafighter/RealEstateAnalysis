import pyodbc
from config import SQLSERVER_CONN_STR

def get_property_details(property_ids):
    conn = pyodbc.connect(SQLSERVER_CONN_STR)
    cursor = conn.cursor()

    placeholders = ",".join("?" for _ in property_ids)
    query = f"""
    SELECT
        p.PropertyId,
        p.Name,
        p.Description,
        p.Address,
        p.Locality,
        p.County,
        p.Country,
        p.NumberOfRooms,
        p.NumberOfFloors,
        p.YearBuilt,
        p.PlotArea,
        p.FloorArea,
        p.Price,
        pt.TypeName AS PropertyType
    FROM dbo.Properties p
    JOIN dbo.PropertyTypes pt ON p.PropertyTypeId = pt.PropertyTypeId
    WHERE p.PropertyId IN ({placeholders})
    """

    cursor.execute(query, property_ids)
    columns = [column[0] for column in cursor.description]
    return [dict(zip(columns, row)) for row in cursor.fetchall()]