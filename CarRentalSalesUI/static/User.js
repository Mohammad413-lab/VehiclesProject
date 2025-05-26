let user = JSON.parse(sessionStorage.getItem("user"));
export let UserKey={
   User:user?user:null,
   UserId:user?user.userID:null,
   CountryId:user?user.countryId:null
}