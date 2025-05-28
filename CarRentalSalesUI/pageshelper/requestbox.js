import { createImage } from "../functions/CreateImageSlider.js";
import { eventListener, eventListenerAsync } from "../functions/EventListener.js";
import { RequestSale } from "../dtos/request/requestSale.js";
import { ApiServices } from "../apiservices/ApiServices.js";
import { UserKey } from "../static/User.js";
import { showSucessfulLogo } from "./sendmessage.js";
export async function loadingRequestBox() {
    return fetch("../../pageshelper/requestbox.html")
        .then(response => response.text())
        .then(data => {
            document.getElementById("RequestBox").innerHTML = data;
        });
}


export async function showRequestBox(vehicle) {

    function fillBoxInformation() {
        WrapperV.innerHTML = '';
        OwnerValue.textContent = vehicle.user.firstName + " " + vehicle.user.lastName;
        UserPhoneValue.textContent = vehicle.user.phoneNumber;
        EmailValue.textContent = vehicle.user.email;
        AddressValue.textContent = vehicle.user.address;
        CountryValue.textContent = vehicle.user.countryName;
        PriceValue.textContent = vehicle.carPrice;
        RentalPriceValue.textContent = vehicle.carRentalPrice ? vehicle.carRentalPrice : "-";
        MakeValue.textContent = vehicle.make;
        ModelValue.textContent = vehicle.model;
        YearValue.textContent = vehicle.year;
        ColorValue.textContent = vehicle.color;
        if (vehicle.imagesPath.length == 0) {
            createImage(WrapperV, "../../assets/images/carEmptyImage.jpg", false);
        } else {
            for (let image of vehicle.imagesPath) {
                createImage(WrapperV, image, true);
            }
        }
    }

    const WrapperV = document.getElementById("WrapperV");
    const CarImages = document.getElementById("CarImages");
    const VStatus = document.getElementById("VStatus");
    const NoteOrder = document.getElementById("NoteOrderReq");
    const OwnerValue = document.getElementById("OwnerValue");
    const UserPhoneValue = document.getElementById("UserPhoneValue");
    const EmailValue = document.getElementById("EmailValue");
    const AddressValue = document.getElementById("AddressValue");
    const CountryValue = document.getElementById("CountryValue");
    const PriceValue = document.getElementById("PriceValue");
    const RentalPriceWord = document.getElementById("RentalPriceWord");
    const RentalPriceValue = document.getElementById("RentalPriceValue");
    const MakeValue = document.getElementById("MakeValue");
    const ModelValue = document.getElementById("ModelValue");
    const YearValue = document.getElementById("YearValue");
    const ColorValue = document.getElementById("ColorValue");
    const ExitButton = document.getElementById("ExitButtonReq");
    const requestNowButton = document.getElementById("RequestNow");
    const vRequest = document.getElementById("VRequest");
    vRequest.classList.remove("Hidden");
    eventListener(ExitButton, () => hideRequestBox());
    eventListenerAsync(requestNowButton, async function () {
        let selectedValue = document.querySelector('input[name="OrderRequest"]:checked')?.value;
        if (selectedValue == 0) {
            await requestSale(vehicle,NoteOrder,selectedValue);
        }


    })
    fillBoxInformation();


}

export function hideRequestBox() {
    const vRequest = document.getElementById("VRequest");
    vRequest.classList.add("Hidden");
}

async function requestSale(vehicle, note, selectedValue) {

    if (selectedValue == 0 && vehicle.carPrice != null) {
        let request = new RequestSale(vehicle.carID, UserKey.UserId, vehicle.carPrice, note.value == "" ? null : note.value);
        console.log(request);
       let response = await ApiServices.addOrderSale(request);
        if (response.Ok) {
            showSucessfulLogo(response.Data.message);
            note.value='';
            hideRequestBox();

        }
    }

}