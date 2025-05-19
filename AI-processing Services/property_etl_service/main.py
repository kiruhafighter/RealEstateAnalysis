import schedule
import time
from migrate import run_pipeline

def start_scheduler():
    run_pipeline()

    schedule.every().day.at("00:00").do(run_pipeline)

    while True:
        schedule.run_pending()
        time.sleep(60)

if __name__ == "__main__":
    start_scheduler()