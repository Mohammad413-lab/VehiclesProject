import { ApiServices } from "../../apiservices/ApiServices.js";
import { eventListenerAsync } from "../../functions/EventListener.js";

let emailInput = document.getElementById("EmailInput");
let buttonVerfied = document.getElementById("ButtonVerfied");
let emailHint = document.getElementById("EmailHint");


eventListenerAsync(buttonVerfied, async function () {
   
    emailHint.style.color = "#a3bfb9";
    if (emailInput.value != "") {
        const response = await ApiServices.emailVerfied(emailInput.value);

        if (!response.status) {
            emailHint.textContent = response.message;
            emailHint.style.color = "red";
            
        } else {
            emailHint.textContent = response.message;
        }
    }else{
        emailHint.textContent = "please enter your email";
        emailHint.style.color = "red";
    }
    emailHint.style.display = "flex";

});

