
import { ApiServices } from "../../apiservices/ApiServices.js";
import { showUlList } from "../../functions/ShowUlList.js";
import { loadingContent, showLoading, hideLoading } from "../../pageshelper/loading.js";
import { Car } from "../../models/car.js";
import { loadingSucessLogo,loadingSendMessageContent } from "../../pageshelper/sendmessage.js";
import { loadingCarUl } from "../../pageshelper/carul.js";
import { loadingSideBar } from "../../pageshelper/sidebar.js";
import { refreshApiState,apiState } from "../../static/apistate.js";
import { loadingNoMore } from "../../pageshelper/nomorecars.js";
import { loadingList
 } from "../../pageshelper/loadinglist.js";
import { searchCarsFromServer } from "../../pageshelper/navbar.js";
import { carUlList,carUl } from "../../pageshelper/carul.js";
import { pushSideBar } from "../../pageshelper/carul.js";
import { loadingUserProfile } from "../../pageshelper/userprofile.js";
import { loadingRequestBox } from "../../pageshelper/requestbox.js";

refreshApiState();
apiState.currentApiType=0;
await loadingList();
await loadingNoMore();
await loadingContent();
await loadingSideBar();
await loadingSendMessageContent();
await loadingSucessLogo();
await loadingUserProfile();
await loadingRequestBox();

await pushSideBar();
await searchCarsFromServer(carUlList, carUl, false);
showLoading();
let token = sessionStorage.getItem("token");


let cars = [];

apiState.listState = await ApiServices.getCountryCars(1);

apiState.listState.forEach(carFromJson => {
    let car = new Car();
    car.fillCarFromjson(carFromJson);
    cars.push(car);
    
});
console.log(apiState.listState);

hideLoading();





 await loadingCarUl(cars);

const swiper = new Swiper('.swiper-container', {
    loop: false, 
    spaceBetween: 2, 
    slidesPerView: 1,

    pagination: {
        el: '.swiper-pagination', 
        clickable: true, 
    },
});

