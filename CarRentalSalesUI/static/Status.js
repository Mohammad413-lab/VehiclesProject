export const RequestStatus={
    0:"Pending",
    1:"Accepted"
}

export function changeStatusColor(status,component){
    switch(status){
        case 0:component.style.color="#ff4747"; 
        break;
        case 1:component.style.color="#537D5D";
    }
}

export function statusInfoForRequest(status,component,bool){

    switch(status){
        case 0:bool?component.textContent="Owner has not seen your request yet":component.textContent="waiting for your reply";
        break;
        case 1:bool?component.textContent="Owner accepts your request ":component.textContent="You accepted this request";
    }

}
