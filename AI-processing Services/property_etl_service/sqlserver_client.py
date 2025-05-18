import pyodbc
from config import get_env

def get_active_properties():
    conn = pyodbc.connect(get_env("SQLSERVER_CONN_STR"))
    cursor = conn.cursor()

    query = """
    SELECT
        p.PropertyId,
        p.Name,
        p.Address,
        p.Country,
        p.County,
        p.Locality,
        p.Description,
        p.NumberOfRooms,
        p.NumberOfFloors,
        p.YearBuilt,
        p.PlotArea,
        p.FloorArea,
        p.Price,
        pt.TypeName AS PropertyType,
        ps.StatusName AS PropertyStatus
    FROM dbo.Properties p
    JOIN dbo.PropertyTypes pt ON p.PropertyTypeId = pt.PropertyTypeId
    JOIN dbo.PropertyStatuses ps ON p.PropertyStatusId = ps.PropertyStatusId
    WHERE ps.PropertyStatusId = 1
    """
    cursor.execute(query)
    columns = [column[0] for column in cursor.description]
    return [dict(zip(columns, row)) for row in cursor.fetchall()]