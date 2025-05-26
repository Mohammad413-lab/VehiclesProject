
import {
    UserKey
} from "../static/User.js";
import { showUlList } from "../functions/ShowUlList.js";
import { createImage } from "../functions/CreateImageSlider.js";
import { loadingContent, showLoading, hideLoading } from "./loading.js";
import { eventListener, eventListenerAsync } from "../functions/EventListener.js";
import { ApiServices } from "../apiservices/ApiServices.js";
import { loadingSucessLogo, showSucessfulLogo } from "./sendmessage.js";
import { imageUrl } from "../static/ApiKeys.js";
import { userModels } from "../js/mainpagesjs/mycars.js";
import { fillSelectedOption } from "../functions/FillSelectedOptions.js";
import { AddCar } from "../dtos/request/addcardtos.js";
import { uploadCarImages } from "../functions/UploadImagesCar.js";
import { showErrorText, checkInputYear } from "../js/mainpagesjs/mycars.js";
import { loadingViewMyCar } from "./viewmycar.js";
import { showAreYouSure, loadingAreYouSure } from "./areyousure.js";
import { ActiveSideBar } from "./sidebar.js";
import { localSearchNavBar } from "./navbar.js";
import { loadingNotFound } from "./nocarsfound.js";
import { loadingUserProfile

 } from "./userprofile.js";

await loadingContent();
await loadingSucessLogo();
await loadingViewMyCar();
await loadingAreYouSure();
await loadingNotFound();
await loadingUserProfile();
console.log(userModels);

showLoading();
export function localSearchForUser(list) {
    localSearchNavBar(MyCarLi, myCarUl, list);
}
async function showEditCarBox(car, bool) {
    if (bool) {
        editCarBox.classList.remove("Hidden");
        await fillEditBox(car);
    } else {
        editCarBox.classList.add("Hidden");
    }

}

async function createImageEdit(image, bool, e, file) {

    let container = document.createElement("div");
    container.classList.add("ImageContainer");
    let overlay = document.createElement("div");
    overlay.classList.add("ImageOverlay");
    let img = document.createElement("img");
    img.classList.add("CarImageArea");
    img.src = bool ? imageUrl + image.imagePath : e.target.result;

    eventListenerAsync(container, async function () {
        if (bool) {
            let response = await ApiServices.removeCarImage(image.imageId, image.imagePath);
            if (response.Ok) {
                showSucessfulLogo(response.Data.message);

            }
        } else {
            let index = imageFileList.indexOf(file);
            imageFileList.splice(index, 1);
        }
        container.remove();
    })
    let i = document.createElement("i");
    i.classList.add("fa-solid", "fa-square-minus");
    overlay.appendChild(i);
    container.appendChild(img);
    container.appendChild(overlay);
    imagesArea.appendChild(container);

}

async function loadImageFromFile() {
    const files = fileInputEdit.files;
    Array.from(files).forEach(file => {
        const reader = new FileReader();
        reader.onload = async e => {
            await createImageEdit(null, false, e, file);
            imageFileList.push(file);
        }
        reader.readAsDataURL(file);
    });
    console.log("this is files");
    console.log(imageFileList);
}

async function fillEditBox(car) {
    for (let model of userModels) {
        if (model.modelName == car.model) {
            disabledOption.value = model.modelId
        }
    }
    console.log("this is modelId for this car")
    console.log(selectedModelEdit.value);
    inputPriceEdit.value = car.carPrice ?? "";
    inputRentalPriceEdit.value = car.carRentalPrice ?? "";
    inputColorEdit.value = car.color ?? "";
    inputYearEdit.value = car.year ?? "";
    descriptionInputEdit.value = car.description ?? "";
    disabledOption.textContent = car.model;
    imagesArea.innerHTML = "";
    let carImages = await ApiServices.getCarImages(car.carID);
    carImages.forEach(async image => {
        await createImageEdit(image, true, null, null);
    });
}

const liMyCar = `
   <div class="LiContainer" >
     
<div class="swiper-container">
    <div id="SwiperContainer" class="swiper-wrapper" >
       
      
    </div>
   
    <div class="swiper-pagination"></div>
</div>
      
         <div class="DivRow">
           <div class="PriceBackground">
            <p id="ModelName" class="CarInfoDesign">Model <i class="fa-solid fa-check-to-slot"></i> </p>
            <p id="ModelNameValue" class="CarInfoDesignValue">--</p>
            </div>
               <div class="PriceBackground">
            <p id="CarYear" class="CarInfoDesign">Year  <i class="fa-solid fa-check-to-slot"></i></p>
            <p id="CarYearValue" class="CarInfoDesignValue">--</p>
            </div>
        </div>
      
        <div class="DivRow">
         
           <div class="PriceBackground">
                <p class="CarInfoDesign">Price <i class="fa-solid fa-tag "></i></p>
            <p id="PriceValue" class="CarInfoDesignValue">--</p>
           </div>
          
      
            <div class="PriceBackground">
            <p id="RentalPriceWord" class="CarInfoDesign">Rental price <i class="fa-solid fa-tag "></i> </p>
            <p id="RentalPriceValue" class="CarInfoDesignValue">--</p>
            </div>
            
         
        </div>
        <div style="display:flex;" >
          <button id="EditCarButton" class="HomeButtonAddTo MainColor">edit <i class="fa-solid fa-upload"></i>
                    </button>
            <button id="RemoveCarButton" class="HomeButtonOrder">remove <i class="fa-regular fa-square-minus"></i>
                    </button>
                 
            <button id="ViewButton" class="HomeButton">view <i
                    class="fa-solid fa-eye"></i>
                    </button>
        </div>
          



    </div>

`;

