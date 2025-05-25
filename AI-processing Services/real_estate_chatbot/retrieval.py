import psycopg2
from config import POSTGRES_CONN_STR
from embedding import get_query_embedding

def search_similar_properties(query, top_k=5):
    query_emb = get_query_embedding(query).tolist()

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