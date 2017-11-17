// // function declaration
// function Task() {

// }

// // function expression
// let task = function Task() {

// }

// // class declaration
// class Task {
//     constructor() {

//     }
// }

// class expression
let Task = class {
    constructor() {
        console.log('Creating a task');
    }
}

let SubTask = class extends Task {
    constructor() {
        super();
        console.log('Creating a subtask');
    }
}

let task = new Task();
let subtask = new SubTask();