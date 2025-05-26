import { eventListener } from "../functions/EventListener.js";
export async function loadingAreYouSure() {
    return fetch("../../pageshelper/areyousure.html")
        .then(response => response.text())
        .then(data => {
            document.getElementById("AreYouSureContainer").innerHTML = data;
        });
}


export async function showAreYouSure() {

    const areYouSure = document.getElementById("AreYouSure");
        console.log(areYouSure);
    areYouSure.style.display = "flex";
    return new Promise((resolve) => {
        eventListener(document.getElementById("Yes"), function () {
            areYouSure.style.display = "none";
            resolve(true);
        });
        eventListener(document.getElementById("No"), function () {
            areYouSure.style.display = "none";
            resolve(false);
        })
    });

}


