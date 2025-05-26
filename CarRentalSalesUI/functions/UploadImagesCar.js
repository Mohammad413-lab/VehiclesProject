import { ApiServices } from "../apiservices/ApiServices.js";
export async function uploadCarImages(fileInput, carId) {
    try {
        if(!Array.isArray(fileInput)){
            if(!fileInput || fileInput.files.length ==0 || !carId ){return null;}
        }
        if(fileInput.length==0){return null;}
        let response = await ApiServices.uploadCarImages(Array.isArray(fileInput)?fileInput:fileInput.files, carId);
        return response;
    } catch (e) {
        throw "error uploading images" + e
    }


}