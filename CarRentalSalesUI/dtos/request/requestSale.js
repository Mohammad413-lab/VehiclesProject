export class RequestSale{
   constructor(carID=null,userId=null,totalAmount,note) {
       this.carID=carID;
       this.userId=userId;
       this.totalAmount=totalAmount,
       this.note=note;
   }
}