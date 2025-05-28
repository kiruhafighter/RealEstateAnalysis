import pyodbc
import os
import random
from dotenv import load_dotenv

load_dotenv()
DB_CONN_STRING = os.getenv('SQLSERVER_CONN_STR')
IMAGES_DIR = os.getenv("IMAGES_DIR", "/images")
ALLOWED_EXTENSIONS = {".jpg", ".jpeg", ".png", ".webp"}
MAX_IMAGES_PER_PROPERTY = 3
EXCLUDED_IMAGES = {"icon.png", "logo.png"}

conn = pyodbc.connect(DB_CONN_STRING)
cursor = conn.cursor()

all_image_files = [
    f for f in os.listdir(IMAGES_DIR)
    if os.path.splitext(f)[1].lower() in ALLOWED_EXTENSIONS and f not in EXCLUDED_IMAGES
]

if not all_image_files:
    raise RuntimeError("No valid image files found in wwwroot")

cursor.execute("""
    SELECT p.PropertyId
    FROM Properties p
    WHERE NOT EXISTS (
        SELECT 1 FROM Images i WHERE i.PropertyId = p.PropertyId
    )
""")
properties_without_images = [row.PropertyId for row in cursor.fetchall()]

print(f"Found {len(properties_without_images)} properties without images.")

for property_id in properties_without_images:
    images_to_assign = random.sample(all_image_files, k=random.randint(1, MAX_IMAGES_PER_PROPERTY))
    for image_name in images_to_assign:
        image_path = f"/images/{image_name}"
        cursor.execute(
            "INSERT INTO Images (PropertyId, ImagePath) VALUES (?, ?)",
            property_id, image_path
        )
    print(f"Assigned {len(images_to_assign)} image(s) to PropertyId {property_id}")

conn.commit()
conn.close()
print("Image assignment complete.")