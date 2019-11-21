$(document).ready(function () {
    $(".box-row__slider").slick({
        infinite: true,
        slidesToShow: 5,
        slidesToScroll: 5,
        prevArrow: `<button type="button" class="slick-prev fa fa-angle-left"></button>`,
        nextArrow: `<button type="button" class="slick-prev fa fa-angle-right"></button>`
    });
    $(".slide-gig-js").slick({
        infinite: true
    });
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
