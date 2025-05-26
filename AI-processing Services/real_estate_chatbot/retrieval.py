import psycopg2
from config import POSTGRES_CONN_STR
from embedding import get_query_embedding

def extract_user_query_context(message: str, history: list[dict], max_length: int = 1500) -> str:
    """Combines current user message with prior user messages, truncated to max_length characters."""
    combined = ""
    for entry in history:
        if entry["role"] == "user":
            combined += entry["content"].strip() + "\n"
    combined += message.strip()
    return combined[-max_length:]

def search_similar_properties(query, history=[], top_k=5):
    contextual_query = extract_user_query_context(query, history)
    query_emb = get_query_embedding(contextual_query).tolist()

    conn = psycopg2.connect(POSTGRES_CONN_STR)
    cur = conn.cursor()

    cur.execute("""
        SELECT property_id, description
        FROM property_embeddings
        ORDER BY embedding <#> %s::vector
        LIMIT %s
    """, (query_emb, top_k))

    results = cur.fetchall()
    cur.close()
    conn.close()

    return results