from argparse import ArgumentParser
from datetime import datetime
from tabulate import tabulate
import json
import os
import sys

def main():
    database = load_database()
    
    parser = ArgumentParser(description="Task Tracker CLI")
    parser.add_argument("command", choices=["add"], help="Action to perform")
    parser.add_argument("--description", help="Description of the task (for 'add')", required=True)
    args = parser.parse_args()

    if args.command == "add":
        add_task(database, args.description)
        save_database(database)
        print("Task added successfully!")

def load_database():
    """Load database from JSON file."""
    try:
        with open('tasks.json', 'r') as file:
            database = json.load(file)
    except FileNotFoundError:
        database = {}
    return database

def save_database(database):
    """Save the database to a JSON file."""
    with open("tasks.json", 'w') as file:
        json.dump(database, file)

def add_task(database, description):
    """Add a new task to the database."""
    id = str(max(map(int, database.keys()), default=0) + 1)
    timer = datetime.now().isoformat()

    database[id] = {
        "description": description,
        "status": "todo",
        "created_at": timer,
        "updated_at": timer,
    }

def update_task():
    pass

def list_task():
    pass
if __name__ == "__main__":
    main()
