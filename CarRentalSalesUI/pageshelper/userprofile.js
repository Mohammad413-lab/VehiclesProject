import { UserKey } from "../static/User.js";
import { eventListener, eventListenerAsync } from "../functions/EventListener.js";
import { ApiServices } from "../apiservices/ApiServices.js";
import { selectCountries } from "../functions/SelecteCountries.js";
import { User } from "../models/user.js";
import { showSucessfulLogo } from "./sendmessage.js";
import { ValidationService } from "../functions/Validation.js";
export async function loadingUserProfile() {
    const allCountries = await ApiServices.getAllCountry();
    return fetch("../../pageshelper/userprofile.html")
        .then(response => response.text())
        .then(data => {
            function resetInput() {
                inputFirstName.value = UserKey.User.firstName;
                inputLastName.value = UserKey.User.lastName;
                inputUserEmail.value = UserKey.User.email;
                inputUserPhone.value = UserKey.User.phoneNumber;
                inputUserAddress.value = UserKey.User.address;
                userId.textContent = UserKey.User.userID;
                selectCountries(profileSelectedCountry, allCountries);
            }
            function toggleButton(isLoading) {
                switch (isLoading) {
                    case true: loadingContainer.style.display = "flex";
                        saveButton.style.display = "none";
                        savePasswordButton.style.display = "none"
                        break;
                    default: loadingContainer.style.display = "none";
                        saveButton.style.display = "block";
                        savePasswordButton.style.display = "block"
                }


            }
            console.log(UserKey.User)
            document.getElementById("UserProfile").innerHTML = data;
            const showChangePasswordB = document.getElementById("ShowChangePasswordButton");
            const passwordContainer = document.getElementById("ChangePasswordContainer");
            let toggleContainer = true;
            const userId = document.getElementById("UserId");
            const inputFirstName = document.getElementById("InputFirstName");
            const inputLastName = document.getElementById("InputLastName");
            const inputUserEmail = document.getElementById("InputUserEmail");
            const inputUserPhone = document.getElementById("InputUserPhone");
            const inputUserAddress = document.getElementById("InputUserAddress");
            const inputOldPassword = document.getElementById("InputOldPassword");
            const inputNewPassword = document.getElementById("InputNewPassword");
            const profileSelectedCountry = document.getElementById("ProfileSelectedCountry");
            const loadingContainer = document.getElementById("ShowLoading");
            const errorParagraph = document.getElementById("ErrorParagraph");
            const saveButton = document.getElementById("SaveButton");
            const savePasswordButton = document.getElementById("SavePasswordButton");
            const exitButton = document.getElementById("ExitButton");



            resetInput();
            eventListener(showChangePasswordB, function () {
                switch (toggleContainer) {
                    case true: passwordContainer.style.display = "flex";
                        break;
                    case false: passwordContainer.style.display = "none";
                }
                toggleContainer = !toggleContainer;
            });
            eventListenerAsync(saveButton, async function () {
                toggleButton(true);
                let firstName = inputFirstName.value;
                let lastName = inputLastName.value;
                let email = inputUserEmail.value;
                let phoneNumber = inputUserPhone.value;
                let countryId = profileSelectedCountry.value;
                let address = inputUserAddress.value;
                let user = new User(UserKey.UserId, firstName, lastName, email, phoneNumber, null, countryId, address);
                let response = await ApiServices.editUser(user);
                toggleButton(false);
                if (response.Ok) {
                    if (response.Data) {
                        showSucessfulLogo(response.Data.message);
                    }
                }

            });
            eventListenerAsync(savePasswordButton, async function () {
                errorParagraph.style.display = "none";
                let oldPassword = inputOldPassword.value;
                let newPassword = inputNewPassword.value;
                toggleButton(true);

                let response = await ApiServices.changePassword(UserKey.UserId, oldPassword, newPassword);
                if (response.Ok) {
                    if (response.Data.status) {

                    } else {
                        errorParagraph.textContent = response.Data.message;
                        errorParagraph.style.display = "flex";
                    }
                }
                toggleButton(false);
            });

            eventListener(exitButton, () => hideUserProfile());

        });
}


export function showUserProfile() {
    const userProfileContainer = document.getElementById("UserProfileContainer");
    userProfileContainer.classList.remove("Hidden");

}

export function hideUserProfile() {
    const userProfileContainer = document.getElementById("UserProfileContainer");
    userProfileContainer.classList.add("Hidden");

}




