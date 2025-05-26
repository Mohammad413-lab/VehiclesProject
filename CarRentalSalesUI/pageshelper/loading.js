
export async function loadingContent() {
    return fetch("../../pageshelper/loading.html")
      .then(response => response.text())
      .then(data => {
        document.getElementById("Loading").innerHTML = data;
      });
  }
  

  export function showLoading() {
    const loadingContainer = document.getElementById("LoadingContainer");
    loadingContainer.style.display="flex";

  }

  export function hideLoading() {
    console.log("Loading is hidden!");
    const loadingContainer = document.getElementById("LoadingContainer");
      loadingContainer.style.display = "none";
    
  }

