export class ValidationService {
    static isValidEmail(email) {
      const emailPattern = /^[^@\s]+@[^@\s]+\.[^@\s]+$/;
      return emailPattern.test(email);
    }
  
    static isValidPhoneNumber(phoneNumber) {
      const phonePattern = /^(\+?\(?\d{1,4}\)?|\d{1})\d{7,15}$/;
      return phonePattern.test(phoneNumber);
    }
  
    static isValidPassword(password) {
      return password.length >= 8 &&
             /[a-z]/.test(password) && 
             /[A-Z]/.test(password) && 
             /\d/.test(password);     
    }
  }