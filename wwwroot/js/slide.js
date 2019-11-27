$(document).ready(function () {
    $(".box-row__slider").slick({
        infinite: true,
        slidesToShow: 5,
        slidesToScroll: 5,
        centerPadding: '60px',
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
    $("#parentCategory").change(function () {
        $('#subCategory').html('');
        $.ajax({
            type: "POST",
            data: {
                categoryName: this.value
            },
            url: "/admin/GetListSubCategoryByCategoryParentId",
            success: function (result) {
                $('#subCategory').stop().animate({ "opacity": "0" }, 0, function () {
                    // After first animation finished
                    $(this).html(
                        result.map((res) => {
                            // return `<option name=${res.categoryName}>${res.categoryName}</option>`
                            return `<input type="checkbox" name="${res.CategoryId}" value="${res.categoryName}">${res.categoryName}<br>`
                        })
                    ).animate({ opacity: 1 });
                });
            }
        });
    });
});
