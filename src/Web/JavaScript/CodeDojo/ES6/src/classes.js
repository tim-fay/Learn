class Task {
    constructor(title = Task.getDefaultTitle()) {
        this.title = title;
        this._done = false;
        Task.count += 1;
        console.log('Task creation');
    }

    get done() {
        return this._done === true ? 'Completed' : 'Uncompleted';
    }

    set done(value) {
        if (value !== undefined && typeof value === 'boolean') {
            this._done = value;
        } else {
            console.error('Error! specify values true or false.');
        }
    }

    complete() {
        this.done = true;
        console.log(`Task "${this.title}" is complete`);
    }

    static getDefaultTitle() {
        return 'Task';
    }
}

Task.count = 0;

let task = new Task('Clean up the room');
console.log(task.done, task._done);
task.complete();
console.log(task.done, task._done);

// let task2 = new Task('Buy food');
// let task3 = new Task();

// console.log(task.title);
// console.log(task2.title);
// console.log(task3.title);

// console.log(Task.count);

// task2.complete();

// task.getDefaultTitle() - raises an error as this is not an instance method but a type method.

// console.log(typeof task);
// console.log(task instanceof Task);