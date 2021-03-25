// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function Validate(obj) {
    if (obj.checked) {
        var item = document.getElementById('SignUp');
        item.disabled = false;
    } else {
        document.getElementById('SignUp').disabled = true;
    }
}