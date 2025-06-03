
import { url } from "../helper/url.js";
import { ApiKeys } from "../static/ApiKeys.js";
import { SignUpDtos } from "../dtos/request/signupdtos.js";
export class ApiServices {
    ////User apis
    static async logIn(password, email) {

        const body = JSON.stringify({
            email: email,
            password: password
        });

        try {

            const response = await fetch(url + ApiKeys.LogIn, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: body
            })
            const data = await response.json();

            return {
                Data: data,
                Ok: response.ok,
                Status: response.status
            };

        } catch (error) {
            console.error("Error", error);
            throw error;
        }

    }

    static async resetPassword(email) {

        try {
            const response = await fetch(`${url}${ApiKeys.ResetPassword}${email}`, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json"
                }
            });


            const data = await response.json();
            return data;
        } catch (ex) {
            console.error("Error resetting password:", ex);
            throw ex;
        }

    }

    static async getAllCountry() {
        let allCountry = [];
        try {
            const response = await fetch(ApiKeys.Countries, { method: "GET", headers: { "Content-Type": "application/json" } });
            const countries = await response.json();
            allCountry = countries;
            return allCountry;
        } catch (ex) {
            throw ex;
        }
    }

    static async userSignUp(user) {
        try {
            const response = await fetch(ApiKeys.SignUp, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    "firstName": user.firstName,
                    "lastName": user.lastName,
                    "email": user.email,
                    "phoneNumber": user.phone,
                    "password": user.password,
                    "countryid": user.countryId,
                    "address": user.address
                })


            });


            const data = await response.json();

            return data;
        } catch (ex) {
            console.error("Error userSignUp :", ex);
            throw ex;
        }
    }

    static async emailVerfied(email) {
        try {
            const response = await fetch(ApiKeys.EmailVerfied + email, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },

            });
            const data = await response.json();

            return data;
        } catch (ex) {
            console.error("Error emailVerfied :", ex);
            throw ex;
        }
    }

    static async getUserByEmail(email) {

        try {
            const response = await fetch(ApiKeys.GetUserByEmail + email, { method: "GET", headers: { "Content-Type": "application/json" } });
            const responseData = await response.json();

            return responseData;
        } catch (ex) {
            throw ex;
        }
    }

    static async getCountryCars(pageNumber) {

        try {
            const response = await fetch(ApiKeys.GetCountryCars(pageNumber), { method: "GET", headers: { "Content-Type": "application/json" } });
            const countries = await response.json();

            return countries;
        } catch (ex) {
            throw ex;
        }
    }

    static async getAllUserCars(userId) {
        let userCars = [];
        try {
            const response = await fetch(ApiKeys.GetUserCars + userId, { method: "GET", headers: { "Content-Type": "application/json" } });
            const cars = await response.json();
            userCars = cars;
            return userCars;
        } catch (ex) {
            throw ex;
        }
    }

    static async getUserCart(userId) {
        let userCart = [];
        try {
            const response = await fetch(ApiKeys.GetUserCart + userId, { method: "GET", headers: { "Content-Type": "application/json" } });
            const cars = await response.json();
            userCart = cars;
            return userCart;
        } catch (ex) {
            throw ex;
        }
    }

    static async addCarToCart(carId) {
        try {
            const response = await fetch(ApiKeys.AddCarToCart + carId, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
            });

            const data = await response.json();

            return data;
        } catch (ex) {
            console.error("Error addCarToCart :", ex);
            throw ex;
        }
    }

    static async GetUserContacted(userId) {

        try {
            const response = await fetch(ApiKeys.GetUserContactedWithEmails + userId, { method: "GET", headers: { "Content-Type": "application/json" } });
            const usersEmail = await response.json();
            return usersEmail;
        } catch (ex) {
            throw ex;
        }
    }

    static async GetMessagesWithUser(otherUserId) {

        try {
            const response = await fetch(ApiKeys.GetUserMessagesWithUser + otherUserId, { method: "GET", headers: { "Content-Type": "application/json" } });
            const messages = await response.json();
            return messages;
        } catch (ex) {
            throw ex;
        }
    }

    static async RemoveMessageWithUserYouChatWith(userId) {
        try {
            const response = await fetch(ApiKeys.RemoveMessageFromUserYouChatWith + userId, {
                method: "PATCH",
                headers: {
                    "Content-Type": "application/json"
                },
            });

            const data = await response.json();

            return data;
        } catch (ex) {
            console.error("Error RemoveMessageWithUserYouChatWith :", ex);
            throw ex;
        }
    }


    static async ReadAllMessageFromUser(userId) {
        try {
            const response = await fetch(ApiKeys.ReadAllMessageFromUser + userId, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
            });

            const data = await response.json();

            return data;
        } catch (ex) {
            console.error("Error ReadAllMessageFromUser :", ex);
            throw ex;
        }
    }

    static async SentMessage(message) {


        const body = JSON.stringify({
            fromId: message.fromId,
            toId: message.toId,
            messageContent: message.message
        });

        try {

            const response = await fetch(ApiKeys.SentMessage, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: body
            })
            const data = await response.json();

            return {
                Data: data,
                Ok: response.ok,
                Status: response.status
            };

        } catch (error) {
            console.error("Error", error);
            throw error;
        }

    }

    static async RemoveCart(carId) {
        try {
            const response = await fetch(ApiKeys.RemoveCart + carId, {
                method: "Delete",
            });

            const data = await response.json();

            return data;
        } catch (ex) {
            console.error("Error delete cart:", ex);
            throw ex;
        }
    }

    static async getUserModels() {

        try {
            const response = await fetch(ApiKeys.GetUserModel, { method: "GET", headers: { "Content-Type": "application/json" } });
            const userModel = await response.json();
            return userModel;
        } catch (ex) {
            throw ex;
        }
    }

    static async getUserMakes() {

        try {
            const response = await fetch(ApiKeys.GetUserMake, { method: "GET", headers: { "Content-Type": "application/json" } });
            const userMakes = await response.json();
            return userMakes;
        } catch (ex) {
            throw ex;
        }
    }


    static async addCarModel(makeId, modelName) {


        const body = JSON.stringify({
            modelName: modelName,
            makeId: makeId,
        });

        try {

            const response = await fetch(ApiKeys.AddCarModel, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: body
            })
            const data = await response.json();

            return {
                Data: data,
                Ok: response.ok,
                Status: response.status
            };

        } catch (error) {
            console.error("Error", error);
            throw error;
        }

    }



    static async addCarMake(makeName) {


        try {

            const response = await fetch(ApiKeys.AddCarMake + makeName, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
            })
            const data = await response.json();

            return {
                Data: data,
                Ok: response.ok,
                Status: response.status
            };

        } catch (error) {
            console.error("Error", error);
            throw error;
        }

    }

    static async addCar(car) {


        const body = JSON.stringify({
            modelId: car.modelId,
            year: car.year,
            color: car.color,
            carPrice: car.carPrice,
            carRentalPrice: car.carRentalPrice,
            description: car.description,
            userId: car.userId
        });

        try {

            const response = await fetch(ApiKeys.AddCar, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: body
            })
            const data = await response.json();

            return {
                Data: data,
                Ok: response.ok,
                Status: response.status
            };

        } catch (error) {
            console.error("Error", error);
            throw error;
        }

    }


    static async uploadCarImages(formFiles, carId) {


        try {
            const formData = new FormData();
            for (let i = 0; i < formFiles.length; i++) {
                formData.append("imageFile", formFiles[i]);
            }

            const response = await fetch(ApiKeys.UploadCarImages + carId, {
                method: "POST",
                body: formData
            });



            const data = await response.json();

            return {
                Data: data,
                Ok: response.ok,
                Status: response.status
            };

        } catch (error) {
            console.error("Upload Error", error);
            throw error;
        }
    }

    static async removeMyCar(carId) {


        try {

            const response = await fetch(ApiKeys.RemoveCarByCarId + carId, {
                method: "Delete",
                headers: {
                    "Content-Type": "application/json"
                },
            })
            const data = await response.json();

            return {
                Data: data,
                Ok: response.ok,
                Status: response.status
            };

        } catch (error) {
            console.error("Error", error);
            throw error;
        }

    }

    static async getCarImages(carId) {

        try {
            const response = await fetch(ApiKeys.GetCarImages + carId, { method: "GET", headers: { "Content-Type": "application/json" } });
            const carImages = await response.json();
            return carImages;
        } catch (ex) {
            throw ex;
        }
    }

    static async removeCarImage(imgId, imgPath) {


        try {

            const response = await fetch(ApiKeys.RemoveCarImage(imgId, imgPath), {
                method: "Delete",
                headers: {
                    "Content-Type": "application/json"
                },
            })
            const data = await response.json();

            return {
                Data: data,
                Ok: response.ok,
                Status: response.status
            };

        } catch (error) {
            console.error("Error", error);
            throw error;
        }

    }


    static async editCar(car) {


        const body = JSON.stringify({
            carId: car.carID,
            modelId: car.modelId,
            year: parseInt(car.year),
            color: car.color,
            carPrice: car.carPrice !== "" && car.carPrice !== null ? parseFloat(car.carPrice) : null,
            carRentalPrice: car.carRentalPrice !== "" && car.carRentalPrice !== null ? parseFloat(car.carRentalPrice) : null,
            description: car.description && car.description !== "" ? car.description : null,
        });

        try {

            const response = await fetch(ApiKeys.EditCar, {
                method: "PATCH",
                headers: {
                    "Content-Type": "application/json"
                },
                body: body
            })
            const data = await response.json();

            return {
                Data: data,
                Ok: response.ok,
                Status: response.status
            };

        } catch (error) {
            console.error("Error", error);
            throw error;
        }

    }

    static async getCarsSortedByYear(startRow) {

        return await this.getCarsSorted(ApiKeys.GetAllCarSortedByYear(startRow));

    }

    static async getCarsSortedByOldest(startRow) {

        return await this.getCarsSorted(ApiKeys.GetAllCarSortedByOldest(startRow));

    }

    static async getCarsForSale(startPage) {
        return this.getCarsSorted(ApiKeys.GetAllCarsForSale(startPage))
    }
    static async getCarsForRental(startPage) {
        return this.getCarsSorted(ApiKeys.GetAllCarsForRental(startPage))
    }


    static async getCarsSorted(apiKey) {
        try {

            const response = await fetch(apiKey, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json"
                },

            })
            const data = await response.json();

            return {
                Data: data,
                Ok: response.ok,
                Status: response.status
            };

        } catch (error) {
            console.error("Error", error);
            throw error;
        }


    }

    static async getSearchedCars(contain, pageNumber) {
        try {
            const response = await fetch(ApiKeys.GetSearchedCars(contain, pageNumber), { method: "GET", headers: { "Content-Type": "application/json" } });
            const carSearched = await response.json();
            return carSearched;
        } catch (ex) {
            throw ex;
        }
    }

    static async editUser(user) {
        const body = {
            userID: user.userID,
            firstName: user.firstName,
            lastName: user.lastName,
            email: user.email,
            phoneNumber: user.phoneNumber,
            countryId: user.countryId,
            address: user.address
        }
        try {

            const response = await fetch(ApiKeys.EditUser, {
                method: "PATCH",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(body)

            })
            const data = await response.json();

            return {
                Data: data,
                Ok: response.ok,
                Status: response.status
            };

        } catch (error) {
            console.error("Error", error);
            throw error;
        }


    }


    static async changePassword(userId, oldPassword, newPassword) {
        const body = {
            userID: userId,
            oldPassword: oldPassword,
            newPassword: newPassword,
        }
        try {

            const response = await fetch(ApiKeys.ChangePassword, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(body)

            })
            const data = await response.json();

            return {
                Data: data,
                Ok: response.ok,
                Status: response.status
            };

        } catch (error) {
            console.error("Error", error);
            throw error;
        }


    }

    static async getMyRequesyVechile() {
        try {
            const response = await fetch(ApiKeys.GetMyRequest, { method: "GET", headers: { "Content-Type": "application/json" } });
            const myRequest = await response.json();
            return myRequest;
        } catch (ex) {
            throw ex;
        }
    }

       static async getMyVechileRequested() {
        try {
            const response = await fetch(ApiKeys.GetMyVehcilRequested, { method: "GET", headers: { "Content-Type": "application/json" } });
            const myRequested = await response.json();
            return myRequested;
        } catch (ex) {
            throw ex;
        }
    }

    static async getMyRequesyVechileId() {
        try {
            const response = await fetch(ApiKeys.GetMyVehcileRequestId, { method: "GET", headers: { "Content-Type": "application/json" } });
            const myRequest = await response.json();
            return myRequest;
        } catch (ex) {
            throw ex;
        }
    }
    static async addOrderSale(requestSale) {
        console.log("this is request");
        console.log(requestSale);
        const body = {
            carId: requestSale.carID,
            userId: requestSale.userId,
            totalAmount: requestSale.totalAmount,
            notes: requestSale.note
        }
        try {

            const response = await fetch(ApiKeys.AddSaleOrder, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(body)

            });
            const data = await response.json();

            return {
                Data: data,
                Ok: response.ok,
                Status: response.status
            };

        } catch (error) {
            console.error("Error", error);
            throw error;
        }


    }

       static async updateRequestStatus(orderId,status) {
    
        try {

            const response = await fetch(ApiKeys.UpdateRequestStatus(orderId,status), {
                method: "PATCH",
                headers: {
                    "Content-Type": "application/json"
                },

            });
            const data = await response.json();

            return {
                Data: data,
                Ok: response.ok,
                Status: response.status
            };

        } catch (error) {
            console.error("Error", error);
            throw error;
        }


    }





}