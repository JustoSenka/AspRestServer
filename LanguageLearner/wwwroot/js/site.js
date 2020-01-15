// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    highlightActiveMenuItem();
    /*
    $('ul.navbar-nav > li').click(function (e) {
        $('ul.navbar-nav > li').removeClass('font-weight-bold');
        $(this).addClass('font-weight-bold');
    });*/
});

highlightActiveMenuItem = function () {
    var url = window.location.pathname;

    var controller = "/" + url.split("/")[1]; // etc:  /controller/action

    $('.nav-item > a.nav-first[href="' + controller + '"]').addClass('font-weight-bold'); // Selects index actions by ctrl name
    $('.nav-item > a.nav-first[href="' + url + '"]').addClass('font-weight-bold'); // Selects non index first level nav items like Privacy

    $('.nav-item > a.nav-second[href="' + url + '"]').addClass('font-weight-bold'); // Selects second level nav fields by full url
};


// Searcheable dropdown
 //run when the DOM is ready
$(function () {                      
    // Reset input if clicking on dropdown again
    $('.searcheable-dropdown').click(function () {
        var input = $('.searcheable-dropdown input');
        input.val('');
        filterFunction(input);
    });
    // Filtering function
    $(".dropdown-menu input").keyup(function (e) {
        filterFunction($(this));
    });
    $(".dropdown-menu input").click(function (e) {  //use a class, since your ID gets mangled
        e.stopPropagation();   // do not close dropdown when clicking on search field
    });
});

function filterFunction(obj) { // obj: JQuery<HTMLElement>
    var filter = obj.val().toUpperCase();
    var links = obj.parent().children('a');

    for (var i = 0; i < links.length; i++) {
        txtValue = links[i].textContent || links[i].innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            links[i].style.display = "";
        } else {
            links[i].style.display = "none";
        }
    }
}