'use strict';

var person = {
    firstname: 'John',
    lastname: 'Doe'
};

// ES5
// let firstname = person.firstname;
// let lastname = person.lastname;

// let {firstname, lastname} = person;
// console.log(firstname, lastname);

// let {firstname: first, lastname: last} = person;
// let {firstname: first, lastname: last, age = 25} = {firstname: 'John', lastname: 'Doe'}; //
// console.log(first, last, age);

var user = {
    firstname: 'John',
    lastname: 'Doe',
    social: {
        facebook: 'johndoe',
        twitter: 'jdoe'
    }
};

var first = user.firstname,
    last = user.lastname,
    fb = user.social.facebook,
    _user$age = user.age,
    age = _user$age === undefined ? 25 : _user$age;

console.log(first, last, fb, age);

function post(url, _ref) {
    var _ref$data = _ref.data,
        firstname = _ref$data.firstname,
        lastname = _ref$data.lastname,
        cache = _ref.cache;

    console.log(firstname, lastname, cache);
}

var result = post('api/users', { data: user, cache: false });

function getUserInfo() {
    return {
        firstname: 'John',
        lastname: 'Doe',
        social: {
            facebook: 'johndoe',
            twitter: 'jdoe'
        }
    };
}

var _getUserInfo = getUserInfo(),
    firstname = _getUserInfo.firstname,
    lastname = _getUserInfo.lastname,
    twitter = _getUserInfo.social.twitter;

console.log(firstname, lastname, twitter);