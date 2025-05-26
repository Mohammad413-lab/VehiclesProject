

import { eventListenerAsync, eventListener } from "../../functions/EventListener.js";
import { ApiServices } from "../../apiservices/ApiServices.js";
import { selectCountries } from "../../functions/SelecteCountries.js";
import { showSuccess, loadAlertBox } from "../../components/alert.js";
import { ValidationService } from "../../functions/Validation.js";
import { SignUpDtos } from "../../dtos/request/signupdtos.js";


loadAlertBox();
function isInputNotEmpty(input, message) {
    if (input.value === "") {
        message.style.display = "flex";
        return false;
    }

    message.style.display = "none";
    return true;
}

function isValidInput(bool, p, warnMessage) {
    p.style.display = 'none';
    if (bool) {
        return true;
    }
    p.textContent = warnMessage
    p.style.display = "flex";
    return false;
}


function checkeAllInput(firstName, lastName, email, phone, address, password, confirmPassword, countrySelect) {
    let isValid = true;
    isValid = isValid && isInputNotEmpty(firstName, warningFirstName);
    isValid = isValid && isInputNotEmpty(lastName, warningLastName);
    isValid = isValid && isInputNotEmpty(email, warningEmail);
    isValid = isValid && isInputNotEmpty(phone, warningPhone);
    isValid = isValid && isInputNotEmpty(address, warningAddress);
    isValid = isValid && isInputNotEmpty(password, warningPassword);
    isValid = isValid && isInputNotEmpty(confirmPassword, warningConfirmPassword);
    isValid = isValid && isInputNotEmpty(countrySelect, warningSelect);

    return isValid;
}

const firstName = document.getElementsByName("firstName")[0];
const lastName = document.getElementsByName("lastName")[0];
const email = document.getElementsByName("email")[0];
const phone = document.getElementsByName("phone")[0];
const address = document.getElementsByName("address")[0];
const password = document.getElementsByName("password")[0];
const confirmPassword = document.getElementsByName("confirmpassword")[0];
let warningFirstName = document.getElementById("WarningFirstName");
let warningLastName = document.getElementById("WarningLasttName");
let warningEmail = document.getElementById("WarningEmail");
let warningPhone = document.getElementById("WarningPhone");
let warningAddress = document.getElementById("WarningAddress");
let warningPassword = document.getElementById("WarningPassword");
let warningConfirmPassword = document.getElementById("WarningConfirmPassword");
let allCountries = await ApiServices.getAllCountry();
let warningSelect = document.getElementById("WarningSelected");
let countrySelect = document.getElementById("CountrySelect");

selectCountries(countrySelect, allCountries);


let registerButton = document.getElementById("RegisterButton");

eventListenerAsync(registerButton, async function () {
    warningConfirmPassword.style.display = "none";
    warningFirstName.textContent = "please enter your first name";
    warningLastName.textContent = "please enter your last name";
    warningEmail.textContent = "please enter your email";
    warningPhone.textContent = "please enter your phone number";
    warningAddress.textContent = "please enter your address";
    warningPassword.textContent = "please enter your password";
    warningConfirmPassword.textContent = "please confirm your password";

    console.log("there is password: " + password);

    console.log(ValidationService.isValidEmail(email.value));
    if (checkeAllInput(firstName, lastName, email, phone, address, password, confirmPassword, countrySelect)) {

        if (isValidInput(ValidationService.isValidEmail(email.value), warningEmail, "please enter  valid email") &&
            isValidInput(ValidationService.isValidPhoneNumber(phone.value), warningPhone, "please enter valid phone number") &&
            isValidInput(ValidationService.isValidPassword(password.value), warningPassword, "password must contain 8 characters, an uppercase letter, a lowercase letter, and a number")) {
            if (password.value == confirmPassword.value) {

                let user = new SignUpDtos(firstName.value, lastName.value, email.value, phone.value, address.value, password.value, countrySelect.value);
                let response = await ApiServices.userSignUp(user)
                console.log(response);
                if (!response.status) {
                    if (response.message == "email is already exist") {
                        warningEmail.textContent = "email is already exist"
                        warningEmail.style.display = "flex";
                    } else {
                        window.alert(response.message);
                    }
                } else {
                    showSuccess();
                }

            } else {
                warningConfirmPassword.textContent = "not matched your password";
                warningConfirmPassword.style.display = "flex";
            }


        }
    };
});