const myCarUl = document.getElementById("MyCarUl");
let userCars = await ApiServices.getAllUserCars(UserKey.UserId);
hideLoading();

const inputPriceEdit = document.getElementById("InputPriceEdit");
const inputRentalPriceEdit = document.getElementById("InputRentalPriceEdit");
const warningInputPriceOrReental = document.getElementById("WarningInputPriceOrReental");
const inputColorEdit = document.getElementById("InputColorEdit");
const warningInputColorEdit = document.getElementById("WarningInputColorEdit");
const inputYearEdit = document.getElementById("InputYearEdit");
const warningInputYearEdit = document.getElementById("WarningInputYearEdit");
const fileInputEdit = document.getElementById("fileInputEdit");
const selectedModelEdit = document.getElementById("SelectedModelEdit");
const saveEditCarButton = document.getElementById("SaveEditCarButton");
const descriptionInputEdit = document.getElementById("DescriptionInputEdit");
const imagesArea = document.getElementById("ImagesArea");
const editCarBox = document.getElementById("EditCarBox");
const disabledOption = document.getElementById("DisabledOption");
const selectedModel = document.getElementById("SelectedModelEdit");
const closeEditBox = document.getElementById("CloseEditBox");
let choosedCarId = null;
let imageFileList = [];

fileInputEdit.addEventListener("change", async () => await loadImageFromFile());
eventListenerAsync(saveEditCarButton, async function () {

    let checkInputPriceAndRental = showErrorText("you need to type your price or rental price", warningInputPriceOrReental, (inputRentalPriceEdit.value != null || inputPriceEdit.value != null));
    let checkColor = showErrorText("please type car color", warningInputColorEdit, inputColorEdit.value != null);
    let checkYear = checkInputYear(inputYearEdit.value, warningInputYearEdit);
    let price = inputPriceEdit != '' ? inputPriceEdit.value : null;
    let rental = inputRentalPriceEdit != '' ? inputRentalPriceEdit.value : null;
    let description = descriptionInputEdit.value != '' ? descriptionInputEdit.value : null;

    if (checkInputPriceAndRental && checkColor && checkYear) {
        showLoading();
        let car = new AddCar(choosedCarId, selectedModel.value, inputYearEdit.value, inputColorEdit.value
            , price, rental, description, null);
        console.log("this is car desc");
        console.log(car.description);
        let editCarResponse = await ApiServices.editCar(car);
        console.log(editCarResponse);
        if (editCarResponse.Ok) {
            showSucessfulLogo(editCarResponse.Data.message);
            if (imageFileList.length > 0) {
                let responseUpdateImages = await uploadCarImages(imageFileList, car.carID);
                console.log(responseUpdateImages);
            }

        }

        imageFileList.length = 0;
        hideLoading();
        showEditCarBox(null, false);
    }



});
eventListener(closeEditBox, function () {
    showEditCarBox(null, false);
});

userModels.forEach(model => {
    fillSelectedOption(selectedModel, model.modelId, model.modelName);
});
function MyCarLi(car) {
    let li = document.createElement('li');
    li.innerHTML = liMyCar;
    const carImages = li.querySelector("#CarImages");
    const rentalPrice = li.querySelector("#RentalPriceValue");
    const price = li.querySelector("#PriceValue");
    const editCarButton = li.querySelector("#EditCarButton");
    const viewButton = li.querySelector("#ViewButton");
    const removeCarButton = li.querySelector("#RemoveCarButton");
    const modelName = li.querySelector("#ModelNameValue");
    const carYear = li.querySelector("#CarYearValue");
    const swiperContainer = li.querySelector("#SwiperContainer");

    if (car) {
        modelName.textContent = car.model;
        carYear.textContent = car.year;
        price.textContent = car.carPrice ?? "No Price";
        rentalPrice.textContent = car.carRentalPrice ?? "No rental";
        if (car.imagesPath.length == 0) {
            createImage(swiperContainer, "../../assets/images/carEmptyImage.jpg", false);
        } else {
            for (let image of car.imagesPath) {
                createImage(swiperContainer, image, true);
            }
        }
        eventListenerAsync(removeCarButton, async function () {

            if (await showAreYouSure()) {
                let removeCarResponse = await ApiServices.removeMyCar(car.carID);
                if (removeCarResponse.Ok) {
                    li.remove();
                    showSucessfulLogo("Your car has been removed");

                }
            }

        });

        eventListener(editCarButton, function () {
            showEditCarBox(car, true);
            choosedCarId = car.carID;

        });

        eventListener(viewButton, function () {


        });
    }

    myCarUl.appendChild(li);

}


showUlList(userCars, MyCarLi, myCarUl);
localSearchForUser(userCars);
await ActiveSideBar(userCars, MyCarLi, myCarUl, true);

console
.log(userCars)


const swiper2 = new Swiper('.swiper-container', {
    loop: false,
    spaceBetween: 2,
    slidesPerView: 1,

    pagination: {
        el: '.swiper-pagination',
        clickable: true,
    },
});