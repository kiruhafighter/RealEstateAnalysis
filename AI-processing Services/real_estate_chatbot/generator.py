import ollama
from config import MODEL_CHAT

SYSTEM_PROMPT = """You are a helpful real estate assistant. You help users find properties that best match their needs. 
When recommending properties, include the property name and a link to its detail page.
Be friendly, concise, and useful."""

def generate_llm_response(user_message, chat_history, context):
    messages = [{"role": "system", "content": SYSTEM_PROMPT}]
    for msg in chat_history:
        messages.append({"role": msg["role"], "content": msg["content"]})
    messages.append({"role": "user", "content": user_message})
    messages.append({"role": "system", "content": f"Here are some listings that might help:\n\n{context}"})

    try:
        res = ollama.chat(model=MODEL_CHAT, messages=messages)
        return res["message"]["content"]
    except Exception as e:
        raise RuntimeError(f"LLM generation failed: {e}")