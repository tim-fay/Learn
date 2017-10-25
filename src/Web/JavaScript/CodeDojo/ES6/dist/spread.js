'use strict';

var staticLanguages = ['C', 'C++', 'C#'];
var dynamicLanguages = ['JavaScript', 'PHP', 'Ruby'];

var languages = [].concat(staticLanguages, ['Java'], dynamicLanguages, ['Python']);

console.log(languages);

function add(x, y, z) {
    console.log(x + y + z);
}

var numbers = [1, 2, 3];

add.apply(undefined, numbers);