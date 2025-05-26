export class AddCar{
   constructor(carID=null,modelId,year,color,carPrice,carRentalPrice,description,userId) {
        this.modelId=modelId;
        this.year=year;
        this.color=color;
        this.carPrice=carPrice;
        this.carRentalPrice=carRentalPrice;
        this.description=description;
        this.userId=userId;
        this.carID=carID;
   }
}