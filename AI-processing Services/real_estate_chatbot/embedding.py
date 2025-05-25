import ollama
import numpy as np
from config import MODEL_EMBEDDING

def get_query_embedding(query: str):
    try:
        res = ollama.embeddings(model=MODEL_EMBEDDING, prompt=query)
        return np.array(res["embedding"], dtype=np.float32)
    except Exception as e:
        raise RuntimeError(f"Embedding failed: {e}")