export function eventListener(button, arrowfunction) {
    button.onclick = arrowfunction;
}

export function eventListenerAsync(button, arrowfunction) {
    button.onclick = async () => await arrowfunction();
}