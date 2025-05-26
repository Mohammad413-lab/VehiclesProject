
import { eventListener, eventListenerAsync } from "../../functions/EventListener.js";
import { fillSelectedOption } from "../../functions/FillSelectedOptions.js";
import { AddCar } from "../../dtos/request/addcardtos.js";
import { UserKey } from "../../static/User.js";
import { uploadCarImages } from "../../functions/UploadImagesCar.js";
import { ApiServices, } from "../../apiservices/ApiServices.js";
import { showSucessfulLogo } from "../../pageshelper/sendmessage.js";
import { loadingSideBar } from "../../pageshelper/sidebar.js";
import { loadSwiper } from "../../functions/Swiper.js";
import { loadingNoMore } from "../../pageshelper/nomorecars.js";
import { pushSideBar } from "../../pageshelper/carul.js";


await loadingSideBar();
await loadingNoMore();

console.log(window.Swiper)
function showHiddenComponent() {
    if (!isShow) {
        divInputModelName.classList.remove("Hidden");
        noteCarMake.classList.remove("Hidden");
        CarMakeContainer.classList.remove("Hidden");
    } else {
        divInputModelName.classList.add("Hidden");
        noteCarMake.classList.add("Hidden");
        CarMakeContainer.classList.add("Hidden");
    }
    isShow = !isShow;

}
 async function changeFileEvent(swiper,file) {
    file.addEventListener("change", async () => {
        const files = fileInput.files;
        wrapper.innerHTML = "";

        Array.from(files).forEach(file => {
            const reader = new FileReader();
            reader.onload = e => {
                const slide = document.createElement("div");
                slide.className = "swiper-slide";
                const img = document.createElement("img");
                img.src = e.target.result;
                slide.appendChild(img);
                wrapper.appendChild(slide);
                swiper.update();
            };

            reader.readAsDataURL(file);
        });


    });
}

export function checkInputYear(year,warn) {

    function showWarnYear(text, bool) {
        if (bool) {
            warn.textContent = text;
            warn.style.display = "flex";
            return;
        }

        warn.style.display = "none";
    }
    if (year == '') { showWarnYear("please type your car year", true); return false; }
    const num = Number(year);
  
    if (Number.isInteger(num)) {
        if (num >= 1885 && num<=new Date().getFullYear()) {
            showWarnYear('', false);
            return true;
        }
        showWarnYear("a car year more than 1884 and equal to "+ new Date().getFullYear(), true);
        return false
    }
    showWarnYear("please type a car year", true);
    return false

}


export function showErrorText(text, warningP, bool) {
    if (bool) {
        warningP.textContent = '';
        warningP.style.display = "none";
        return bool;
    }
    warningP.textContent = text;
    warningP.style.display = "flex";
    return bool

}





const swiper = await loadSwiper();
const showCreateModelButton = document.getElementById("ShowCreateModel");
const divInputModelName = document.getElementById("DivInputModelName")
const noteCarMake = document.getElementById("NoteCarMake");
const CarMakeContainer = document.getElementById("CarMakeContainer");
const showInputCarMakeButton = document.getElementById("ShowInputCarMake");
const diveInputCarMake = document.getElementById("DiveInputCarMake");
const selectedModel = document.getElementById("SelectedModel");
const selectedMake = document.getElementById("SelectedMake");
const inputMakeName = document.getElementById("InputMakeName");
const inputModelName = document.getElementById("InputModelName");
const saveModelNameButton = document.getElementById("SaveModelNameButton");
const saveMakeNameButton = document.getElementById("SaveMakeNameButton");
const warningModel = document.getElementById("WarningModel");
const warningMake = document.getElementById("WarningMake");
const warningPriceOrRental = document.getElementById("WarningInputPriceOrReental");
const warningColor = document.getElementById("WarningInputColor");
const warningYear = document.getElementById("WarningInputYear");
const inputPrice = document.getElementById("InputPrice");
const inputRentalPrice = document.getElementById("InputRentalPrice");
const inputColor = document.getElementById("InputColor");
const inputYear = document.getElementById("InputYear");
const descriptionInput = document.getElementById("DescriptionInput");
const addCarButton = document.getElementById("AddCarButton");
const fileInput = document.getElementById("fileInput");
const wrapper = document.getElementById("Wrapper");
const showOrHideAddCarButton = document.getElementById("ShowOrHideAddCarButton");
const addCarContainer = document.getElementById("AddCarContainer");

