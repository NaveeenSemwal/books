// const companies = [
//     { name: "Google", category: "Product based", start: 1989, end: 2009 },
//     { name: "Amazon", category: "Product based", start: 1981, end: 2009 },
//     { name: "Facebook", category: "Product based", start: 1985, end: 2009 },
//     { name: "Coforge", category: "Service based", start: 1980, end: 2009 },
//     { name: "Cognizant", category: "Service based", start: 1989, end: 2009 }
// ];

class Company {
    constructor(name, category, start, end) {

        this.name = name;
        this.category = category;
        this.start = start;
        this.end = end;

    }
}

const companies = [
    new Company("Google", "Product based", 1989, 2009),
    new Company("Amazon", "Product based", 1989, 2009)
];

function updatecompany(company) {
    const index = companies.indexOf(company)

    console.log(index);
}

var company =  new Company("Amazon", "Product based", 1989, 2009);

updatecompany(company);



