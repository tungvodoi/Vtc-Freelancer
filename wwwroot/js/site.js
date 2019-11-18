// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function Checkpass() {
  var a = document.getElementById("p1").value;
  var b = document.getElementById("p2").value;
  if (a !== b) {
    document.getElementById("error").innerHTML = "Invalid password";
    document.getElementById("but").disabled = true;
  } else {
    document.getElementById("error").innerHTML = "Valid Password";
    document.getElementById("but").disabled = false;
  }
}
