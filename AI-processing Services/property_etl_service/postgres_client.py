import psycopg2
from config import get_env

def upsert_embedding(record):
    conn = psycopg2.connect(get_env("POSTGRES_CONN_STR"))
    cur = conn.cursor()
    cur.execute("""
        INSERT INTO property_embeddings (property_id, description, embedding, last_synced_at)
        VALUES (%s, %s, %s, now())
        ON CONFLICT (property_id) DO UPDATE
        SET description = EXCLUDED.description,
            embedding = EXCLUDED.embedding,
            last_synced_at = now();
    """, (record["property_id"], record["description"], record["embedding"]))
    conn.commit()
    cur.close()
    conn.close()

def delete_outdated_properties(valid_ids):
    conn = psycopg2.connect(get_env("POSTGRES_CONN_STR"))
    cur = conn.cursor()
    cur.execute("DELETE FROM property_embeddings WHERE property_id NOT IN %s", (tuple(valid_ids),))
    conn.commit()
    cur.close()
    conn.close()