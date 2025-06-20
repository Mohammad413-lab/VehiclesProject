
import { showUlList } from "../functions/ShowUlList.js";
import { eventListener, eventListenerAsync } from "../functions/EventListener.js"
import { ApiServices } from "../apiservices/ApiServices.js";
import { showChatLogo, showSucessfulLogo } from "./sendmessage.js";
import { createImage } from "../functions/CreateImageSlider.js";
import { loadingNotFound } from "./nocarsfound.js";
import { UserKey } from "../static/User.js";
import { showVInfo } from "./vinfo.js";
import { RequestStatus, changeStatusColor,statusInfoForRequest } from "../static/Status.js";
import { ActiveSideBarForRequest } from "./sidebar.js";
import { localSearchNavBarReq } from "./navbar.js";
import { loadSwiper } from "../functions/Swiper.js";



export const requestUl = document.getElementById("MyCarUlReq");

await loadingNotFound();
export let userCart = await ApiServices.getUserCart(UserKey.UserId
);
let userCartMap = {};
if (Array.isArray(userCart)) {
    userCart.forEach(element => {
        userCartMap[element.carID] = true;
    });
}
const forMe = `  <div class="swiper-slide">
            <img id="CarImages" src="../assets/images/jocar.png" alt="Car Image 1">
        </div>`;

console.log(userCartMap);

const liInformation = `
   <div class="LiContainerReq" >

   <div class="DivRow">
     <p id="ReqStatus"><i class="fa-solid fa-font-awesome"></i> Active</p>
     <p id="ReqInfo" class="CarInfoDesign" style="margin-top:19px;"> Active</p>
      </div>
<div class="swiper-container"  id="SwiperContainer">
    <div class="swiper-wrapper" id="SwiperWrapper" >
       
      
    </div>
   
    <div class="swiper-pagination"></div>
</div>


        <div class="DivRow">
      
           <div class="PriceBackground">
               <p id="ModelName" class="CarInfoDesign">Model <i class="fa-solid fa-check-to-slot MainColor"></i> </p>
            <p id="ModelNameValue" class="CarInfoDesignValue">--</p>
            </div>
                  
            <div class="PriceBackground">
             <p id="CarYear" class="CarInfoDesign"  >Year  <i class="fa-solid fa-check-to-slot MainColor"></i></p>
            <p id="CarYearValue" class="CarInfoDesignValue" >--</p>
            </div>
        </div>
      
        <div class="DivRow">
         
           <div class="PriceBackground">
                <p class="CarInfoDesign">Price <i class="fa-solid fa-tag  MainColor"></i></p>
            <p id="PriceValue" class="CarInfoDesignValue">--</p>
           </div>
          
      
            <div class="PriceBackground">
            <p id="RentalPriceWord" class="CarInfoDesign">Rental price <i class="fa-solid fa-tag  MainColor"></i> </p>
            <p id="RentalPriceValue" class="CarInfoDesignValue">--</p>
            </div>
            
         
        </div>
         <div class="Line"></div>
        
        <div style="display:flex;" >
         
            <button id="CancelRequestButton" class="HomeButtonOrder">cancel request <i class="fa-solid fa-hand"></i>
                    </button>
                     <button id="ChatButton" class="HomeButtonChat">chat    <i class="fa-solid fa-comment"></i>
                    </button>
            <button id="ViewButton" class="HomeButton">view <i
                    class="fa-solid fa-eye"></i>
                    </button>
        </div>
          



    </div>

`;

const liInformationMyRequested = `
   <div class="LiContainerRequested" >
   <div class="DivRow">
     <p id="ReqStatus"><i class="fa-solid fa-font-awesome"></i> Active</p>
      <p id="ReqInfo" class="CarInfoDesign" style="margin-top:19px;"> Active</p>
      </div>
<div class="swiper-container"  id="SwiperContainer">
    <div class="swiper-wrapper" id="SwiperWrapper" >
       
      
    </div>
   
    <div class="swiper-pagination"></div>
</div>


        <div class="DivRow">
      
           <div class="PriceBackground">
               <p id="ModelName" class="CarInfoDesign">Model <i class="fa-solid fa-check-to-slot MainColor"></i> </p>
            <p id="ModelNameValue" class="CarInfoDesignValue">--</p>
            </div>
                  
            <div class="PriceBackground">
             <p id="CarYear" class="CarInfoDesign"  >Year  <i class="fa-solid fa-check-to-slot MainColor"></i></p>
            <p id="CarYearValue" class="CarInfoDesignValue" >--</p>
            </div>
        </div>
      
        <div class="DivRow">
         
           <div class="PriceBackground">
                <p class="CarInfoDesign">Price <i class="fa-solid fa-tag  MainColor"></i></p>
            <p id="PriceValue" class="CarInfoDesignValue">--</p>
           </div>
          
      
            <div class="PriceBackground">
            <p id="RentalPriceWord" class="CarInfoDesign">Rental price <i class="fa-solid fa-tag  MainColor"></i> </p>
            <p id="RentalPriceValue" class="CarInfoDesignValue">--</p>
            </div>
            
         
        </div>
        <div class="Line"></div>
           <div class="DivRow">
                    <button id="CarAcceptButton" class="HomeButtonAccept">Accept <i class="fa-solid fa-user-check"></i>
                    </button>
       <button id="CarHoldButton" class="HomeButtonHold">Hold <i class="fa-solid fa-circle-pause"></i></i>
                    </button>
                     <button id="CarCancelButton" class="HomeButtonCancel">Cancel <i class="fa-solid fa-ban"></i></i>
                    </button>
   </div>
            <div class="Line"></div>
        
        <div style="display:flex;" >
         

                     <button id="ChatButton" class="HomeButtonChat">chat    <i class="fa-solid fa-comment"></i>
                    </button>
            <button id="ViewButton" class="HomeButton">view <i
                    class="fa-solid fa-eye"></i>
                    </button>
        </div>
          



    </div>

`;
export async function loadingVecUl(list) {

    showUlList(list, requestUlList, requestUl);
    await loadSwiper();

}

