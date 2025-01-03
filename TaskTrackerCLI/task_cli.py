from argparse import ArgumentParser
from datetime import datetime
from tabulate import tabulate
import json

def main():
    database = load_database()

    parser = ArgumentParser(description="Task Tracker CLI")
    parser.add_argument("command", choices=["add", "update"], help="Action to perform")
    parser.add_argument("--id", type=int, help="Task ID to update (for 'update')")
    parser.add_argument("--description", help="Description of the task")
    parser.add_argument("--status", choices=["todo", "in progress", "done"], help="New status for the task (for 'update')")

    args = parser.parse_args()

    if args.command == "add":
        if not args.description:
            print("Please provide a description to add a task.")
        else:
            add_task(database, args.description)
            save_database(database)
            print("Task added successfully!")

    elif args.command == "update":
        if not args.id and not (args.description or args.status):
            print("Please provide --id and at least one of --description or --status to update a task.")
        else:
            update_task(database, args.id, args.description, args.status)
            save_database(database)


def load_database():
    try:
        with open('tasks.json', 'r') as file:
            database = json.load(file)
    except FileNotFoundError:
        database = {}
    return database

def save_database(database):
    with open("tasks.json", 'w') as file:
        json.dump(database, file)

def add_task(database, description):
    id = str(max(map(int, database.keys()), default=0) + 1)
    timer = datetime.now().isoformat()

    database[id] = {
        "description": description,
        "status": "todo",
        "created_at": timer,
        "updated_at": timer,
    }

def update_task(database, id, description=None, status=None):
    if str(id) in database:
        task = database[str(id)]
        if description:
            task["description"] = description
        if status and status in ["todo", "in progress", "done"]:
            task["status"] = status
        elif status:
            print(f"Invalid status: {status}. Allowed values are 'todo', 'in progress', or 'done'.")
        task["updated_at"] = datetime.now().isoformat()
        print(f"Task {id} updated successfully!")
    else:
        print(f"Task with ID {id} does not exist.")


if __name__ == "__main__":
    main()
