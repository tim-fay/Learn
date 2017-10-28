'use strict';

// function add(x, y) {
//     return x + y;
// }
var add = function add(x, y) {
    return x + y;
};

// let square = function(x) {
//     return x * x;
// }
var square = function square(x) {
    return x * x;
};

// let giveMeAnswer = function() {
//     return 42;
// }
var giveMeAnswer = function giveMeAnswer() {
    return 42;
};

// let log = function() {
//     console.log('logging');
// }
var log = function log() {
    return console.log('logging');
};

// let multiply = function(x, y) {
//     let result = x * y;
//     return result;
// }
var multiply = function multiply(x, y) {
    var result = x * y;
    return result;
};

// let getPerson = function() {
//     return { name: 'John' };
// }
var getPerson = function getPerson() {
    return { name: 'John' };
};

// (function() {
//     console.log('IIFE'); // Immediately Invoked Function Expression
// })();
(function () {
    return console.log('IIFE');
})();

//console.log(typeof add);
// console.log(add(2, 5));
// console.log(square(3));
// console.log(giveMeAnswer());
// log();
// console.log(multiply(3, 7));
// console.log(getPerson());

var numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

var sum = 0;

// numbers.forEach(function(num) {
//     sum += num;
// });
numbers.forEach(function (num) {
    return sum += num;
});

var squared = numbers.map(function (n) {
    return n * n;
});

console.log(sum);
console.log(squared);

// let person = {
//     name: 'Bob',
//     greet: function() {
//         var that = this;
//         window.setTimeout(function() {
//             console.log(`Hello my name is ${that.name}`);
//             console.log('"this" is', this);
//             console.log('"that" is ', that);
//         }, 2000);
//     }
// };
var person = {
    name: 'Bob',
    greet: function greet() {
        var _this = this;

        window.setTimeout(function () {
            console.log('Hello my name is ' + _this.name);
            console.log('"this" is', _this);
        }, 2000);
    }
};

person.greet();