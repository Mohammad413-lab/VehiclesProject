
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
import { UserKey } from "../static/User.js";
import { showRequestBox } from "./requestbox.js";

export const carUl = document.getElementById("CarUl");
apiState.ulState = carUl;
const carSuggUl = document.getElementById("SuggUl");

await loadingNotFound();
export let userCart = await ApiServices.getUserCart(UserKey.UserId);
export const userRequest = await ApiServices.getMyRequesyVechileId();
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

if (carSuggUl) {
    let isDown = false;
    let startX;
    let scrollLeft;

    carSuggUl.addEventListener('mousedown', (e) => {
        isDown = true;
        startX = e.pageX - carSuggUl.offsetLeft;
        scrollLeft = carSuggUl.scrollLeft;
    });

    carSuggUl.addEventListener('mouseleave', () => {
        isDown = false;

    });

    carSuggUl.addEventListener('mouseup', () => {
        isDown = false;
    });

    carSuggUl.addEventListener('mousemove', (e) => {
        if (!isDown) return;
        e.preventDefault();
        const x = e.pageX - carSuggUl.offsetLeft;
        const walk = (x - startX) * 1.5;
        carSuggUl.scrollLeft = scrollLeft - walk;
    });
}
const liInformation = `

 
   <div class="LiContainer">
   <div id="OverLayBack" class="OverlayBack Hidden" ><p class="OverLayParagraph"> Requested <i class="fa-solid fa-hand"></i> </p></div>
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
          <button id="AddToCartButton" class="HomeButtonAddTo GreenColor"><i class="fas fa-warehouse"></i>
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

export async function pushSideBar(list, bool) {
    await ActiveSideBar(list, carUlList, carUl, bool);
}




export function loadingCarUlSugg(list) {

    showUlList(list, carUlList, carSuggUl);

}

export function loadingLocalSearchedNavBar(list) {
    localSearchNavBar(carUlList, carUl, list);
}

function toggleClass(button, isTrue) {
    if (isTrue) {
        button.classList.remove("GreenColor");
        button.classList.add("AddColor");

    } else {
        button.classList.remove("AddColor");
        button.classList.add("GreenColor");
    }


}


export function carUlList(car, carU) {
    const li = document.createElement("li");
    li.innerHTML = liInformation;
    const carImages = li.querySelector("#CarImages");
    const rentalPrice = li.querySelector("#RentalPriceValue");
    const price = li.querySelector("#PriceValue");
    const viewButton = li.querySelector("#ViewButton");
    const addToCartButton = li.querySelector("#AddToCartButton");
    const chatButton = li.querySelector("#ChatButton");
    const swiperContainer = li.querySelector("#SwiperContainer");
    const swiperWrapper = li.querySelector("#SwiperWrapper");
    const modelName = li.querySelector("#ModelNameValue");
    const carYear = li.querySelector("#CarYearValue");
    const carRequestButton = li.querySelector("#CarRequestButton");
    const overLay = li.querySelector("#OverLayBack");

    if (userRequest.includes(car.carID)) {
        toggleOverLay(overLay);
        viewButton.classList.add("Hidden");
        addToCartButton.classList.add("Hidden");
        chatButton.classList.add("Hidden");
        carRequestButton.classList.add("Hidden");
    }



    if (userCartMap[car.carID]) {
        toggleClass(addToCartButton, true);
    }
    eventListener(viewButton, function () {
        sessionStorage.setItem("carData", JSON.stringify(car));
        location.replace("../mainpages/viewcar.html");
    });
    eventListenerAsync(addToCartButton, async function () {

        if (userCartMap[car.carID]) {
            toggleClass(addToCartButton, false);
            let removeCarFromCart = await ApiServices.RemoveCart(car.carID);
            if (removeCarFromCart.status == true) {
                userCartMap[car.carID] = false;

            } else {
                toggleClass(addToCartButton, true); 
            }
        } else {
            toggleClass(addToCartButton, true);
            let response = await ApiServices.addCarToCart(car.carID);

            if (response.status == true) {
                userCartMap[car.carID] = true;
            } else { toggleClass(addToCartButton, false); }

        }
    })
    eventListenerAsync(chatButton, async () => showChatLogo(car.user));
    eventListenerAsync(carRequestButton, async () => await showRequestBox(car,overLay));
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



    carU.appendChild(li)
}


function toggleOverLay(overLay) {
    overLay.classList.remove("Hidden");
    overLay.classList.add("Flex");
}






