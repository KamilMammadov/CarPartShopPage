
$(document).ready(function () {


    if ($(".form-check input")) {
        $(".parentList").addClass("d-none")
        $(".mainImg").removeClass("d-none")


    }
    else {
        $(".mainImg").addClass("d-none")
        $(".parentList").removeClass("d-none")
    }

    $(".form-check input").click(function () {

        if ($(".form-check input")) {
            $(".parentList").addClass("d-none")
            $(".mainImg").removeClass("d-none")


        }
        else {
            $(".mainImg").addClass("d-none")
            $(".parentList").removeClass("d-none")
        }
    })
})