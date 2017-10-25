let staticLanguages = ['C', 'C++', 'C#'];
let dynamicLanguages = ['JavaScript', 'PHP', 'Ruby'];

let languages = [...staticLanguages, 'Java', ...dynamicLanguages, 'Python'];

console.log(languages);


function add(x, y, z) {
    console.log(x + y + z);
}

let numbers = [1, 2, 3];

add(...numbers);