import ollama
import pyodbc
import uuid
import random
import os
from dotenv import load_dotenv
from datetime import datetime

load_dotenv()
conn = pyodbc.connect(os.getenv('SQLSERVER_CONN_STR'))
cursor = conn.cursor()

agent_ids = [
    'C27C3687-B53A-49CF-9E44-0027FEF34033', 'F8EA0BCA-0D4C-4A3A-A2BD-0AE1AA457710',
    '63F908F5-0AE9-4F76-B48D-1962FBF991F5', 'D1961AC8-733A-42CA-9256-22C9F0BA4AC6',
    'C58E32DE-B254-4128-8204-28969740D5FE', '1B67CA12-48D9-43A9-9233-390F1CB33177',
    '20AA8EF5-CE73-48BB-846D-6E18023295D5', 'E93E46EE-E7E4-4BCD-913C-6F1B0F1B7B2D',
    '1B07A003-E19F-4AD5-99ED-87A1638DA250', '98CDCEFF-000B-41A5-9572-AB4B96F88795',
    '78B73A5E-BEBD-4D72-8F6D-B7D484410A31', '4D49020D-DC74-4C18-A9CB-D70DAD572175',
    '1B1D94E4-64A7-42AE-8109-F591E458E7E2', '58815217-69AB-44C1-ADAC-F825A6538680'
]

prompt = (
    "Generate a fictional real estate property listing in the United States with the following fields in JSON format:\n"
    "Name, Description, County, Country, Locality, Postcode.\n"
    "The listing should be realistic and detailed, as if written for a real estate website. Only return JSON. Do not explain anything.\n"
    "Example:\n"
    "{\n"
    "  \"Name\": \"Maplewood Family Retreat\",\n"
    "  \"Description\": \"This spacious 4-bedroom home features a large open-plan kitchen, hardwood floors, a finished basement, and a fenced backyard perfect for families and pets. Located in a quiet residential area close to schools, parks, and local shops.\",\n"
    "  \"County\": \"Westchester\",\n"
    "  \"Country\": \"United States\",\n"
    "  \"Locality\": \"New York\",\n"
    "  \"Postcode\": \"10583\"\n"
    "}\n"
    "The JSON must be valid and include all fields. Description must not exceed 500 characters. Do not include any comments, markdown, or explanation.\n"
)

for _ in range(50):
    response = ollama.chat(model='mistral:instruct', messages=[{'role': 'user', 'content': prompt}])
    content = response['message']['content']

    try:
        data = eval(content)
    except Exception:
        continue

    property_id = str(uuid.uuid4())
    type_id = random.randint(1, 8)
    rooms = random.randint(1, 10)
    floors = random.randint(1, 4)
    floor_area = random.randint(50, 400)
    plot_area = floor_area + random.randint(50, 200)
    year_built = random.randint(1950, 2025)
    price = round(floor_area * random.uniform(500, 2000), 2)
    status_id = 1
    agent_id = random.choice(agent_ids)

    cursor.execute("""
        INSERT INTO dbo.Properties (
            PropertyId, Name, Address, Description, County, Country, Locality, Postcode,
            PropertyTypeId, NumberOfRooms, NumberOfFloors, YearBuilt,
            PlotArea, FloorArea, Price, PropertyStatusId, AgentId
        ) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
    """, (
        property_id,
        data.get("Name", f"Property #{_+1}"),
        f"Street {random.randint(1,100)}, Building {random.randint(1,10)}",
        data.get("Description", "A sample property")[:500],
        data.get("County", "Unknown"),
        data.get("Country", "Unknown"),
        data.get("Locality", "Unknown"),
        data.get("Postcode", "00000"),
        type_id,
        rooms,
        floors,
        year_built,
        plot_area,
        floor_area,
        price,
        status_id,
        agent_id
    ))

conn.commit()
cursor.close()
conn.close()