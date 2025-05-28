DO
$$
BEGIN
   IF NOT EXISTS (
     SELECT FROM pg_database WHERE datname = 'ai_property_db'
   ) THEN
     CREATE DATABASE ai_property_db;
   END IF;
END
$$;

\c ai_property_db

CREATE EXTENSION IF NOT EXISTS vector;

CREATE TABLE IF NOT EXISTS property_embeddings (
    property_id UUID PRIMARY KEY,
    description TEXT NOT NULL,
    embedding vector(768),
    last_synced_at TIMESTAMP DEFAULT now()