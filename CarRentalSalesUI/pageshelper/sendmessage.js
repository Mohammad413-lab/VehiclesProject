import { eventListener, eventListenerAsync } from "../functions/EventListener.js";
import { ApiServices } from "../apiservices/ApiServices.js";
import { SentMessage } from "../dtos/request/sentMessage.js";
import { UserKey } from "../static/User.js";
export async function loadingSendMessageContent() {
    return fetch("../../pageshelper/sendmessage.html")
        .then(response => response.text())
        .then(data => {
            document.getElementById("Message").innerHTML = data;
        });
}

export async function loadingSucessLogo() {
    return fetch("../../pageshelper/successfullogo.html")
        .then(response => response.text())
        .then(data => {
            document.getElementById("Success").innerHTML = data;
      
        });
}

export function showSucessfulLogo(text) {
    const sentSuccess = document.getElementById("SentSuccess");
    const textNote = document.getElementById("TextNote")
    textNote.textContent=text;
    sentSuccess.style.display = "flex";
    setTimeout(() => {
        sentSuccess.style.display = "none";
    }, 5000);

}


export async function showChatLogo(owner) {
 
    const ownerEmail = document.getElementById("OwnerEmail");
    const chatLogo = document.getElementById("ChatLogo");
    const closeChatLogo = document.getElementById("closeChatLogo");
    const sendMessageButton = document.getElementById("SendMessageButton");
    const sendMessageInput = document.getElementById("SendMessageInput");
    ownerEmail.textContent = owner.firstName + " " + owner.lastName;
    chatLogo.style.display = "flex";
    eventListenerAsync(sendMessageButton, async function () {
        let message = sendMessageInput.value
        if (message != "") {
            let response = await ApiServices.SentMessage(new SentMessage(UserKey.UserId, owner.userID, message));
            if (response.Status == 200) {
                console.log(response.Data.message);
                sendMessageInput.value = '';
                chatLogo.style.display = "none";
                showSucessfulLogo("Your message has been sent");
            }
        }
    });

    eventListener(closeChatLogo, function () {
        sendMessageInput.value = '';
        chatLogo.style.display = "none";

    });


}

