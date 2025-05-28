import { formattedDate } from "../functions/DateFormate.js";
import { createImage } from "../functions/CreateImageSlider.js";
import { eventListener } from "../functions/EventListener.js";
import { RequestStatus,changeStatusColor } from "../static/Status.js";
export async function loadingVInfo() {
    return fetch("../../pageshelper/vinfo.html")
        .then(response => response.text())
        .then(data => {
            document.getElementById("VInfoContainer").innerHTML = data;
        });
}


export function showVInfo(order) {


    function fillVecValue() {
        WrapperV.innerHTML='';
        NoteOrder.textContent=order.note??"No note";
        OrderDate.textContent = formattedDate(order.orderDate);
        OrderUpdate.textContent = order.updatedAt ?  formattedDate(order.updatedAt)  : "Not updated yet";
        OwnerValue.textContent = order.car.user.firstName + " " + order.car.user.lastName;
        UserPhoneValue.textContent = order.car.user.phoneNumber;
        EmailValue.textContent = order.car.user.email;
        AddressValue.textContent = order.car.user.address;
        CountryValue.textContent = order.car.user.countryName;
        PriceValue.textContent = order.car.carPrice ;
        RentalPriceValue.textContent = order.car.carRentalPrice ? order.car.carRentalPrice  : "-";
        MakeValue.textContent = order.car.make;
        ModelValue.textContent = order.car.model;
        YearValue.textContent = order.car.year;
        ColorValue.textContent = order.car.color;
        changeStatusColor(order.status,VStatus);
        VStatus.innerHTML = `<i class="fa-solid fa-circle MainColor"></i> ${RequestStatus[order.status]}`;
        if(order.car.imagesPath.length==0){
             createImage(WrapperV,"../../assets/images/carEmptyImage.jpg",false);
        }else{
            for(let image of order.car.imagesPath){
                createImage(WrapperV,image,true);
            }
        }
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
    const CloseButton=document.getElementById("CloseButton");
    fillVecValue();
    vInfoBox.classList.remove("Hidden");
    eventListener(CloseButton,()=>vInfoBox.classList.add("Hidden"));

}

export function hideLoading() {
    const vInfoBox = document.getElementById("VInfoBox");
    vInfoBox.classList.add("Hidden")

}