$(document).ready(function() {
    $(".bottomForMobileGeneral a").click(function(e){
        e.preventDefault();
     })
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
    $('.slider-for').slick({
      slidesToShow: 1,
      slidesToScroll: 1,
      autoplay:true,
      arrows: false,
      fade: true
   });
   $('.slider-nav').slick({
      slidesToShow: 5,
      slidesToScroll: 1,
      autoplay:true,
      asNavFor: '.slider-for',
      dots: false,
      centerMode: false,
      focusOnSelect: false
   });
      $('.slider-nav .slick-slide').on('click', function (event) {
        $('.slider-for').slick('slickGoTo', $(this).data('slickIndex'));
     });
     $('#recentProduct .productBox').slick({
      dots: true,
      autoplay: true,
      infinite: false,
      prevArrow: false,
    nextArrow: false,
      speed: 300,
      slidesToShow: 5,
      slidesToScroll: 1,
      responsive: [
        {
          breakpoint: 1024,
          settings: {
            slidesToShow: 5,
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
            slidesToShow: 2,
            slidesToScroll: 1
          }
        }
        // You can unslick at a given breakpoint now by adding:
        // settings: "unslick"
        // instead of a settings object
      ]
    });
       $('.descriptionTechnicalSpecsReviews #DescriptionP').click(function(){
           if($("#Description").css("display")=="none"){
            $("#Description").css("display","block");
       $("#Reviews").css("display","none");
       $("#TechnicalSpecs").css("display","none");
       $(".descriptionTechnicalSpecsReviews .single #DescriptionP").css("background-color","rgb(245, 245, 245)");
       $(".descriptionTechnicalSpecsReviews .single #TechnicalP").css("background-color","white");
       $(".descriptionTechnicalSpecsReviews .single #ReviewsP").css("background-color","white");
         }
         
       })
       $('.descriptionTechnicalSpecsReviews #TechnicalP').click(function(){
         if($("#TechnicalSpecs").css("display")=="none"){
          $("#TechnicalSpecs").css("display","block");
          $("#Description").css("display","none");
       $("#Reviews").css("display","none");
       $(".descriptionTechnicalSpecsReviews .single #TechnicalP").css("background-color","rgb(245, 245, 245)");
       $(".descriptionTechnicalSpecsReviews .single #DescriptionP").css("background-color","white");
       $(".descriptionTechnicalSpecsReviews .single #ReviewsP").css("background-color","white");
       
       }
     })
     $('.descriptionTechnicalSpecsReviews #ReviewsP').click(function(){
      if($("#Reviews").css("display")=="none"){
       $("#Reviews").css("display","block");
       $("#Description").css("display","none");
       $("#TechnicalSpecs").css("display","none");
       $(".descriptionTechnicalSpecsReviews .single #ReviewsP").css("background-color","rgb(245, 245, 245)");
       $(".descriptionTechnicalSpecsReviews .single #DescriptionP").css("background-color","white");
       $(".descriptionTechnicalSpecsReviews .single #TechnicalP").css("background-color","white");
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