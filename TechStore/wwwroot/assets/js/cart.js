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