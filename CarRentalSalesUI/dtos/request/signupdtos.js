export class SignUpDtos{
    constructor(firstName, lastName, email, phone, address, password,countryId) {
        this.firstName = firstName.trim();
        this.lastName = lastName.trim();
        this.email = email.trim();
        this.phone = phone.trim();
        this.address = address.trim();
        this.password = password.trim();
        this.countryId=countryId;
        this.validate();
      }
    
      validate() {
        if (!this.firstName)
          throw new Error(" firstName  is required");
        if (!this.lastName)
          throw new Error(" lastName  is required");
        if (!this.email)
          throw new Error(" email  is required");
        if (!this.phone)
          throw new Error(" phone is required");
        if (!this.address)
          throw new Error("address is required");
        if (!this.password)
          throw new Error("password is required");
        if(!this.countryId)
          throw new Error("countryId is required")
      }
}