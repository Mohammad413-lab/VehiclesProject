
import { ApiServices } from "../apiservices/ApiServices.js";
import { showUlList } from "../functions/ShowUlList.js"
import { eventListenerAsync } from "../functions/EventListener.js"
import { formattedDate } from "../functions/DateFormate.js";
import { SentMessage } from "../dtos/request/sentMessage.js";
import { UserKey } from "../static/User.js";



let response = null;
let messagesResponse = null;
let userChoosedEmail = document.getElementById("footerName");
let chatsList = [];
let emailsList = [];
if (UserKey.UserId) {
    emailsList = response = await ApiServices.GetUserContacted(UserKey.UserId);

}


let liIChatnformation = `
  
   
    <div class="BorderBottom">
        <p id="UserEmail" class="CarInfoDesignValue"></p>
         <p id="Message" class="CarInfoDesignValue">  </p>
         <p id="MessageDate" class="time"> </p>
   </div>
  
`;


const chatUl = document.getElementById("MessageUl");
const emailsUl = document.getElementById("EmailsUl");
const trashButton = document.getElementById("TrashButton");
const inputSentMessage = document.getElementById("InputSentMessage");
const buttonSentMessage = document.getElementById("ButtonSentMessage");
let whoIamTalkingTo = null;
eventListenerAsync(buttonSentMessage, async function () {

    if (whoIamTalkingTo != null && inputSentMessage.value != '') {

        let sent = new SentMessage(UserKey.UserId, whoIamTalkingTo, inputSentMessage.value);
        inputSentMessage.value='';
        await ApiServices.SentMessage(sent);
        chatsList=await ApiServices.GetMessagesWithUser(whoIamTalkingTo);
        chatUl.innerHTML='';
        showUlList(chatsList, messageLi, chatUl);
        chatUl.scrollTop = chatUl.scrollHeight;
     
        
          
       

    }

})
eventListenerAsync(trashButton, async function () {
    let emails = emailsUl.querySelectorAll('input[type="checkbox"]:checked');
    if (emails.length > 0) {
        let emailsArray = Array.from(emails);
        let selectedIds = emailsArray.map(checkbox => checkbox.value);
        for (let id of selectedIds) {
            let index = emailsList.findIndex(email => email.userId == id);
            if (index !== -1) {
                emailsList.splice(index, 1);
            }
            let response=await ApiServices.RemoveMessageWithUserYouChatWith(id);
    
        }
        chatUl.innerHTML = '';
        emailsUl.innerHTML = '';
        showUlList(emailsList, EmailLi, emailsUl);
    }

});


if (emailsUl) {
    if (response != null) {
        showUlList(emailsList, EmailLi, emailsUl);
    }
}

function messageLi(message, ul) {
    let li = document.createElement("li")
    li.innerHTML = liIChatnformation;
    let userEmail = li.querySelector("#UserEmail");
    let theMessage = li.querySelector("#Message");
    let messageDate = li.querySelector("#MessageDate");
    messageDate.textContent = formattedDate(message.dateSent);
    if (message.fromUser.userId == UserKey.UserId) {
        userEmail.classList.remove("CarInfoDesignValue");
        userEmail.classList.add("Response");
    }
    userEmail.textContent = message.fromUser.userId == UserKey.UserId ? "( Me )" : message.fromUser.firstName+" "+message.fromUser.lastName;
    theMessage.textContent = message.message;
    ul.appendChild(li);
}

function EmailLi(email, ul) {
    let li = document.createElement("li")
    li.innerHTML = `
  
   
          <div class="MessagesLeftSide" style="margin:px padding:0px;">
                                <div style="display: flex; margin-bottom:10px;">
                                    <div class="checkbox-container" >
                                        <input type="checkbox" name="emails" value="${email.userId}"/>
                                        <span class="checkmark"></span>
                                    </div>
                                    <div id="EmailMessages" >
                                        <p id="UserEmail" style="margin:0px;">mohammadaqrbawi@yahoo.com</p>
                                        <div class="DivRow"  >
                                        <p id="Message" class="time" style=" margin:0px; overflow: hidden; text-overflow: ellipsis; 
                                        white-space: nowrap; width: 150px;">--</p>
                                        <p id="DateSent" class="time" style=" margin:0px; overflow: hidden; text-overflow: ellipsis; 
                                        white-space: nowrap; width: 150px;">--</p> </div>
                                    </div>

                                </div>
                            </div>
`;

    let userEmail = li.querySelector("#UserEmail");
    let message = li.querySelector("#Message");
    let emailMessages = li.querySelector("#EmailMessages");
    let dateSent=li.querySelector("#DateSent");
    dateSent.textContent=formattedDate(email.dateSent);
    userEmail.textContent = email.fullName;
    message.textContent = email.message;
    ul.appendChild(li);
    if (email.isFromOthers && !email.isRead) {
        message.style.color = "black";
    }
    eventListenerAsync(emailMessages, async function () {
        whoIamTalkingTo = email.userId;
        userChoosedEmail.textContent = email.fullName;
        chatsList = messagesResponse = await ApiServices.GetMessagesWithUser(email.userId);
        await ApiServices.ReadAllMessageFromUser(email.userId);
        chatUl.innerHTML = '';
        showUlList(chatsList, messageLi, chatUl);
        chatUl.scrollTop = chatUl.scrollHeight;
      
    }
    );


}

