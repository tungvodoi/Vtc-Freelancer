$(document).ready(function () {
    $('.box-row__slider').slick({
        infinite: true,
        slidesToShow: 5,
        slidesToScroll: 5,
        prevArrow : `<button type="button" class="slick-prev fa fa-angle-left"></button>`,
        nextArrow : `<button type="button" class="slick-prev fa fa-angle-right"></button>`
    });
    $('.slide-gig-js').slick({
        infinite: true
    });
});
