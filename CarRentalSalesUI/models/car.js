import { User } from "./user.js";
export class Car {

    constructor(carID, make, model, year, carPrice, carRentalPrice, color, status, purchaseDate, imagesPath, description, priceDiscount, rentalDiscount, user) {
        this.carID = carID;
        this.make = make;
        this.model = model;
        this.year = year;
        this.carPrice = carPrice;
        this.carRentalPrice = carRentalPrice;
        this.color = color;
        this.status = status;
        this.purchaseDate = purchaseDate;
        this.imagesPath = imagesPath;
        this.description = description;
        this.priceDiscount = priceDiscount;
        this.rentalDiscount = rentalDiscount;
        this.user = user;
    }

    fillCarFromjson(json) {
        this.carID = json.carID;
        this.make = json.make;
        this.model = json.model;
        this.year = json.year;
        this.carPrice = json.carPrice;
        this.carRentalPrice = json.carRentalPrice;
        this.color = json.color;
        this.status = json.status;
        this.purchaseDate = json.purchaseDate;
        this.imagesPath = json.imagesPath;
        this.description = json.description;
        this.priceDiscount = json.priceDiscount;
        this.rentalDiscount = json.rentalDiscount;
        this.user = new User(
            json.user.userID,
            json.user.firstName,
            json.user.lastName,
            json.user.email,
            json.user.phoneNumber,
            json.user.countryName,
            json.user.countryId,
            json.user.address
        );
    }
    fillOnlyCarFromjson(json) {
        this.carID = json.carID;
        this.make = json.make;
        this.model = json.model;
        this.year = json.year;
        this.carPrice = json.carPrice;
        this.carRentalPrice = json.carRentalPrice;
        this.color = json.color;
        this.status = json.status;
        this.purchaseDate = json.purchaseDate;
        this.imagesPath = json.imagesPath;
        this.description = json.description;
        this.priceDiscount = json.priceDiscount;
        this.rentalDiscount = json.rentalDiscount;
        this.user = null;
    }
    getRentalDiscount() {
        return this.carRentalPrice - this.rentalDiscount
    }

    getPriceDiscount() {
        return this.carPrice - this.priceDiscount;
    }

}