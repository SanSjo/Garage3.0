jQuery(document).ready(function () {

    /* replace the default browser scrollbar in the sidebar, in case the sidebar menu has a height that is bigger than the viewport */
    $('.sidebar').mCustomScrollbar({
        theme: "minimal-dark"
    });

    /*
        Wow
    */
    new WOW().init();
});