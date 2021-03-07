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
            $("nav").addClass("white-nav-top");


            // Show dark logo
            $(".navbar-brand img").attr("src", "/Image/Home/logo blue.png");
        
        

        } else {

            // Hide white nav
            $("nav").removeClass("white-nav-top");
    

            // Show logo
            $(".navbar-brand img").attr("src", "/Image/Home/top-logo.png");
            
            

        }
    }
});

/* =========================================
              Mobile Menu
============================================ */
$(function () {

    // Show mobile nav
    $("#mobile-nav-open-btn").click(function () {
        $("#mobile-nav").css("height", "100%");
    });

    // Hide mobile nav
    $("#mobile-nav-close-btn, #mobile-nav a").click(function () {
        $("#mobile-nav").css("height", "0%");
    });

});