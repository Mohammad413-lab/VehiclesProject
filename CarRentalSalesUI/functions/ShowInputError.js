
export function showInputError(text, warningP, bool) {
    if (bool) {
        warningP.textContent = '';
        warningP.style.display = "none";
        return bool;
    }
    warningP.textContent = text;
    warningP.style.display = "flex";
    return bool
}
