

export let swiper = null;


export async function refreshSwiper() {
  if (swiper && typeof swiper.destroy === 'function') {
    swiper.destroy(true, true);
  }

  swiper = await loadSwiper();
}

export async function loadSwiper() {
  return new Swiper('.swiper-container', {
    loop: false,
    spaceBetween: 2,
    slidesPerView: 1,

    pagination: {
      el: '.swiper-pagination',
      clickable: true,
    },
  });
}