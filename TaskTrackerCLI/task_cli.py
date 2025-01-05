from argparse import ArgumentParser
from datetime import datetime
from rich.console import Console
from rich.table import Table
import json

def main():
    database = load_database()
    list_tasks(database)

    parser = ArgumentParser(description="Task Tracker CLI")
    parser.add_argument("command", choices=["add", "update", "list"], help="Action to perform")
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
    elif args.command == "list":
        list_tasks(database)

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

def list_tasks(database):
    table = Table(title="Task List")

    table.add_column("ID", style="cyan")
    table.add_column("Description", style="magenta")
    table.add_column("Status", style="bold")
    table.add_column("Created At", style="dim")
    table.add_column("Updated At", style="dim")

    for task_id, task in database.items():
        status_color = "red" if task["status"] == "todo" else "yellow" if task["status"] == "in progress" else "green"
        table.add_row(
            task_id,
            task["description"],
            f"[{status_color}]{task['status']}[/{status_color}]",
            task["created_at"],
            task["updated_at"]
        )
    
    console = Console()
    console.print(table)
def delete_task(database, task_id):
    """Delete a task from the database."""
    task_id = str(task_id)  
    
    if task_id in database:
        del database[task_id]  
        print(f"Task {task_id} deleted successfully.")
        save_database(database)  
    else:
        print(f"Task with ID {task_id} does not exist.")


if __name__ == "__main__":
    main()
