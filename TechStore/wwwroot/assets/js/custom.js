$(document).ready(() => {

    $(".addtobasket").click(function (e) {
        e.preventDefault();
        let url = $(this).attr("href");
        fetch(url)
            .then(response => 
             response.text()
            ).then(data => {
                $("#headerCart").html(data)

            }
            )
    })
   
    $(document).on('click', '#closeIconforCartProduct', function (e) { 
        e.preventDefault();
        let url = $(this).attr("href");
        fetch(url)
            .then(response => 
                 response.text()
            ).then(data => {
                $("#headerCart").html(data);
            })
       

     })
    $(document).on('click', '.deletefromcartbtn', function (e) {
        e.preventDefault();

        fetch($(this).attr('href'))
            .then(res => res.text())
            .then(data => {
                $('.basketindexcontainer').html(data);
                fetch('/basket/getbasket')
                    .then(res => res.text())
                    .then(data => {
                        $('#headerCart').html(data);
                    });
            })
    })


})
