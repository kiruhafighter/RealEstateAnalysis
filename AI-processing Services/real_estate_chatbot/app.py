from flask import Flask, request, jsonify
from retrieval import search_similar_properties
from generator import generate_llm_response
from sqlserver_client import get_property_details
from context_formatter import format_rich_context
from config import APP_URL

app = Flask(__name__)

@app.route("/chat", methods=["POST"])
def chat():
    data = request.json
    user_message = data.get("message")
    chat_history = data.get("history", [])

    if not user_message:
        return jsonify({"error": "Missing message"}), 400

    try:
        properties = search_similar_properties(user_message)
        property_ids = [pid for pid, _ in properties]
        details = get_property_details(property_ids)
        context = format_rich_context(details, APP_URL)
        response = generate_llm_response(user_message, chat_history, context)

        return jsonify({
            "response": response,
            "suggested_property_ids": [pid for pid, _ in properties]
        })
    except Exception as e:
        return jsonify({"error": str(e)}), 500

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=8000)