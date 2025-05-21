import schedule
import time
from migrate import run_pipeline

def start_scheduler():
    print("Starting the scheduler...")
    run_pipeline()
    print("Scheduler started, with the initial execution finished. Running the pipeline every day at midnight.")

    schedule.every().day.at("00:00").do(run_pipeline)

    while True:
        schedule.run_pending()
        time.sleep(60)

if __name__ == "__main__":
    start_scheduler()