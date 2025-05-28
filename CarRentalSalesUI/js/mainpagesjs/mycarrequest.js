
import { eventListener, eventListenerAsync } from "../../functions/EventListener.js";
import { fillSelectedOption } from "../../functions/FillSelectedOptions.js";
import { AddCar } from "../../dtos/request/addcardtos.js";
import { UserKey } from "../../static/User.js";
import { uploadCarImages } from "../../functions/UploadImagesCar.js";
import { ApiServices, } from "../../apiservices/ApiServices.js";
import { loadingSucessLogo,loadingSendMessageContent } from "../../pageshelper/sendmessage.js";
import { loadingSideBar } from "../../pageshelper/sidebar.js";
import { loadSwiper } from "../../functions/Swiper.js";
import { loadingNoMore } from "../../pageshelper/nomorecars.js";
import { pushSideBar } from "../../pageshelper/carul.js";
import { loadingVecUl } from "../../pageshelper/requestorderUl.js";
import { loadingVInfo } from "../../pageshelper/vinfo.js";
import { activeSideBarForRequest } from "../../pageshelper/requestorderUl.js";
import { loadingUserProfile } from "../../pageshelper/userprofile.js";
import { activeLocalNavBarRequestSearch } from "../../pageshelper/requestorderUl.js";



await loadingVInfo();
await loadingSideBar();
await loadingNoMore();
await loadingUserProfile();
await loadingSucessLogo();
await loadSwiper();
await loadingSendMessageContent();

let myRequest = await ApiServices.getMyRequesyVechile();
await activeLocalNavBarRequestSearch(myRequest);
activeSideBarForRequest(myRequest);
loadingVecUl(myRequest);


