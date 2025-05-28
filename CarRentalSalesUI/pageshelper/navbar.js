
import { eventListener } from "../functions/EventListener.js";
import { ApiServices } from "../apiservices/ApiServices.js";
import { showUlList } from "../functions/ShowUlList.js";
import { showNoCarFound } from "./nocarsfound.js";
import { apiState } from "../static/apistate.js";
import { Car } from "../models/car.js";
import { refreshApiState } from "../static/apistate.js";
import { showNoMore } from "./nomorecars.js";
import { refreshSwiper } from "../functions/Swiper.js";
import { showLoadingList, hideLoadingList } from "./loadinglist.js";
import { UserKey } from "../static/User.js";

document.addEventListener("DOMContentLoaded", function () {
  fetch("../../pageshelper/navbar.html")
    .then(response => response.text())
    .then(data => {
      document.getElementById("NavBar").innerHTML = data;
      const emailButton = document.getElementById("EmailButton");
      const dropDownMenuButton = document.getElementById("DropDownMenuButton");
      const dropDownMenuUl = document.getElementById("DropDownMenuUl");
      let isClickedDropDownMenu = false;
      const logOutButton = document.getElementById("ButtonLogout");
      emailButton.innerHTML = `<i class="fa-solid fa-envelope-circle-check" style="margin-top:2px;"></i> ${UserKey.User.email} `
      eventListener(dropDownMenuButton, function () {
        if (isClickedDropDownMenu) {
          isClickedDropDownMenu = false;
          dropDownMenuUl.style.display = "block";
        } else {
          isClickedDropDownMenu = true;
          dropDownMenuUl.style.display = "none";
        }
      });
      eventListener(logOutButton, function () {
        sessionStorage.clear();
        location.replace("../../index.html");
      });


    }
    );


});

let inputSearched = '';

export async function searchCarsFromServer(liFunc, ul, isForUserCars) {
  const carSearch = document.getElementById("CarSearch");
  const listLi = document.getElementById("ListLi");
  let list = [];
  carSearch.addEventListener('input', async function () {

    refreshApiState();
    showNoMore("", false);
    list.length = 0;
    ul.innerHTML = '';

    if (!isForUserCars) {
      if (carSearch.value != '') {

        apiState.currentApiType = 1;
        inputSearched = carSearch.value.trim();
        let response = await ApiServices.getSearchedCars(inputSearched, 1);
        if (Array.isArray(response)) {
          list = response;
          console.log(list);
          showNoCarFound('', false);
        } else {
          showNoCarFound(response.message, true);
        }

      } else {

        showNoCarFound('', false);
        apiState.currentApiType = 0;
        let response = await ApiServices.getCountryCars(1);
        list = response;
        console.log(list);

      }
    }

    showUlList(list, liFunc, ul);
    await refreshSwiper();

  });

  window.onscroll = async function () {
    if (window.scrollY > 0 && window.scrollY + window.innerHeight >= document.documentElement.scrollHeight) {
      showLoadingList();

      if (apiState.hasMore && apiState.currentApiType == 0) {

        let response = await ApiServices.getCountryCars(++apiState.pageNumber);
        await getMoreCars(response, liFunc, ul);


      } else if (apiState.hasMore && apiState.currentApiType == 1) {

        let response = await ApiServices.getSearchedCars(inputSearched, ++apiState.pageNumber);
        await getMoreCars(response, liFunc, ul);

      } else if (apiState.hasMore && apiState.currentApiType == 2) {

        let response = await ApiServices.getCarsSortedByYear(++apiState.pageNumber);
        await getMoreCars(response.Data, liFunc, ul);

      } else if (apiState.hasMore && apiState.currentApiType == 3) {

        let response = await ApiServices.getCarsSortedByOldest(++apiState.pageNumber);
        await getMoreCars(response.Data, liFunc, ul);

      } else if (apiState.hasMore && apiState.currentApiType == 4) {

        let response = await ApiServices.getCarsForSale(++apiState.pageNumber);
        await getMoreCars(response.Data, liFunc, ul);

      } else if (apiState.hasMore && apiState.currentApiType == 5) {

        let response = await ApiServices.getCarsForRental(++apiState.pageNumber);
        await getMoreCars(response.Data, liFunc, ul);

      }

      await refreshSwiper();
      hideLoadingList();
    }
  };
}


async function getMoreCars(response, liFunc, ul) {
  showNoMore('', false);
  let tempCars = [];
  if (Array.isArray(response) && response.length > 0) {
    response.forEach(carFromJson => {
      let car = new Car();
      car.fillCarFromjson(carFromJson);
      tempCars.push(car);
      apiState.listState.push(car);

    });
    console.log("this is car from json");

    showUlList(tempCars, liFunc, ul);
  } else {
    apiState.hasMore = false;
    showNoMore("No more vehicle", true);
  }
}


export function localSearchNavBar(liFunc, ul, list) {
 
  const carSearch = document.getElementById("CarSearch");
  carSearch.addEventListener('input', async function () {

    showNoMore("", false);
    showNoCarFound('', false);
    let userInput = carSearch.value;
    ul.innerHTML = '';
    filterCars(userInput,list,liFunc,ul);
    await refreshSwiper();

  }

  );

   onScrollWindow("You dont have more vehicle in your list");

}

export function localSearchNavBarReq(liFunc, ul, list) {
 
  const carSearch = document.getElementById("CarSearch");
  carSearch.addEventListener('input', async function () {

    showNoMore("", false);
    showNoCarFound('', false);
    let userInput = carSearch.value;
    ul.innerHTML = '';
    filterRequestOrder(userInput,list,liFunc,ul);
    await refreshSwiper();

  });


  onScrollWindow("You dont have more vehicle in your list");



}


function filterRequestOrder(input, list,liFunc,ul) {
   let filterReq = list.filter(order => {
    return order.car.year.toString() === input ||
      order.car.carPrice != null && order.car.carPrice.toString() === input ||
      order.car.rentalPrice != null && order.car.rentalPrice.toString() === input ||
      order.car.model.toLowerCase().includes(input.toLowerCase()) ||
      order.car.make.toLowerCase().includes(input.toLowerCase())
  });
  if (filterReq.length == 0) {
    showNoCarFound("No vehicle found", true);
  }
  showUlList(filterReq, liFunc, ul);

}

function filterCars(input, list,liFunc,ul) {
   let filterCar  = list.filter(car => {
    return car.year.toString() === input ||
      car.carPrice != null && car.carPrice.toString() === input ||
      car.rentalPrice != null && car.rentalPrice.toString() === input ||
      car.model.toLowerCase().includes(input.toLowerCase()) ||
      car.make.toLowerCase().includes(input.toLowerCase())
  });
  if (filterCar.length == 0) {
    showNoCarFound("No vehicle found", true);
  }
  showUlList(filterCar, liFunc, ul);
}


function onScrollWindow(text){
  window.onscroll = async function () {
    if (window.scrollY > 0 && window.scrollY + window.innerHeight >= document.documentElement.scrollHeight) {
      showNoMore(text, true);
    }
  };

}





