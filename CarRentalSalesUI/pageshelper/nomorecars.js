export async function loadingNoMore() {
    return fetch("../../pageshelper/nomorecars.html")
      .then(response => response.text())
      .then(data => {
        document.getElementById("NoMoreContainer").innerHTML = data;
      });
  }
  
  export function showNoMore(text, bool) {
  const noMoreContainer = document.getElementById("NoMore");
  const textNoMore = document.getElementById("TextNoMore");
  textNoMore.innerHTML = `${text} <i class="fa-solid fa-car"></i>`;
  if (bool) {
    noMoreContainer.style.display='flex';
  } else {
    noMoreContainer.style.display='none';
  }
}

