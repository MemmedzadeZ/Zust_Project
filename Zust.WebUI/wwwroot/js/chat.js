"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

connection.start().then(function () {
    GetAllUsers();
})
    .catch(function (err) {
        return console.error(err.toString());
    });

const element = document.querySelector("#alert");


if (element) {
    element.style.display = "none";
}


connection.on("Connect", function (info) {
    GetAllUsers();  
    if (element) {
        element.style.display = "block"; 

        element.innerHTML = info; 
        setTimeout(() => {
            element.style.display = "none"; 
            element.innerHTML = ""; 
        }, 5000);
    }
});


connection.on("Disconnect", function (info) {
    GetAllUsers();  
    if (element) {
        element.style.display = "block";

        element.innerHTML = info; 
        setTimeout(() => {
            element.style.display = "none"; 
            element.innerHTML = ""; 
        }, 5000);
    }
});
