
import { eventListener } from "../../functions/EventListener.js"
let backButton=document.getElementById("BackButton");
eventListener(backButton,function(){
  window.location.replace("home.html");
});