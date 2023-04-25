$(document).ready(function () {
    $(".bottomForMobileGeneral a").click(function (e) {
        e.preventDefault();
    })
    $("#categoryInputOnSearch").click(function () {
        if ($(".allCategories").css("visibility") == "visible") {
            $(".allCategories").css("visibility", "hidden");
            $(".allCategories").css("opacity", "0");
            $(".allCategories").css("top", "55px");
            $("#triangle").css("visibility", "hidden");
            $("#triangle").css("opacity", "0");
            $("#triangle").css("top", "50px");
        }
        else {
            $(".allCategories").css("visibility", "visible");
            $(".allCategories").css("opacity", "1");
            $(".allCategories").css("top", "35px");
            $("#triangle").css("visibility", "visible");
            $("#triangle").css("opacity", "1");
            $("#triangle").css("top", "35px");
        }
    })
    $("#inputSearch").click(function () {
        if ($(".searchSuggestions").css("visibility") == "hidden") {
            $(".searchSuggestions").css("visibility", "visible");
            $(".searchSuggestions").css("opacity", "1");
            $(".searchSuggestions").css("top", "78px");
        }
    })
    $('#BestSeller .productBoxes').slick({
        dots: false,
        infinite: true,
        autoplay: true,
        dots: true,
        appendDots: $('#dotsForBestSeller'),
        speed: 300,
        slidesToShow: 4,
        slidesToScroll: 1,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 1,
                    infinite: false,
                    dots: true
                }
            },
            {
                breakpoint: 991,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 576,
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
    $(".RIGHTonShop .category i").click(function () {
        if ($(".RIGHTonShop .categoryList").css("display") == "none") {
            $(".RIGHTonShop .categoryList").css("display", "block");
            $(".RIGHTonShop .category i").removeClass("fa-solid fa-plus").addClass("fa-solid fa-minus");

        }
        else {
            $(".RIGHTonShop .categoryList").css("display", "none");
            $(".RIGHTonShop .category i").removeClass("fa-solid fa-minus").addClass("fa-solid fa-plus");

        }
    })
    $(".RIGHTonShop .price .priceTab #iconForPrice").click(function () {
        if ($(".RIGHTonShop .rangeGeneral").css("display") == "none") {
            $(".RIGHTonShop .rangeGeneral").css("display", "block");
            $(".RIGHTonShop .price .priceTab #iconForPrice").removeClass("fa-solid fa-plus").addClass("fa-solid fa-minus");

        }
        else {
            $(".RIGHTonShop .rangeGeneral").css("display", "none");
            $(".RIGHTonShop .price .priceTab #iconForPrice").removeClass("fa-solid fa-minus").addClass("fa-solid fa-plus");

        }
    })
    $(".RIGHTonShop .brands .brandTab i").click(function () {
        if ($(".RIGHTonShop .brandSearch").css("display") == "none") {
            $(".RIGHTonShop .brandSearch").css("display", "block");
            $(".RIGHTonShop .brand  i").removeClass("fa-solid fa-plus").addClass("fa-solid fa-minus");

        }
        else {
            $(".RIGHTonShop .brandSearch").css("display", "none");
            $(".RIGHTonShop .brand i").removeClass("fa-solid fa-minus").addClass("fa-solid fa-plus");

        }
    })
    $(".RIGHTonShop .colors .colorTab i").click(function () {
        if ($(".RIGHTonShop .colorSearch").css("display") == "none") {
            $(".RIGHTonShop .colorSearch").css("display", "block");
            $(".RIGHTonShop .colors .colorTab i").removeClass("fa-solid fa-plus").addClass("fa-solid fa-minus");

        }
        else {
            $(".RIGHTonShop .colorSearch").css("display", "none");
            $(".RIGHTonShop .colors .colorTab i").removeClass("fa-solid fa-minus").addClass("fa-solid fa-plus");

        }
    })
    $(".RIGHTonShop .bestseller .bestsellerTab i").click(function () {
        if ($(".RIGHTonShop .bestsellerPart").css("display") == "none") {
            $(".RIGHTonShop .bestsellerPart").css("display", "block");
            $(".RIGHTonShop .bestseller .bestsellerTab i").removeClass("fa-solid fa-plus").addClass("fa-solid fa-minus");

        }
        else {
            $(".RIGHTonShop .bestsellerPart").css("display", "none");
            $(".RIGHTonShop .bestseller .bestsellerTab i").removeClass("fa-solid fa-minus").addClass("fa-solid fa-plus");

        }
    })

})

function controlFromInput(fromSlider, fromInput, toInput, controlSlider) {
    const [from, to] = getParsed(fromInput, toInput);
    fillSlider(fromInput, toInput, '#C6C6C6', '#25daa5', controlSlider);
    if (from > to) {
        fromSlider.value = to;
        fromInput.value = to;
    } else {
        fromSlider.value = from;
    }
}

function controlToInput(toSlider, fromInput, toInput, controlSlider) {
    const [from, to] = getParsed(fromInput, toInput);
    fillSlider(fromInput, toInput, '#C6C6C6', '#25daa5', controlSlider);
    setToggleAccessible(toInput);
    if (from <= to) {
        toSlider.value = to;
        toInput.value = to;
    } else {
        toInput.value = from;
    }
}

function controlFromSlider(fromSlider, toSlider, fromInput) {
    const [from, to] = getParsed(fromSlider, toSlider);
    fillSlider(fromSlider, toSlider, '#C6C6C6', '#f28b00', toSlider);
    if (from > to) {
        fromSlider.value = to;
        fromInput.value = to;
    } else {
        fromInput.value = from;
    }
}

function controlToSlider(fromSlider, toSlider, toInput) {
    const [from, to] = getParsed(fromSlider, toSlider);
    fillSlider(fromSlider, toSlider, '#C6C6C6', '#f28b00', toSlider);
    setToggleAccessible(toSlider);
    if (from <= to) {
        toSlider.value = to;
        toInput.value = to;
    } else {
        toInput.value = from;
        toSlider.value = from;
    }
}

function getParsed(currentFrom, currentTo) {
    const from = parseInt(currentFrom.value, 10);
    const to = parseInt(currentTo.value, 10);
    return [from, to];
}

function fillSlider(from, to, sliderColor, rangeColor, controlSlider) {
    const rangeDistance = to.max - to.min;
    const fromPosition = from.value - to.min;
    const toPosition = to.value - to.min;
    controlSlider.style.background = `linear-gradient(
    to right,
    ${sliderColor} 0%,
    ${sliderColor} ${(fromPosition) / (rangeDistance) * 100}%,
    ${rangeColor} ${((fromPosition) / (rangeDistance)) * 100}%,
    ${rangeColor} ${(toPosition) / (rangeDistance) * 100}%, 
    ${sliderColor} ${(toPosition) / (rangeDistance) * 100}%, 
    ${sliderColor} 100%)`;
}

function setToggleAccessible(currentTarget) {
    const toSlider = document.querySelector('#toSlider');
    if (Number(currentTarget.value) <= 0) {
        toSlider.style.zIndex = 2;
    } else {
        toSlider.style.zIndex = 0;
    }
}

const fromSlider = document.querySelector('#fromSlider');
const toSlider = document.querySelector('#toSlider');
const fromInput = document.querySelector('#fromInput');
const toInput = document.querySelector('#toInput');
fillSlider(fromSlider, toSlider, '#C6C6C6', '#f28b00', toSlider);
setToggleAccessible(toSlider);

fromSlider.oninput = () => controlFromSlider(fromSlider, toSlider, fromInput);
toSlider.oninput = () => controlToSlider(fromSlider, toSlider, toInput);
fromInput.oninput = () => controlFromInput(fromSlider, fromInput, toInput, toSlider);
toInput.oninput = () => controlToInput(toSlider, fromInput, toInput, toSlider);

function on() {
    document.getElementById("overlay").style.display = "block";
}

function off() {
    document.getElementById("overlay").style.display = "none";
    document.querySelector(".searchSuggestions").style.visibility = "hidden";
    document.querySelector(".searchSuggestions").style.opacity = "0";
    document.querySelector(".searchSuggestions").style.top = "100px";
}