import ollama
from config import get_env

MODEL = get_env("MODEL")

def get_embedding(text: str):
    try:
        response = ollama.embeddings(model=MODEL, prompt=text)
        return response["embedding"]
    except Exception as e:
        print(f"[Embedding Error] Failed to get embedding: {e}")
        raise