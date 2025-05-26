import { userCart,loadingCarUl } from "../../pageshelper/carul.js";
import { loadingSucessLogo,loadingSendMessageContent } from "../../pageshelper/sendmessage.js";
import { loadingContent, showLoading, hideLoading } from "../../pageshelper/loading.js";
import { loadingSideBar } from "../../pageshelper/sidebar.js";
import { pushSideBar } from "../../pageshelper/carul.js";
import { loadingList } from "../../pageshelper/loadinglist.js";
import { loadingNotFound } from "../../pageshelper/nocarsfound.js";
import { loadingNoMore } from "../../pageshelper/nomorecars.js";
import { loadingUserProfile } from "../../pageshelper/userprofile.js";
import { loadingLocalSearchedNavBar } from "../../pageshelper/carul.js";


await loadingContent();
await loadingSendMessageContent();
await loadingList();
await loadingSucessLogo();
await loadingSideBar();
await pushSideBar(userCart,true);
await loadingNoMore();
await loadingUserProfile();
await loadingLocalSearchedNavBar(userCart);
showLoading();
loadingCarUl(userCart);
hideLoading();


const swiper = new Swiper('.swiper-container', {
    loop: false, 
    spaceBetween: 2, 
    slidesPerView: 1,

    pagination: {
        el: '.swiper-pagination', 
        clickable: true, 
    },
});
