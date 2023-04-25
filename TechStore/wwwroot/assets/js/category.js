$(document).ready(function() {
    $(".bottomForMobileGeneral a").click(function(e){
        e.preventDefault();
     })
     $('.mainSliderSection').slick({
        dots: true,
        infinite: false,
        prevArrow: false,
        nextArrow:false,
        speed: 300,
        slidesToShow: 1,
        slidesToScroll: 1,
        responsive: [
          {
            breakpoint: 1024,
            settings: {
              slidesToShow: 1,
              slidesToScroll: 1,
              infinite: false,
              dots: true
            }
          },
          {
            breakpoint: 991,
            settings: {
              slidesToShow: 1,
              slidesToScroll: 1
            }
          },
          {
            breakpoint: 768,
            settings: {
              slidesToShow: 1,
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
     $("#categoryInputOnSearch").click(function(){
      if($(".allCategories").css("visibility")=="visible"){
         $(".allCategories").css("visibility","hidden");
         $(".allCategories").css("opacity","0");
         $(".allCategories").css("top","55px");
         $("#triangle").css("visibility","hidden");
         $("#triangle").css("opacity","0");
         $("#triangle").css("top","50px");
      }
      else{
         $(".allCategories").css("visibility","visible");
         $(".allCategories").css("opacity","1");
         $(".allCategories").css("top","35px");
         $("#triangle").css("visibility","visible");
         $("#triangle").css("opacity","1");
         $("#triangle").css("top","35px");
      }
    })
    $("#inputSearch").click(function(){
      if($(".searchSuggestions").css("visibility")=="hidden"){
         $(".searchSuggestions").css("visibility","visible");
         $(".searchSuggestions").css("opacity","1");
         $(".searchSuggestions").css("top","78px");
      }
    })
      
    })
    function on() {
        document.getElementById("overlay").style.display = "block";
      }
      
      function off() {
        document.getElementById("overlay").style.display = "none";
        document.querySelector(".searchSuggestions").style.visibility = "hidden";
        document.querySelector(".searchSuggestions").style.opacity = "0";
        document.querySelector(".searchSuggestions").style.top = "100px";
      }