let isAddCarContainerShow = false;

await changeFileEvent(swiper,fileInput);
let isShow = false;
let isClickedCarMakeButton = true;
export const userModels = await ApiServices.getUserModels();
const userMakes = await ApiServices.getUserMakes();

try {


    if (selectedModel && Array.isArray(userModels) && userModels.length) {
        userModels.forEach(model => fillSelectedOption(selectedModel, model.modelId, model.modelName));
    }

    if (selectedMake && Array.isArray(userMakes) && userMakes.length) {
        userMakes.forEach(make => fillSelectedOption(selectedMake, make.makeId, make.makeName));
    }

} catch (error) {
    console.error(" ERROR WHILE LOADING USER -> MAKES , MODELS: معلش ", error);
}




eventListener(showOrHideAddCarButton, function () {
    isAddCarContainerShow ? addCarContainer.classList.add("Hidden") : addCarContainer.classList.remove("Hidden");
    isAddCarContainerShow = !isAddCarContainerShow;
});
eventListener(showCreateModelButton, function () {
    showHiddenComponent();
});

eventListener(showInputCarMakeButton, function () {
    if (isClickedCarMakeButton) {
        diveInputCarMake.classList.remove("Hidden");
    } else {
        diveInputCarMake.classList.add("Hidden");
    }
    isClickedCarMakeButton = !isClickedCarMakeButton;
});
eventListenerAsync(saveModelNameButton, async function () {

    let modelName = inputModelName.value;
    let makeId = selectedMake.value;
    if (showErrorText("please type car model", warningModel, modelName != '') && showErrorText("please create or select make", warningMake, makeId != '')) {
        let response = await ApiServices.addCarModel(makeId, modelName);

        if (response.Status == 200) {
            showSucessfulLogo("Model has been created");
        }
    }
});

eventListenerAsync(saveMakeNameButton, async function () {
    let makeName = inputMakeName.value;
    if (showErrorText("please type make name for cars", warningMake, makeName != "")) {
        let response = await ApiServices.addCarMake(makeName);
        console.log(response);
        if (response.Status == 200) {
            showSucessfulLogo("Make has been created");
        }
    }
});
eventListenerAsync(addCarButton, async function () {

    // please don't close the windows (cmd and chrome) :), iam not, okay thank you 
    console.log(fileInput.files);
    let checkInputPrice = showErrorText("please type your car price or Rental price..", warningPriceOrRental, inputPrice.value != '');
    let checkInputRental = false;
    let checkColor = showErrorText("please type your car color..", warningColor, inputColor.value != '');
    let checkYear = checkInputYear(inputYear.value,warningYear);
    let checkModelId = showErrorText("please select car model or created one", warningModel, selectedModel.value != '')
    if (!checkInputPrice) {
        checkInputRental = showErrorText("please type your car price or Rental price..", warningPriceOrRental, inputRentalPrice.value != '');
    }
    if ((checkInputPrice || checkInputRental) && checkColor && checkYear && checkModelId) {
        let modelId = selectedModel.value;
        let year = inputYear.value;
        let color = inputColor.value;
        let carPrice = inputPrice.value != "" ? inputPrice.value : null;
        let carRentalPrice = inputRentalPrice.value != "" ? inputRentalPrice.value : null;
        let description = descriptionInput.value != '' ? descriptionInput.value.replace(/\n/g, " ") : null;
        let car = new AddCar(null,modelId, year, color, carPrice, carRentalPrice, description, UserKey.UserId);
        let addCarResponse = await ApiServices.addCar(car);
        if (addCarResponse.Status == 200) {
            showSucessfulLogo("your vehicle has been added");
            let responseImagesCar = await uploadCarImages(fileInput, addCarResponse.Data.carId);
            console.log(responseImagesCar);
        }

    }


});







//1885