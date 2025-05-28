import { eventListenerAsync,eventListener } from "../functions/EventListener.js";
import { showUlList } from "../functions/ShowUlList.js";
import { ApiServices } from "../apiservices/ApiServices.js";
import { apiState, refreshApiState } from "../static/apistate.js";
import { refreshSwiper, swiper } from "../functions/Swiper.js";
import { showNoMore } from "./nomorecars.js";
import { showUserProfile } from "./userprofile.js";


export async function loadingSideBar() {
    return fetch("../../pageshelper/sidebar.html")
        .then(response => response.text())
        .then(data => {
            document.getElementById("SideBarContainer").innerHTML = data;
        });
}


export async function ActiveSideBar(list, liFunc, ul, isCarUser) {

    const profileButton=document.getElementById("ProfileButton");
    const allButton = document.getElementById("AllButton");
    const newestButton = document.getElementById("Newest");
    const oldestButton = document.getElementById("Oldest");
    const saleButton = document.getElementById("SaleButton");
    const rentalButton = document.getElementById("RentalButton");
    eventListenerAsync(newestButton, async function () {
        apiState.isLoading = true;
        await Sorted(list, liFunc, ul, isCarUser, 0);
        apiState.isLoading = false;
    });
    eventListenerAsync(oldestButton, async function () {
        showNoMore('',false);
        apiState.isLoading = true;
        await Sorted(list, liFunc, ul, isCarUser, 1);
        apiState.isLoading = false;
    });
    eventListenerAsync(saleButton, async function () {
        showNoMore('',false);
        apiState.isLoading = true;
        await Sorted(list, liFunc, ul, isCarUser, 2);
        apiState.isLoading = false;
    });
    eventListenerAsync(rentalButton, async function () {
        showNoMore('',false);
        apiState.isLoading = true;
        await Sorted(list, liFunc, ul, isCarUser, 3);
        apiState.isLoading = false;
    });

    eventListenerAsync(allButton, async function () {
        showNoMore('',false);
        apiState.isLoading = true;
        await Sorted(list, liFunc, ul, isCarUser, 4);
        apiState.isLoading = false;
    });

    eventListener(profileButton,()=>showUserProfile());
        


}
export async function ActiveSideBarForRequest(list, liFunc, ul) {

    const profileButton=document.getElementById("ProfileButton");
    const allButton = document.getElementById("AllButton");
    const newestButton = document.getElementById("Newest");
    const oldestButton = document.getElementById("Oldest");
    const saleButton = document.getElementById("SaleButton");
    const rentalButton = document.getElementById("RentalButton");
    eventListenerAsync(newestButton, async function () {
        apiState.isLoading = true;
         SortedOrderRequest(list, liFunc, ul,  0);
        apiState.isLoading = false;
    });
    eventListenerAsync(oldestButton, async function () {
        showNoMore('',false);
        SortedOrderRequest(list, liFunc, ul,  1);
   
    });
    eventListenerAsync(saleButton, async function () {
       
        showNoMore('',false);
        SortedOrderRequest(list, liFunc, ul, 2);
   
    });
    eventListenerAsync(rentalButton, async function () {
        showNoMore('',false);
        SortedOrderRequest(list, liFunc, ul,3);
      
    });

    eventListenerAsync(allButton, async function () {
        showNoMore('',false);
          SortedOrderRequest(list, liFunc, ul,  4);
    });

    eventListener(profileButton,()=>showUserProfile());
        


}
 async function Sorted(list, liFunc, ul, isCarUser, forButton) {
    refreshApiState();
    let filterList = [];
    ul.innerHTML = '';
    let carsResponse = null;
    if (!isCarUser) {
        switch (forButton) {
            case 0: apiState.currentApiType = 2;

                carsResponse = await ApiServices.getCarsSortedByYear(1);
                apiState.listState = Array.isArray(carsResponse.Data) ? carsResponse.Data : [];
                break;
            case 1: apiState.currentApiType = 3;
                carsResponse = await ApiServices.getCarsSortedByOldest(1);
                apiState.listState = Array.isArray(carsResponse.Data) ? carsResponse.Data : [];
                break;
            case 2:
                apiState.currentApiType = 4;
                carsResponse = await ApiServices.getCarsForSale(1);
                apiState.listState = Array.isArray(carsResponse.Data) ? carsResponse.Data : [];
                break;
            case 3: apiState.currentApiType = 5;
                carsResponse = await ApiServices.getCarsForRental(1);
                apiState.listState = Array.isArray(carsResponse.Data) ? carsResponse.Data : [];
                break;
            case 4: apiState.currentApiType = 0;
                carsResponse = await ApiServices.getCountryCars(1);
                apiState.listState = Array.isArray(carsResponse.Data) ? carsResponse.Data : [];
                break;
        }

        if (Array.isArray(carsResponse.Data) || Array.isArray(carsResponse)) {
            filterList = carsResponse.Data ? carsResponse.Data : carsResponse;
        }

    } else {
        switch (forButton) {
            case 0: filterList = list.slice().sort((car1, car2) => car2.year - car1.year);
                break;
            case 1: filterList = list.slice().sort((car1, car2) => car1.year - car2.year);
                break;
            case 2: filterList = list.slice().filter((car) => car.carPrice != null);
                break;
            case 3: filterList = list.slice().filter((car) => car.carRentalPrice != null);
                break;
            case 4: filterList = list;
                break;
        }
        //slice عشان ما ياثر ع الاراي الاصلية  يوخذ نسخة منها بس
    }

    showUlList(filterList, liFunc, ul);



    await refreshSwiper();

}

async function SortedOrderRequest(list, liFunc, ul,  forButton) {
    console.log(list);
    let filterList = [];
    ul.innerHTML = '';
        switch (forButton) {
            case 0: filterList = list.slice().sort((order1, order2) =>order2.car.year - order1.car.year);
                break;
            case 1: filterList = list.slice().sort((order1, order2) => order1.car.year - order2.car.year);
                break;
            case 2: filterList = list.slice().filter((order) => order.car.carPrice != null);
                break;
            case 3: filterList = list.slice().filter((order) => order.car.carRentalPrice != null);
                break;
            case 4: filterList = list;
                break;
        }
       console.log(filterList)
   

    showUlList(filterList, liFunc, ul);
    await refreshSwiper();

}







