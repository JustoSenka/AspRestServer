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