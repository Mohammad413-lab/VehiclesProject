
import { ApiServices, } from "../../apiservices/ApiServices.js";
import { loadingSucessLogo,loadingSendMessageContent } from "../../pageshelper/sendmessage.js";
import { loadingSideBar } from "../../pageshelper/sidebar.js";
import { loadSwiper } from "../../functions/Swiper.js";
import { loadingNoMore } from "../../pageshelper/nomorecars.js";
import { loadingVecRequestedUl } from "../../pageshelper/requestorderUl.js";
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

let myRequested= await ApiServices.getMyVechileRequested();

loadingVecRequestedUl(myRequested);