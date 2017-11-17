'use strict';

var firstName = 'Bill',
    lastName = 'Gates',
    email = 'bill.gates@microsoft.com';

var person = {
    firstName: firstName,
    lastName: lastName,
    email: email,
    sayHello: function sayHello() {
        console.log('Hi my name is ' + this.firstName + ' ' + this.lastName);
    },

    get fullName() {
        return this.firstName + ' ' + this.lastName;
    },
    set fullName(value) {
        this.firstName = value;
    }
};

// ES5 
// Object.defineProperty(person, 'fullName', {
//     get: function() {
//         return this.firstName + ' ' + this.lastName;
//     },
//     set: function(value) {
//         this.firstName = value;
//     }
// })

console.log(person);
// person.sayHello();

// person.firstName;
// person['firstName'];

// let property = 'lastName';
// person[property]; // person['lastName']
// person = {
//     [property]: 'Bill'
// };

// ES5
// function createCar(property, value) {
//     var car = {};

//     car[property] = value;

//     return car;
// }

// function createCar(property, value) {
//     return {
//         [property]: value,
//         ['_' + property]: value,
//         [property.toUpperCase()] : value,
//         ['get' + property]() {
//             return this[property];
//         }
//     };
// }

// console.log(createCar('vin', 1));