export async function loadingVecRequestedUl(list) {

    showUlList(list, requestedVecUlList, requestUl);
    await loadSwiper();

}

export async function activeSideBarForRequest(orderList) {
    ActiveSideBarForRequest(orderList, requestUlList, requestUl);
}

export async function activeLocalNavBarRequestSearch(list) {
    localSearchNavBarReq(requestUlList, requestUl, list);
}






export function requestUlList(order, reqUl) {
    const li = document.createElement("li");
    li.innerHTML = liInformation;

    const rentalPrice = li.querySelector("#RentalPriceValue");
    const price = li.querySelector("#PriceValue");
    const viewButton = li.querySelector("#ViewButton");
    const chatButton = li.querySelector("#ChatButton");
    const swiperContainer = li.querySelector("#SwiperContainer");
    const cancelRequestButton=li.querySelector("#CancelRequestButton");
    const swiperWrapper = li.querySelector("#SwiperWrapper");
    const modelName = li.querySelector("#ModelNameValue");
    const carYear = li.querySelector("#CarYearValue");
    const requestStatus = li.querySelector("#ReqStatus");
    const reqInfo=li.querySelector("#ReqInfo");
    changeStatusColor(order.status, requestStatus);
      

    eventListener(viewButton, () => showVInfo(order,false));
    eventListenerAsync(chatButton, async () => showChatLogo(order.car.user));
    eventListenerAsync(cancelRequestButton,async function () {
        let cancelResponse=await ApiServices.cancelRequest(order.saleOrderId);
        if(cancelResponse.Ok){
            showSucessfulLogo(cancelResponse.Data.message);
            reqUl.removeChild(li);
        }
    })
    statusInfoForRequest(order.status,reqInfo,true);
    modelName.textContent = order.car.model;
    carYear.textContent = order.car.year;
    price.textContent = order.car.carPrice ?? "No Price";
    rentalPrice.textContent = order.car.carRentalPrice ?? "No rental";
    requestStatus.innerHTML = `<i class="fa-solid fa-font-awesome MainColor"></i> ${RequestStatus[order.status]}`;
    if (order == null || order.car.imagesPath == null || order.car.imagesPath.length == 0) {
        createImage(swiperWrapper, "../../assets/images/carEmptyImage.jpg", false);
    } else {
        for (let image of order.car.imagesPath) {
            createImage(swiperWrapper, image, true);

        }

    }



    reqUl.appendChild(li)
}

export function requestedVecUlList(order, reqUl) {
    const li = document.createElement("li");
    li.innerHTML = liInformationMyRequested;
    const acceptButton = li.querySelector("#CarAcceptButton");
    const rentalPrice = li.querySelector("#RentalPriceValue");
    const price = li.querySelector("#PriceValue");
    const viewButton = li.querySelector("#ViewButton");
    const chatButton = li.querySelector("#ChatButton");
    const swiperContainer = li.querySelector("#SwiperContainer");
    const swiperWrapper = li.querySelector("#SwiperWrapper");
    const modelName = li.querySelector("#ModelNameValue");
    const carYear = li.querySelector("#CarYearValue");
    const requestStatus = li.querySelector("#ReqStatus");
    const reqInfo=li.querySelector("#ReqInfo");
    changeStatusColor(order.status, requestStatus);


    eventListener(viewButton, () => showVInfo(order,true));
    eventListenerAsync(chatButton, async () => showChatLogo(order.car.user));
    eventListenerAsync(acceptButton,async function(){
        let response=await ApiServices.updateRequestStatus(order.saleOrderId,1);
        console.log(order)
        if(response.Ok){
             showSucessfulLogo("Accepted vechile for "+order.car.user.firstName +" " +order.car.user.lastName);
            }
       
    });

    statusInfoForRequest(order.status,reqInfo,false);
    modelName.textContent = order.car.model;
    carYear.textContent = order.car.year;
    price.textContent = order.car.carPrice ?? "No Price";
    rentalPrice.textContent = order.car.carRentalPrice ?? "No rental";
    requestStatus.innerHTML = `<i class="fa-solid fa-font-awesome MainColor"></i> ${RequestStatus[order.status]}`;
    if (order == null || order.car.imagesPath == null || order.car.imagesPath.length == 0) {
        createImage(swiperWrapper, "../../assets/images/carEmptyImage.jpg", false);
    } else {
        for (let image of order.car.imagesPath) {
            createImage(swiperWrapper, image, true);

        }

    }



    reqUl.appendChild(li)
}








