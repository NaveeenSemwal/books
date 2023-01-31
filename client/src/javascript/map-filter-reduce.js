// Higher order function

// 1. forEach
// 2. filter
// 3. map
// 4. sort
// 5. reduce


const companies = [
    { name: "Google", category: "Product based", start: 1989, end: 2009 },
    { name: "Amazon", category: "Product based", start: 1981, end: 2009 },
    { name: "Facebook", category: "Product based", start: 1985, end: 2009 },
    { name: "Coforge", category: "Service based", start: 1980, end: 2009 },
    { name: "Cognizant", category: "Service based", start: 1989, end: 2009 }
];


// forEach

let callbackfn = function (values, index, items) {

    console.log(values.start);
}

companies.forEach(callbackfn);


companies.forEach(function (values, index, items) {

    console.log(values.name);
})


companies.forEach((values, index, items) => {

    console.log(values.category);
})



// filter

let result = companies.filter(x => x.start > 1985);
console.log(result);



// map

// Note : Map function creates a new array but forEach doesn't. This is the difference
let callbackmapfn = function (values, index, items) {

    console.log(values.start);
}
companies.map(callbackmapfn);


companies.map((values, index, items) => {

    console.log(values.end);
})


// Sort

const sortedCompanies = companies.sort((c1, c2) => {

    return c1.start > c2.start; // sort in asc order
})

const sorteddescCompanies = companies.sort((c1, c2) => {

    return c1.start < c2.start; // sort in desc order
})
console.log(sorteddescCompanies);



// reduce  :  Reduce .. accumulate karta hai

let ages = [23, 24, 25, 44, 32, 65, 87];

// without reduce .. caclulate some of ages
let total = 0;

for (let index = 0; index < ages.length; index++) {
    total = total + ages[index];

}
console.log(total);


// with reduce

const sumages = ages.reduce((total, age) => {

    return total + age;
},0)

console.log(sumages);