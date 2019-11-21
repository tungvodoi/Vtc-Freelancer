$(document).ready(function () {
    $('.box-row__slider').slick({
        infinite: true,
        slidesToShow: 5,
        slidesToScroll: 5,
        prevArrow: `<button type="button" class="slick-prev fa fa-angle-left"></button>`,
        nextArrow: `<button type="button" class="slick-prev fa fa-angle-right"></button>`, responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3,
                    infinite: true,
                }
            },
            {
                breakpoint: 600,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
            // You can unslick at a given breakpoint now by adding:
            // settings: "unslick"
            // instead of a settings object
        ]
    });
    $('.slide-gig-js').slick({
        infinite: true,
        prevArrow: `<i class="fa fa-chevron-left arrowSlide arrowSlide-left"></i>`,
        nextArrow: `<i class="fa fa-chevron-right arrowSlide arrowSlide-right"></i>`
    });
    $('.arrowSlide').click((e) => {
        e.preventDefault();
    })
});
