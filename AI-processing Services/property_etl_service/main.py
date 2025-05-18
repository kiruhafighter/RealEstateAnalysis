import schedule
import time
from migrate import run_pipeline

def start_scheduler():
    schedule.every(30).minutes.do(run_pipeline)

    while True:
        schedule.run_pending()
        time.sleep(60)

if __name__ == "__main__":
    run_pipeline()