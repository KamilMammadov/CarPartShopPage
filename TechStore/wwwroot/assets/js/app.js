

$(document).ready(function() {

   $("#categories .productPart").css("display", "none");
   $("#categories #Smart").css("display", "block");

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
    $('#OurProducts .productBoxes').slick({
      dots: false,
      infinite: false,
      prevArrow: false,
    nextArrow: false,
      speed: 300,
      slidesToShow: 3,
      rows : 2,
      slidesToScroll: 1,
      responsive: [
        {
          breakpoint: 1024,
          settings: {
            slidesToShow: 3,
            slidesToScroll: 1,
            infinite: false,
            dots: true
          }
        },
        {
          breakpoint: 991,
          settings: {
            slidesToShow: 2,
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
    $('#mostViewed .productBox').slick({
      dots: false,
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
    $('.specialOffer').slick({
      dots: false,
      infinite: true,
      autoplay: true,
      prevArrow: $('.arrowLeft i'),
    nextArrow: $('.arrowRight i'),
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
        }
        // You can unslick at a given breakpoint now by adding:
        // settings: "unslick"
        // instead of a settings object
      ]
    });
    
   $(".modalBootstrap").modal("show");
   
     $(".bottomForMobileGeneral a").click(function(e){
        e.preventDefault();
     })
    $("#productBoxNewArrival").click(function(e){
      e.preventDefault();
      $(".productBoxNewArrival").css("display","block")
      $(".productBoxFeatured").css("display","none");
      $(".productBoxTopSelling").css("display","none");
    })
    $("#productBoxFeatured").click(function(e){
      e.preventDefault();
      $(".productBoxNewArrival").css("display","none")
      $(".productBoxFeatured").css("display","block");
      $(".productBoxTopSelling").css("display","none");
    })
    $("#productBoxTopSelling").click(function(e){
      e.preventDefault();
      $(".productBoxNewArrival").css("display","none")
      $(".productBoxFeatured").css("display","none");
      $(".productBoxTopSelling").css("display","block");
    })
   $(".closeForModalOnBody").click(function(){
      $(".modalBootstrap").modal("hide");
   })
   $("button").click(function(e){
      e.preventDefault();

   })
   
   $("#icon1ForParentList").click(function(){
      if($("#icon1ForParentListUl").css("display")=="block"){
         $("#icon1ForParentListUl").css("display","none");
         $("#icon1ForParentList").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon1ForParentListUl").css("display","block");
         $("#icon1ForParentList").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#icon1ForParentChildList1").click(function(){
      if($("#icon1ForParentChildListUl1").css("display")=="block"){
         $("#icon1ForParentChildListUl1").css("display","none");
         $("#icon1ForParentChildList1").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
         
      }
      else{
         $("#icon1ForParentChildListUl1").css("display","block");
         $("#icon1ForParentChildList1").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");

      }
   })
   $("#icon1ForParentChildList2").click(function(){
      if($("#icon1ForParentChildListUl2").css("display")=="block"){
         $("#icon1ForParentChildListUl2").css("display","none");
         $("#icon1ForParentChildList2").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon1ForParentChildListUl2").css("display","block");
         $("#icon1ForParentChildList2").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");

      }
   })
   $("#icon2ForParentChildList1").click(function(){
      if($("#icon2ForParentChildListUl1").css("display")=="block"){
         $("#icon2ForParentChildListUl1").css("display","none");
         $("#icon2ForParentChildList1").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon2ForParentChildListUl1").css("display","block");
         $("#icon2ForParentChildList1").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#icon2ForParentChildList2").click(function(){
      if($("#icon2ForParentChildListUl2").css("display")=="block"){
         $("#icon2ForParentChildListUl2").css("display","none");
         $("#icon2ForParentChildList2").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon2ForParentChildListUl2").css("display","block");
         $("#icon2ForParentChildList2").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#icon3ForParentChildList1").click(function(){
      if($("#icon3ForParentChildListUl1").css("display")=="block"){
         $("#icon3ForParentChildListUl1").css("display","none");
         $("#icon3ForParentChildList1").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon3ForParentChildListUl1").css("display","block");
         $("#icon3ForParentChildList1").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#icon3ForParentChildList2").click(function(){
      if($("#icon3ForParentChildListUl2").css("display")=="block"){
         $("#icon3ForParentChildListUl2").css("display","none");
         $("#icon3ForParentChildList2").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon3ForParentChildListUl2").css("display","block");
         $("#icon3ForParentChildList2").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#icon4ForParentChildList1").click(function(){
      if($("#icon4ForParentChildListUl1").css("display")=="block"){
         $("#icon4ForParentChildListUl1").css("display","none");
         $("#icon4ForParentChildList1").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon4ForParentChildListUl1").css("display","block");
         $("#icon4ForParentChildList1").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#icon4ForParentChildList2").click(function(){
      if($("#icon4ForParentChildListUl2").css("display")=="block"){
         $("#icon4ForParentChildListUl2").css("display","none");
         $("#icon4ForParentChildList2").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon4ForParentChildListUl2").css("display","block");
         $("#icon4ForParentChildList2").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#icon5ForParentChildList1").click(function(){
      if($("#icon5ForParentChildListUl1").css("display")=="block"){
         $("#icon5ForParentChildListUl1").css("display","none");
         $("#icon5ForParentChildList1").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon5ForParentChildListUl1").css("display","block");
         $("#icon5ForParentChildList1").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#icon5ForParentChildList2").click(function(){
      if($("#icon5ForParentChildListUl2").css("display")=="block"){
         $("#icon5ForParentChildListUl2").css("display","none");
         $("#icon5ForParentChildList2").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon5ForParentChildListUl2").css("display","block");
         $("#icon5ForParentChildList2").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#icon6ForParentChildList1").click(function(){
      if($("#icon6ForParentChildListUl1").css("display")=="block"){
         $("#icon6ForParentChildListUl1").css("display","none");
         $("#icon6ForParentChildList1").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon6ForParentChildListUl1").css("display","block");
         $("#icon6ForParentChildList1").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
 
   $("#icon2ForParentList").click(function(){
      if($("#icon2ForParentListUl").css("display")=="block"){
         $("#icon2ForParentListUl").css("display","none");
         $("#icon2ForParentList").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon2ForParentListUl").css("display","block");
         $("#icon2ForParentList").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#icon3ForParentList").click(function(){
      if($("#icon3ForParentListUl").css("display")=="block"){
         $("#icon3ForParentListUl").css("display","none");
         $("#icon3ForParentList").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon3ForParentListUl").css("display","block");
         $("#icon3ForParentList").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#icon4ForParentList").click(function(){
      if($("#icon4ForParentListUl").css("display")=="block"){
         $("#icon4ForParentListUl").css("display","none");
         $("#icon4ForParentList").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon4ForParentListUl").css("display","block");
         $("#icon4ForParentList").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#icon5ForParentList").click(function(){
      if($("#icon5ForParentListUl").css("display")=="block"){
         $("#icon5ForParentListUl").css("display","none");
         $("#icon5ForParentList").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon5ForParentListUl").css("display","block");
         $("#icon5ForParentList").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");

      }
   })
   $("#icon6ForParentList").click(function(){
      if($("#icon6ForParentListUl").css("display")=="block"){
         $("#icon6ForParentListUl").css("display","none");
         $("#icon6ForParentList").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon6ForParentListUl").css("display","block");
         $("#icon6ForParentList").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#iconleftOnBottomForMobile").click(function(){
      if($("#bottomForMobileList").css("display")=="block"){
         $("#bottomForMobileList").css("display","none");
         $("#iconleftOnBottomForMobile").removeClass("fa-solid fa-xmark").addClass("fa-solid fa-bars");
      }
      else{
         $("#bottomForMobileList").css("display","block");
         $("#iconleftOnBottomForMobile").removeClass("fa-solid fa-bars").addClass("fa-solid fa-xmark");
      }
   })
   $("#iconrightOnBottomForMobile").click(function(){
      if($("#bottomForMobileListRight").css("display")=="block"){
         $("#bottomForMobileListRight").css("display","none");
         $("#iconrightOnBottomForMobile").removeClass("fa-solid fa-xmark").addClass("fa-solid fa-bars");
      }
      else{
         $("#bottomForMobileListRight").css("display","block");
         $("#iconrightOnBottomForMobile").removeClass("fa-solid fa-bars").addClass("fa-solid fa-xmark");
      }
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
   $("#icon6ForParentListRight").click(function(){
      if($("#icon6ForParentListRightList").css("display")=="block"){
         $("#icon6ForParentListRightList").css("display","none");
         $("#icon6ForParentListRight").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon6ForParentListRightList").css("display","block");
         $("#icon6ForParentListRight").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#icon7ForParentListRight").click(function(){
      if($("#icon7ForParentListRightList").css("display")=="block"){
         $("#icon7ForParentListRightList").css("display","none");
         $("#icon7ForParentListRight").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon7ForParentListRightList").css("display","block");
         $("#icon7ForParentListRight").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#icon1ForParentListRight").click(function(){
      if($("#icon1ForParentListRightList").css("display")=="block"){
         $("#icon1ForParentListRightList").css("display","none");
         $("#icon1ForParentListRight").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon1ForParentListRightList").css("display","block");
         $("#icon1ForParentListRight").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#icon2ForParentListRight").click(function(){
      if($("#icon2ForParentListRightList").css("display")=="block"){
         $("#icon2ForParentListRightList").css("display","none");
         $("#icon2ForParentListRight").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon2ForParentListRightList").css("display","block");
         $("#icon2ForParentListRight").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#icon3ForParentListRight").click(function(){
      if($("#icon3ForParentListRightList").css("display")=="block"){
         $("#icon3ForParentListRightList").css("display","none");
         $("#icon3ForParentListRight").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon3ForParentListRightList").css("display","block");
         $("#icon3ForParentListRight").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#icon4ForParentListRight").click(function(){
      if($("#icon4ForParentListRightList").css("display")=="block"){
         $("#icon4ForParentListRightList").css("display","none");
         $("#icon4ForParentListRight").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon4ForParentListRightList").css("display","block");
         $("#icon4ForParentListRight").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#icon5ForParentListRight").click(function(){
      if($("#icon5ForParentListRightList").css("display")=="block"){
         $("#icon5ForParentListRightList").css("display","none");
         $("#icon5ForParentListRight").removeClass("fa-solid fa-angle-up").addClass("fa-solid fa-angle-down");
      }
      else{
         $("#icon5ForParentListRightList").css("display","block");
         $("#icon5ForParentListRight").removeClass("fa-solid fa-angle-down").addClass("fa-solid fa-angle-up");
      }
   })
   $("#overlay").click(function(){
      $("#overlay").css("display","none");
     
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
 
 

    


function openCity(evt, cityName) {
   // Declare all variables
   var i, tabcontent, tablinks;
 
   // Get all elements with class="tabcontent" and hide them
   tabcontent = document.getElementsByClassName("tabcontent");
   for (i = 0; i < tabcontent.length; i++) {
     tabcontent[i].style.display = "none";
   }
 
   // Get all elements with class="tablinks" and remove the class "active"
   tablinks = document.getElementsByClassName("tablinks");
   for (i = 0; i < tablinks.length; i++) {
     tablinks[i].className = tablinks[i].className.replace(" active", "");
   }
 
   // Show the current tab, and add an "active" class to the button that opened the tab
   document.getElementById(cityName).style.display = "block";
   evt.currentTarget.className += " active";
 }