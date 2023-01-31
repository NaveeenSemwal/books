
//  ----------------------------------------- Rest and Spread operator--------------------------------------------------------


//  Any variable name followed by ...name is SPREAD and REST operator.  Diffence is the scenrios in which get used.

// Note : Rest and Spread operator are used with Array and Object.

// Rest : It is used to combine the values
// Spread : It is used to spread the values


// _________________________________________ Rest with Array__________________________________________________

function add(a, b, c, ...other) {

    console.log(...other);
  
      return a + b + c;
  }
  
  var result1 = add(2, 3, 4, 5, 6)
  
  console.log(result1);
  
  // _________________________________________ Rest with Array__________________________________________________
  let student = {
      name: "Naveen",
      age: 34,
      hobbies: ["Playing", "Singing"]
  };
  
  // This is array destructing in ES6
  const {age, ...remaining} = student;
  
  console.log(age,remaining);
  
  // ----------------------------------------------------------------------------------------------------------
  
  //___________________________________ Spread with Array______________________________________________________
  
  let names = ["Naveen", "Chander", "Semwal"];
  
  function getnames(name1, name2, name3) {
  
     console.log(name1, name2, name3);
  
  }
  
  getnames(...names);
  
  //___________________________________ Spread with Object______________________________________________________
  let newstudent = {
      name: "Naveen",
      age: 34,
      hobbies: ["Playing", "Singing"]
  };
  
  // We need to update the value of age
  let updatedsu =  {...newstudent, age: 35};
  
  console.log(updatedsu);
  
  //------------------------------------------------------------------------------------------------------------