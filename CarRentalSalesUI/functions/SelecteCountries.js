export async function selectCountries(select, countryList) {
    select.innerHtml = '';
    countryList.forEach(country => {
        const option = document.createElement('option');
        option.textContent = country.countryName;
        option.value = country.countryId;
        select.appendChild(option);
    });
}