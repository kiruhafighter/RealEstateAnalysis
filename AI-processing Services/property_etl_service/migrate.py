from sqlserver_client import get_active_properties
from embedding import get_embedding
from postgres_client import upsert_embedding, delete_outdated_properties

def format_description(row):
    return f"""
{row['Name']} — {row['Description']}.
Located at {row['Address']}, {row['Locality']}, {row['County']}, {row['Country']}.
Features: {row['NumberOfRooms']} rooms, {row['NumberOfFloors']} floors, built in {row['YearBuilt']}.
Plot area: {row['PlotArea']} m². Floor area: {row['FloorArea']} m².
Price: {row['Price']} US dollars. Type: {row['PropertyType']}.
"""

def run_pipeline():
    rows = get_active_properties()
    valid_ids = []

    for row in rows:
        formatted = format_description(row)
        emb = get_embedding(formatted)
        rec = {
            "property_id": row["PropertyId"],
            "description": formatted,
            "embedding": emb
        }
        upsert_embedding(rec)
        valid_ids.append(row["PropertyId"])

    delete_outdated_properties(valid_ids)