import pyodbc
import random
import os
from dotenv import load_dotenv
from datetime import datetime, timedelta
from decimal import Decimal

MAX_PROPERTIES = 100
MIN_DAYS = 90
MAX_DAYS = 365
MIN_PERCENT = 0.02
MAX_PERCENT = 0.22

load_dotenv()
conn = pyodbc.connect(os.getenv('SQLSERVER_CONN_STR'))
conn.autocommit = False  # Begin transaction manually
cursor = conn.cursor()

try:
    # Disable system versioning safely
    cursor.execute("ALTER TABLE dbo.Properties SET (SYSTEM_VERSIONING = OFF)")

    cursor.execute(f"""
        SELECT TOP {MAX_PROPERTIES} 
            p.PropertyId, p.Price, p.YearBuilt, p.ValidFrom
        FROM dbo.Properties p
        ORDER BY NEWID()
    """)
    properties = cursor.fetchall()

    for prop_id, current_price, year_built, current_valid_from in properties:
        earliest_valid_from = datetime(year_built, 1, 1)

        cursor.execute("""
            SELECT TOP 1 ValidFrom
            FROM dbo.PropertiesHistory
            WHERE PropertyId = ?
            ORDER BY ValidFrom ASC
        """, prop_id)
        oldest_history_row = cursor.fetchone()

        insertion_valid_to = oldest_history_row.ValidFrom if oldest_history_row else current_valid_from

        available_days = (insertion_valid_to - earliest_valid_from).days
        if available_days < MIN_DAYS:
            print(f"⚠️ Skipping PropertyId {prop_id}: insufficient room to add new history row.")
            continue

        duration_days = random.randint(MIN_DAYS, min(MAX_DAYS, available_days))
        fake_valid_from = insertion_valid_to - timedelta(days=duration_days)

        if fake_valid_from < earliest_valid_from:
            fake_valid_from = earliest_valid_from
            duration_days = (insertion_valid_to - fake_valid_from).days
            if duration_days < MIN_DAYS:
                print(f"⚠️ Skipping PropertyId {prop_id}: adjusted range too small.")
                continue

        change_sign = random.choice([-1, 1])
        change_percent = Decimal(str(random.uniform(MIN_PERCENT, MAX_PERCENT))) * change_sign
        old_price = round(current_price * (Decimal('1') + change_percent), 2)

        cursor.execute("""
            INSERT INTO dbo.PropertiesHistory (
                PropertyId, Name, Address, Description, County, Country,
                Locality, Postcode, PropertyTypeId, NumberOfRooms, NumberOfFloors,
                YearBuilt, PlotArea, FloorArea, Price, PropertyStatusId,
                AgentId, ValidFrom, ValidTo
            )
            SELECT PropertyId, Name, Address, Description, County, Country,
                   Locality, Postcode, PropertyTypeId, NumberOfRooms, NumberOfFloors,
                   YearBuilt, PlotArea, FloorArea, ?, PropertyStatusId,
                   AgentId, ?, ?
            FROM dbo.Properties
            WHERE PropertyId = ?
        """, old_price, fake_valid_from, insertion_valid_to, prop_id)

        print(f"✅ Inserted for PropertyId {prop_id}: {fake_valid_from.date()} to {insertion_valid_to.date()}, Price: {old_price}")

    # Re-enable system versioning
    cursor.execute("""
        ALTER TABLE dbo.Properties
        SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.PropertiesHistory))
    """)

    conn.commit()
    print("✅ Transaction committed successfully.")

except Exception as e:
    conn.rollback()
    print(f"❌ Transaction rolled back due to error: {e}")

finally:
    cursor.close()
    conn.close()