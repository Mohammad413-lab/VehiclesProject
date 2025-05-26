
import { showUlList } from "../functions/ShowUlList.js";
import { imageUrl } from "../static/ApiKeys.js";
import { eventListener, eventListenerAsync } from "../functions/EventListener.js"
import { ApiServices } from "../apiservices/ApiServices.js";
import { showChatLogo } from "./sendmessage.js";
import { createImage } from "../functions/CreateImageSlider.js";
import { ActiveSideBar } from "./sidebar.js";
import { searchCarsFromServer } from "./navbar.js";
import { loadingNotFound } from "./nocarsfound.js";
import { apiState } from "../static/apistate.js";
import { localSearchNavBar } from "./navbar.js";

export const requestUl = document.getElementById("CarUl");

await loadingNotFound();
export let userCart = await ApiServices.getUserCart(user.userID);
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
   <div class="LiContainer">
     
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

        
        <div style="display:flex;" >
          <button id="AddToCartButton" class="HomeButtonAddTo MainColor">add <i class="fas fa-warehouse"></i>
                    </button>
            <button id="CarRequestButton" class="HomeButtonOrder">request <i class="fa-solid fa-hand"></i>
                    </button>
                     <button id="ChatButton" class="HomeButtonChat">chat    <i class="fa-solid fa-comment"></i>
                    </button>
            <button id="ViewButton" class="HomeButton">view <i
                    class="fa-solid fa-eye"></i>
                    </button>
        </div>
          



    </div>

`;
export async function loadingCarUl(list) {

    showUlList(list, carUlList, carUl);

    
}







export function requestUlList(car, reqUl) {
    const li = document.createElement("li");
    li.innerHTML = liInformation;
    const carImages = li.querySelector("#CarImages");
    const rentalPrice = li.querySelector("#RentalPriceValue");
    const price = li.querySelector("#PriceValue");
    const viewButton = li.querySelector("#ViewButton");
    const chatButton = li.querySelector("#ChatButton");
    const swiperContainer = li.querySelector("#SwiperContainer");
    const swiperWrapper= li.querySelector("#SwiperWrapper");
    const modelName = li.querySelector("#ModelNameValue");
    const carYear = li.querySelector("#CarYearValue");

  
  

    eventListenerAsync(chatButton, async () => showChatLogo(car.user));
    modelName.textContent = car.model;
    carYear.textContent = car.year;
    price.textContent = car.carPrice ?? "No Price";
    rentalPrice.textContent = car.carRentalPrice ?? "No rental";
    if (car == null || car.imagesPath == null || car.imagesPath.length == 0) {
        createImage(swiperContainer, "../../assets/images/carEmptyImage.jpg", false);
    } else {
        for (let image of car.imagesPath) {
            createImage(swiperWrapper, image, true);
            
        }

    }



    reqUl.appendChild(li)
}







