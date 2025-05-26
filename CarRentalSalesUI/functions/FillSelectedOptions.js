export function fillSelectedOption(selected,id,value){
    const option = document.createElement("option");
    option.id=id;
    option.value=id;
    option.textContent = value;
        selected.appendChild(option);
   
}