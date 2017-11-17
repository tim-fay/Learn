class Task {
    constructor(title) {
        this._title = title;
        this.done = false;
        Task.count += 1;
        console.log('Task creation');
    }

    get title() {
        return this._title;
    }

    set title(value) {
        this._title = value;
    }

    static getDefaulttitle() {
        return 'Task';
    }

    complete() {
        this.done = true;
        console.log(`Task ${this.title} complete`);
    }
}

Task.count = 0;

class SubTask extends Task {
    constructor(title, parent) {
        super(title);
        this.parent = parent;
        console.log('Sub-task creation');
    }

    complete() {
        super.complete();
        console.log(`Sub-task ${this.title} complete`);
    }
}

let task = new Task('Learn JavaScript');
let subtask = new SubTask('Learn ES6', task);

task.complete();
subtask.complete();

console.log(SubTask.getDefaulttitle());
console.log(SubTask.count);

console.log(task);
console.log(subtask);

// console.log(subtask instanceof SubTask);
// console.log(subtask instanceof Task);