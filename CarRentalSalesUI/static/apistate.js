export const apiState = {
  currentApiType: -1,
  pageNumber: 1,
  isLoading: false,
  hasMore: true,
  listState:[],
  ulState:null
};

//currentApiType
//-1 do nothing 
//0 its mean getAllCars
//1 for car searched in navbar
//2 for oldest car
//3 for newest
//4 for car sale
//5 for car rental 

export function refreshApiState(){
  apiState.currentApiType=-1;
  apiState.pageNumber=1;
  apiState.isLoading=false;
  apiState.hasMore=true;
  apiState.ulState = null;
  apiState.listState=[];
}

