# Task Tracker CLI

A simple Command Line Interface (CLI) tool to manage your tasks. This tool allows users to add, update, delete, and list tasks, while also marking tasks with various statuses such as "todo", "in-progress", or "done".
project url: https://roadmap.sh/projects/task-tracker
## Features

- **Add tasks**: Add a new task with a description.
- **Update tasks**: Update a task's description and status.
- **Delete tasks**: Remove a task from the task list.
- **List tasks**: View all tasks or filter by status (todo, in progress, done).
- **Color-coded status**: Task status is color-coded when listed in the terminal.

## Task Properties

Each task includes the following properties:

- `id`: A unique identifier for the task
- `description`: A short description of the task
- `status`: The status of the task (`todo`, `in-progress`, or `done`)
- `createdAt`: The date and time when the task was created
- `updatedAt`: The date and time when the task was last updated

## Installation

1. Clone this repository to your local machine:
    ```bash
    git clone https://github.com/AdrianLopianowski/Mini-Projects.git
    cd Mini-Projects/TaskTrackerCLI
    ```

## Usage

### Add a task
To add a new task:

```bash
python task_cli.py add "Buy groceries"

###Update a task
To update the description of an existing task:

python task_cli.py update --id 1 --description "Buy groceries and cook dinner"

Mark a task as in progress or done
To mark a task as "in progress" or "done":

python task_cli.py update --id 1 --status "in progress"
python task_cli.py update --id 1 --status "done"


Delete a task
To delete a task by its ID:

python task_cli.py delete --id 1

List all tasks
To list all tasks:

python task_cli.py list


List tasks by status
To list tasks by specific statuses (todo, in progress, done):

python task_cli.py list todo
python task_cli.py list in-progress
python task_cli.py list done

Files
task_cli.py: Main script to handle the CLI functionality.
tasks.json: JSON file where tasks are stored.

