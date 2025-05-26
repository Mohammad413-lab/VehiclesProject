export function showUlList(list, fun,ul) {
    if (Array.isArray(list)) {
        list.forEach(element => {
            fun(element,ul);
        });
    }else{
        throw Error("this variable is not array")
    }

}