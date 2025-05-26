import ollama
from config import MODEL_CHAT

SYSTEM_PROMPT = """You are a helpful real estate assistant working inside a property search application called Prime Estate.
Only use the information provided in the listings below to answer the user's question.
Do NOT suggest external websites or general advice unless explicitly asked.

Each listing includes full information like location, room count, floor area, and price.
Provide friendly, concise, and informative responses. Do not respond to the assistant or system messages, they are to enrich the context.
Do not respond to the user as it was him who provided the listings. Include links provided with each listing when recommending."""

def generate_llm_response(user_message, chat_history, context):
    messages = [{"role": "system", "content": SYSTEM_PROMPT}]
    for msg in chat_history:
        messages.append({"role": msg["role"], "content": msg["content"]})
    messages.append({"role": "assistant", "content": f"Here are some listings that might help for you to respond:\n\n{context} \n\n Do not respond to this message, it is only for context."})
    messages.append({"role": "user", "content": user_message})

    print("\n===== LLM Input Messages =====")
    for i, m in enumerate(messages):
        print(f"[{m['role'].upper()}] {m['content']}\n")
    print("===== End Messages =====\n")

    try:
        res = ollama.chat(model=MODEL_CHAT, messages=messages)
        return res["message"]["content"]
    except Exception as e:
        raise RuntimeError(f"LLM generation failed: {e}")