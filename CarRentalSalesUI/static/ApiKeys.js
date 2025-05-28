
import { url } from "../helper/url.js"
import { UserKey } from "./User.js";



export let ApiKeys = {
    User: UserKey.User ,
    LogIn: "Auth/Login",
    ResetPassword: "Users/ResetUserPassword?email=",
    Countries: url + "Users/GetAllCountries",
    SignUp: url + "Auth/Signup",
    EmailVerfied: url + "Auth/EmailVerified?email=",
    GetUserByEmail: url + "Users/GetUserByEmail?email=",
    GetUserCars: url + "CarsContoller/GetAllUserCars?userId=",
    GetCountryCars:(pageNumber)=>  `${url}CarsContoller/GetAllCountryCars?countryId=${UserKey.CountryId}&pageNumber=${pageNumber}`,
    GetAllUserCars: url + "CarsContoller/GetAllUserCars?userId=",
    GetUserCart: url + "CarsContoller/GetUserCarCart?userId=",
    AddCarToCart: url + `Users/AddCart?userId=${UserKey.UserId}&carId=`,
    GetUserContactedWithEmails: url + "Users/GetEmailsYouContact?yourId=",
    GetUserMessagesWithUser: url + `Users/GetUserMessages?firstUserId=${UserKey.UserId}&secondUserId=`,
    RemoveMessageFromUserYouChatWith: url + `Users/RemoveMessageFromUserYouChatWith?yourId=${UserKey.UserId}&userId=`,
    ReadAllMessageFromUser: url + `Users/ReadAllMessageFromUser?yourId=${UserKey.UserId}&userId=`,
    SentMessage: url + "Users/SentMessage",
    RemoveCart: url + `CarsContoller/RemoveCart?userId=${UserKey.UserId}&carId=`,
    GetUserModel: url + `CarsContoller/GetUserModel?userId=${UserKey.UserId}`,
    GetUserMake: url + `CarsContoller/GetUserMake?userId=${UserKey.UserId}`,
    AddCarModel: url + "CarsContoller/AddCarModel",
    AddCarMake: url + `CarsContoller/AddCarMake?userId=${UserKey.UserId}&carMake=`,
    AddCar: url + "CarsContoller/AddCar",
    UploadCarImages: url + "CarsContoller/UploadImageCarByCarId?carId=",
    RemoveCarByCarId: url + "CarsContoller/RemoveCar?carId=",
    GetCarImages: url + "CarsContoller/GetCarImages?carId=",
    RemoveCarImage: (imageId, imagePath) => `${url}CarsContoller/RemoveCarImage?imageId=${imageId}&imagePath=${encodeURIComponent(imagePath)}`,
    //encodeURICompnent عشان يحلي الباث زي ما هو بدون ما يلعب ب الرموز الخاصة 
    EditCar: url + "CarsContoller/EditCar",
    GetAllCarSortedByYear: (pageNumber) => `${url}CarsContoller/GetCarSortedByYear?countryId=${UserKey.CountryId}&pageNumber=${pageNumber}`,
    GetAllCarSortedByOldest: (pageNumber) => `${url}CarsContoller/GetCarSortedByOldest?countryId=${UserKey.CountryId}&pageNumber=${pageNumber}`,
    GetAllCarsForRental: (pageNumber) => `${url}CarsContoller/GetCarsForRental?countryId=${UserKey.CountryId}&pageNumber=${pageNumber}`,
    GetAllCarsForSale: (pageNumber) => `${url}CarsContoller/GetCarsForSale?countryId=${UserKey.CountryId}&pageNumber=${pageNumber}`,
    GetSearchedCars:(contain,pageNumber)=>`${url}CarsContoller/GetCarsContain?countryId=${UserKey.CountryId}&contain=${contain}&pageNumber=${pageNumber}`,
    EditUser:url+"Users/EditUser",
    ChangePassword:url + "Users/ChangeUserPassword",
    GetMyRequest:`${url}SalesOrder/GetMyOrderSale?userId=${UserKey.UserId}`,
    AddSaleOrder:`${url}SalesOrder/AddSaleOrder`,

}

export const imageUrl = "https://localhost:5051/";