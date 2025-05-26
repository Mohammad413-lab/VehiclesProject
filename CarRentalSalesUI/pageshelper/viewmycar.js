export async function loadingViewMyCar() {
    return fetch("../../pageshelper/viewmycar.html")
      .then(response => response.text())
      .then(data => {
        document.getElementById("ViewCar").innerHTML = data;
      });
  }
  

  export function showMyCar(car) {
    const viewCarBox = document.getElementById("ViewCarBox");
    viewCarBox.classList.remove("Hidden");

  }

  export function hideMyCar() {
    const viewCarBox = document.getElementById("ViewCarBox");
    viewCarBox.classList.add("Hidden");
  }