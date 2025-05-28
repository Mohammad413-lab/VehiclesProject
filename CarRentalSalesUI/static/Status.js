export const RequestStatus={
    0:"Pending"
}

export function changeStatusColor(status,component){
    switch(status){
        case 0:component.style.color="red";
    }
}