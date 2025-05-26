
export async function loadingList() {
    return fetch("../../pageshelper/loadinglist.html")
      .then(response => response.text())
      .then(data => {
        document.getElementById("LoadingForList").innerHTML = data;
      });
  }
  

  export function showLoadingList() {
    const loadingContainer = document.getElementById("LoadingList");
    loadingContainer.style.display="flex";

  }

  export function hideLoadingList() {
    console.log("Loading is hidden!");
    const loadingContainer = document.getElementById("LoadingList");
      loadingContainer.style.display = "none";
  }

