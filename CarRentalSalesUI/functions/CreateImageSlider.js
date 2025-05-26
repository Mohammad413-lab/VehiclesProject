
import { imageUrl } from "../static/ApiKeys.js";
export function createImage(swiperWrapper, image,bool) {
    
    let slide = document.createElement('div');
    slide.classList.add('swiper-slide');
    let img = document.createElement('img');
    img.id = "CarImages";
    img.src =bool? imageUrl + image:image;
    img.alt = "Car Image";
    slide.appendChild(img);
    swiperWrapper.appendChild(slide);

}