import { formattedDate } from "../functions/DateFormate.js";
export async function loadingVInfo() {
    return fetch("../../pageshelper/vinfo.html")
        .then(response => response.text())
        .then(data => {
            document.getElementById("VInfoContainer").innerHTML = data;
        });
}


export function showVInfo(order) {


    function fillVecValue() {
        NoteOrder.value = order.note;
        OrderDate.textContent = order.orderDate;
        OrderUpdate.textContent = order.updatedAt ? "Updated " + order.updatedAt : "Not updated yet";

     
        OwnerValue.textContent = order.car.user.firstName + " " + order.car.user.lastName;
        UserPhoneValue.textContent = order.car.user.phoneNumber;
        EmailValue.textContent = order.car.user.email;
        AddressValue.textContent = order.car.user.address;
        CountryValue.textContent = order.car.user.countryName;

        PriceValue.textContent = order.car.carPrice + " USD";
        RentalPriceValue.textContent = order.car.carRentalPrice ? order.car.carRentalPrice + " USD" : "-";
        MakeValue.textContent = order.car.make;
        ModelValue.textContent = order.car.model;
        YearValue.textContent = order.car.year;
        ColorValue.textContent = order.car.color;
        VStatus.innerHTML = `<i class="fa-solid fa-circle MainColor"></i> ${order.car.status === 0 ? "Active" : "Inactive"}`;
    }
    const vInfoBox = document.getElementById("VInfoBox");
    const VInfoBox = document.getElementById("VInfoBox");
    const WrapperV = document.getElementById("WrapperV");
    const CarImages = document.getElementById("CarImages");
    const VStatus = document.getElementById("VStatus");
    const NoteOrder = document.getElementById("NoteOrder");
    const OrderDate = document.getElementById("OrderDate");
    const OrderUpdate = document.getElementById("OrderUpdate");
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
    fillVecValue();
    vInfoBox.classList.remove("Hidden");

}

export function hideLoading() {
    const vInfoBox = document.getElementById("VInfoBox");
    vInfoBox.classList.add("Hidden")

}