var slideIndex = 1;
showSlides(slideIndex);

// Write your JavaScript code.
function plusSlides(n) {
  showSlides((slideIndex += n));
}

function currentSlide(n) {
  showSlides((slideIndex = n));
}
setInterval(() => {
  let a = 1;
  plusSlides(a);
  a += 1;
}, 2000);

function showSlides(n) {
  var i;
  var slides = document.getElementsByClassName("mySlides");
  var dots = document.getElementsByClassName("dot");
  if (n > slides.length) {
    slideIndex = 1;
  }
  if (n < 1) {
    slideIndex = slides.length;
  }
  for (i = 0; i < slides.length; i++) {
    slides[i].style.display = "none";
  }
  for (i = 0; i < dots.length; i++) {
    dots[i].className = dots[i].className.replace(" active", "");
  }
  slides[slideIndex - 1].style.display = "block";
  dots[slideIndex - 1].className += " active";
}

let btn = document.getElementsByClassName("btn-footer-link");
for (let i = 0; i < btn.length; i++) {
  btn[i].addEventListener("click", function() {
    var content = this.nextElementSibling;
    if (content.style.display === "block") {
      content.style.display = "none";
    } else {
      content.style.display = "block";
    }
  });
}

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

function getdeliveredTime(e, time) {
  e.preventDefault();
  var tablinks = document.getElementsByClassName("deliveredTime");
  for (i = 0; i < tablinks.length; i++) {
    tablinks[i].className = tablinks[i].className.replace(" sel", "");
    tablinks[i].className = tablinks[i].className.replace(" other", "");
  }
  e.target.classList.add("sel");
  document.getElementById("js-other-value").style.display = "none";
  document.getElementById("js-other-value").value = time;
}

function openCity(event, cityName) {
  var i;
  var x = document.getElementsByClassName("city");
  for (i = 0; i < x.length; i++) {
    x[i].style.display = "none";
  }
  document.getElementById(cityName).style.display = "block";
  var tablin = document.getElementsByClassName("w3-bar-item");
  for (let i = 0; i < tablin.length; i++) {
    tablin[i].className = tablin[i].className.replace(" brg", "");
  }
  event.currentTarget.className += " brg";
}

function Onscroll() {
  console.log("dcmmm");
  var price = document.getElementById("price-detail");
  if ((price.scrollTop() = 200)) {
    console.log(price.scrollTop() + "px");
  }
}

function InputOther(e) {
  e.preventDefault();
  var tablinks = document.getElementsByClassName("deliveredTime");
  for (i = 0; i < tablinks.length; i++) {
    tablinks[i].className = tablinks[i].className.replace(" sel", "");
  }
  e.target.classList.add("other");
  document.getElementById("js-other-value").style.display = "inline-block";
  document.getElementById("js-other-value").value = "";
}

function SetServiceIdToModal(ServiceId, UserId) {
  document.getElementById(
    "reportService"
  ).href = `/Gig/Report?UserId=${UserId}&ServiceId=${ServiceId}`;
}
