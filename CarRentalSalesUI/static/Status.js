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
