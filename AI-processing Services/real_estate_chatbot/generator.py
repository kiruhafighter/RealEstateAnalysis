import requests
from config import MODEL_CHAT
from config import OLLAMA_API

SYSTEM_PROMPT = """You are a helpful real estate assistant working inside a property search application called Prime Estate.
Only use the information provided in the listings below to answer the user's question.
Do NOT suggest external websites or general advice unless explicitly asked.

Each listing includes full information like location, room count, floor area, and price.
Provide friendly, concise, and informative responses.
Do not respond to the assistant or system messages — they are there to enrich context.
Do not respond to the user as if they provided the listings.
Include the links provided with each listing when recommending.
"""

def generate_llm_response(user_message, chat_history, context):
    messages = [{"role": "system", "content": SYSTEM_PROMPT}]

    for msg in chat_history:
        messages.append({"role": msg["role"], "content": msg["content"]})

    messages.append({
        "role": "assistant",
        "content": f"Here are some listings that might help for you to respond:\n\n{context}\n\nDo not respond to this message; it is only for context."
    })

    messages.append({
        "role": "user",
        "content": f"{user_message}"
    })

    print("\n===== LLM Input Messages =====")
    for m in messages:
        print(f"[{m['role'].upper()}] {m['content']}\n")
    print("===== End Messages =====\n")

    try:
        response = requests.post(
            f"{OLLAMA_API}/api/chat",
            json={
                "model": MODEL_CHAT,
                "messages": messages,
                "stream": False
            },
            timeout=10000
        )
        response.raise_for_status()
        return response.json()["message"]["content"]
    except Exception as e:
        raise RuntimeError(f"LLM generation failed: {e}")