import requests
import numpy as np
from config import MODEL_EMBEDDING
from config import OLLAMA_API

def get_query_embedding(query: str):
    try:
        response = requests.post(
            f"{OLLAMA_API}/api/embeddings",
            json={"model": MODEL_EMBEDDING, "prompt": query},
            timeout=10000
        )
        response.raise_for_status()
        embedding = response.json()["embedding"]
        return np.array(embedding, dtype=np.float32)
    except Exception as e:
        raise RuntimeError(f"Embedding failed: {e}")