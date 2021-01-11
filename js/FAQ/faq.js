/* =========================================
              Navigation
============================================ */



/* Show & Hide White Navigation */
$(function () {

    // show/hide nav on page load
    showHideNav();

    $(window).scroll(function () {

        // show/hide nav on window's scroll
        showHideNav();
    });

    function showHideNav() {

        if ($(window).scrollTop() > 75) {

            // Show white nav
            // $("nav").addClass("white-nav-top");


            // Show dark logo
            // $(".navbar-brand img").attr("src", "image/home/logo blue.png");
        
        

        } else {

            // Hide white nav
            // $("nav").removeClass("white-nav-top");
    

            // Show logo
            // $(".navbar-brand img").attr("src", "image/home/top-logo.png");
            
            

        }
    }
});
