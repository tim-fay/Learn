'use strict';

var _slicedToArray = function () { function sliceIterator(arr, i) { var _arr = []; var _n = true; var _d = false; var _e = undefined; try { for (var _i = arr[Symbol.iterator](), _s; !(_n = (_s = _i.next()).done); _n = true) { _arr.push(_s.value); if (i && _arr.length === i) break; } } catch (err) { _d = true; _e = err; } finally { try { if (!_n && _i["return"]) _i["return"](); } finally { if (_d) throw _e; } } return _arr; } return function (arr, i) { if (Array.isArray(arr)) { return arr; } else if (Symbol.iterator in Object(arr)) { return sliceIterator(arr, i); } else { throw new TypeError("Invalid attempt to destructure non-iterable instance"); } }; }();

// let languages = ['JavaScript', 'PHP', 'Python', 'Ruby'];

// let js = languages[0];
// let php = languages[1];
// let py = languages[2];
// let rb = languages[3];

// let js, php, py, rb;
// [js, php, py, rb] = languages;
var js = 'JavaScript',
    php = 'PHP',
    py = 'Python',
    rb = 'Ruby';


console.log(js, php, py, rb);

// let scores = [3, 4, 5];

// let [low, mid, high, higher] = scores;
// console.log(low, mid, high, higher); // higher === undefined

// let [low, , high] = scores;
// console.log(low, high); // out: 3, 5


// let scores = [3, 4, 5, 6, 7];
// let [low, ...rest] = scores;
// console.log(low);
// console.log(rest);


// let scores = [3, 4];
// let [low, mid, high = 5] = scores;
// console.log(low, mid, high);


// let scores = [3, 4, [5, 6]];
// let [low, mid, [high, higher]] = scores;
// console.log(low, mid, high, higher);


function computeScore(_ref) {
    var _ref2 = _slicedToArray(_ref, 2),
        low = _ref2[0],
        mid = _ref2[1];

    console.log(low, mid);
}

computeScore([3, 4]);

function getScores() {
    return [3, 4, 5];
}

var scores = getScores();
console.log(scores);

var _getScores = getScores(),
    _getScores2 = _slicedToArray(_getScores, 3),
    low = _getScores2[0],
    mid = _getScores2[1],
    high = _getScores2[2];

console.log(low, mid, high);

var yes = 'Yes';
var no = 'No';

var _ref3 = [no, yes];
yes = _ref3[0];
no = _ref3[1];

console.log('Yes is', yes);
console.log('No is', no);