import os
from dotenv import load_dotenv
load_dotenv()

SQLSERVER_CONN_STR = os.getenv("SQLSERVER_CONN_STR")
POSTGRES_CONN_STR = os.getenv("POSTGRES_CONN_STR")
MODEL_CHAT = os.getenv("MODEL_CHAT", "mistral:instruct")
MODEL_EMBEDDING = os.getenv("MODEL_CHAT", "nomic-embed-text")
APP_URL = os.getenv("APP_URL", "https://localhost:7264")