#!/bin/bash
set -e

# Start PostgreSQL in background
/usr/local/bin/docker-entrypoint.sh postgres &

# Wait for DB to accept connections
echo "Waiting for Postgres to start..."
until pg_isready -U "$POSTGRES_USER" -d "$POSTGRES_DB"; do
  sleep 2
done

# Check if ai_property_db exists, and create if it doesn't
DB_EXISTS=$(psql -U "$POSTGRES_USER" -d "$POSTGRES_DB" -tAc "SELECT 1 FROM pg_database WHERE datname = 'ai_property_db'")
if [ "$DB_EXISTS" != "1" ]; then
  echo "Creating database ai_property_db..."
  createdb -U "$POSTGRES_USER" ai_property_db
fi

# Wait again until new DB is ready
until psql -U "$POSTGRES_USER" -d ai_property_db -c '\q'; do
  echo "Waiting for ai_property_db to become available..."
  sleep 2
done

# Run remaining schema logic
psql -U "$POSTGRES_USER" -d ai_property_db <<'EOSQL'
CREATE EXTENSION IF NOT EXISTS vector;

CREATE TABLE IF NOT EXISTS property_embeddings (
    property_id UUID PRIMARY KEY,
    description TEXT NOT NULL,
    embedding vector(768),
    last_synced_at TIMESTAMP DEFAULT now()
);
EOSQL

# Keep container running
wait