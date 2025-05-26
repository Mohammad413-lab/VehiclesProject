export async function loadingNotFound() {
  return fetch("../../pageshelper/nocarsfound.html")
    .then(response => response.text())
    .then(data => {
      document.getElementById("NoCarsFound").innerHTML = data;
    });
}

export function showNoCarFound(text, bool) {
  const noCarsFound = document.getElementById("NotFoundContainer");
  const textSearched = document.getElementById("TextSearched");
  textSearched.innerHTML = `${text} <i class="fa fa-search"></i>`;
  if (bool) {
    noCarsFound.style.display='flex';
  } else {
    noCarsFound.style.display='none';
  }
}