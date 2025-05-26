import { eventListener } from "../functions/EventListener.js";
function showSuccess() {
    const box = document.getElementById('successBox');

    box.style.display = 'flex';

    setTimeout(() => {
      box.style.display = 'none';
   }, 5*1000);
  }
  

  function loadAlertBox() {
    fetch('../../components/alert.html')
      .then(response => response.text())
      .then(html => {
        document.body.insertAdjacentHTML('beforeend', html);
      });
  }

  export{showSuccess,loadAlertBox}