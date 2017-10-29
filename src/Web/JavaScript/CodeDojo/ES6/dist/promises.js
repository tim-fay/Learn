'use strict';

// Callback HELL example
// function applyForVisa(documents, resolve, reject) {
//     console.log('Processing application...');
//     setTimeout(function() {
//         Math.random() > .5 ? resolve({}) : reject('Visa rejected: not enough documents');
//     }, 2000);
// }

// function bookHotel() {

// }

// function buyTickets() {

// }

// applyForVisa({}, function(visa) {
//     console.info('Visa granted');
//     bookHotel(visa, function(reservation) {
//         buyTickets(reservation, function() {

//         }, function() {

//         });
//     }, function(error) {

//     });
// },
// function(reason) {
//     console.error(reason);
// });

function applyForVisa(documents) {
    console.log('Processing application...');
    var promise = new Promise(function (resolve, reject) {
        setTimeout(function () {
            Math.random() > 0 ? resolve({}) : reject('Visa rejected: not enough documents');
        }, 2000);
    });
    return promise;
}

function getVisa(visa) {
    console.info('Visa granted');
    return new Promise(function (resolve, reject) {
        setTimeout(function () {
            return resolve(visa);
        }, 2000);
    });
}

function bookHotel(visa) {
    console.log(visa);
    console.log('Booking hotel');
    return Promise.resolve(visa);
    // return new Promise(function(resolve, reject) {
    //     resolve({});
    //     // reject('No rooms'); // this is for example
    // });
}

function buyTickets(booking) {
    console.log('Buying tickets');
    console.log('Booking', booking);
}

applyForVisa({}).then(getVisa).then(bookHotel).then(buyTickets).catch(function (error) {
    return console.error(error);
});