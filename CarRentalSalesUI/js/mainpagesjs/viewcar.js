import { imageUrl } from "../../static/ApiKeys.js";
import { showChatLogo, loadingSendMessageContent, loadingSucessLogo, showSucessfulLogo } from "../../pageshelper/sendmessage.js";
import { loadingContent, showLoading, hideLoading } from "../../pageshelper/loading.js";
import { loadingCarUl, loadingCarUlSugg } from "../../pageshelper/carul.js";
import { ApiServices } from "../../apiservices/ApiServices.js";
import { Car } from "../../models/car.js";
import { User } from "../../models/user.js";
import { eventListener, eventListenerAsync } from "../../functions/EventListener.js";
import { createImage } from "../../functions/CreateImageSlider.js";

function fillFullCarInformation() {
    ownerValue.textContent = `${choosedCar.user.firstName} ${choosedCar.user.lastName}`;
    emailValue.textContent = choosedCar.user.email;
    userPhoneValue.textContent = choosedCar.user.phoneNumber;
    addressValue.textContent = choosedCar.user.address;
    countryValue.textContent = choosedCar.user.countryName;
    makeValue.textContent = choosedCar.make;
    modelValue.textContent = choosedCar.model;
    yearValue.textContent = choosedCar.year;
    priceValue.textContent = choosedCar.carPrice ?? "Not for sale";
    rentalPriceValue.textContent = choosedCar.carRentalPrice ?? "No rental";
    colorValue.textContent = choosedCar.color;
    carDescription.textContent = choosedCar.description;
}

let choosedCar = JSON.parse(sessionStorage.getItem("carData"));

console.log(choosedCar);
await loadingSucessLogo();
await loadingSendMessageContent();
await loadingContent();
showLoading();
let countryCars = await ApiServices.getCountryCars(1);
let cars = [];
let userCars = await ApiServices.getAllUserCars(choosedCar.user.userID);
let allUserCars = [];


if (userCars.length > 0) {
    userCars.forEach(carFromJson => {
        if (carFromJson.carID != choosedCar.carID) {
            let car = new Car();
            car.fillOnlyCarFromjson(carFromJson);

            car.user = new User(choosedCar.user.userID, choosedCar.user.firstName, choosedCar.user.lastName, choosedCar.user.email, choosedCar.user.phoneNumber, choosedCar.user.countryName, choosedCar.user.countryId, choosedCar.user.address);
            allUserCars.push(car);
        }

    });
}



countryCars.forEach(carFromJson => {
    if (carFromJson.carID != choosedCar.carID) {
        let car = new Car();
        car.fillCarFromjson(carFromJson);
        cars.push(car);
    }

});

hideLoading();



const ownerValue = document.getElementById("OwnerValue");
const userPhoneValue = document.getElementById("UserPhoneValue");
const emailValue = document.getElementById("EmailValue");
const addressValue = document.getElementById("AddressValue");
const countryValue = document.getElementById("CountryValue");
const priceValue = document.getElementById("PriceValue");
const rentalPriceWord = document.getElementById("RentalPriceWord");
const rentalPriceValue = document.getElementById("RentalPriceValue");
const makeValue = document.getElementById("MakeValue");
const modelValue = document.getElementById("ModelValue");
const yearValue = document.getElementById("YearValue");
const colorValue = document.getElementById("ColorValue");
const carDescription = document.getElementById("DescriptionValue");
const addToCartButton = document.getElementById("AddToCartButton");
const carOrderButton = document.getElementById("CarOrderButton");
const chatButton = document.getElementById("ChatButton");
eventListenerAsync(chatButton, async function () {
    await showChatLogo(choosedCar.user);
});
const ownerCarWord = document.getElementById("OwnerCar");
if (allUserCars.length > 0) {
    ownerCarWord.style.display = "flex";
    loadingCarUl(allUserCars);
}
fillFullCarInformation();

let wrapper = document.getElementById("Wrapper");
if (choosedCar.imagesPath.length > 0) {
    choosedCar.imagesPath.forEach(image => {
        createImage(wrapper, image, true);
    });
} else {
    createImage(wrapper, "../../assets/images/carEmptyImage.jpg");
}


const swiper = new Swiper('.swiper-container', {
    loop: false,
    spaceBetween: 2,
    slidesPerView: 1,

    pagination: {
        el: '.swiper-pagination',
        clickable: true,
    },
});

