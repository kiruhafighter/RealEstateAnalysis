import requests
from config import get_env

OLLAMA_API = get_env("OLLAMA_API")
MODEL = get_env("MODEL")

def get_embedding(text: str):
    try:
        response = requests.post(
            f"{OLLAMA_API}/api/embeddings",
            json={
                "model": MODEL,
                "prompt": text
            },
            timeout=10000
        )
        response.raise_for_status()
        return response.json()["embedding"]
    except Exception as e:
        print(f"[Embedding Error] Failed to get embedding: {e}")
        raise