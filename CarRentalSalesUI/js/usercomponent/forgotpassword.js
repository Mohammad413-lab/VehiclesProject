
import { eventListenerAsync } from "../../functions/EventListener.js";
import { ApiServices } from "../../apiservices/ApiServices.js";
let emailInput = document.getElementById("EmailInput");
let resetPasswordButton = document.getElementsByClassName("BtnSize")[0];
let emailHint = document.getElementById("EmailHint");

eventListenerAsync(resetPasswordButton, async function () {
    emailHint.style.display = "none";
    emailHint.style.color="#a3bfb9";
    if (emailInput.value != "") {
        let response = await ApiServices.resetPassword(emailInput.value);
        if (response != null && response.status) {
            emailHint.style.display = "flex";
            emailHint.textContent = response.message;
        }else{
            emailHint.style.display = "flex";
            emailHint.textContent = response.message;
            emailHint.style.color="gray";
        }
    }else{
        emailHint.style.display = "flex";
        emailHint.textContent="please enter your valid email";
        emailHint.style.color="red";

    }

   
   
});


