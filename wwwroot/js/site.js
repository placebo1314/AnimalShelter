const menu = document.getElementById('index_menu');
const nav = document.querySelector('nav');
const body = document.body;
const switcher = document.getElementById('themeSwitch');

menu.addEventListener('click', function (event){
    nav.classList.toggle('menu-active');
    menu.classList.toggle('fi-align-justify');
    menu.classList.toggle('fi-arrow-left');
});

switcher.addEventListener('click', function(event){
    body.classList.toggle('bg-dark');
    if(body.classList.contains('bg-dark')) {
        switcher.innerText = 'Activate Light Theme';
    } else {
        switcher.innerText = 'Activate Dark Theme';
        }
});

//animals page
var trs = document.getElementsByClassName("animaltr");
var modals = document.getElementsByClassName("modal");
for (var i = 0; i < trs.length; i++)
    trs[i].onclick = openModal;

function openModal(e) {
    var openedModal;
    for (var i = 0; i < modals.length; i++) {
        if (modals[i].id == e.currentTarget.id) {
            modals[i].style.display = "block";
            openedModal = modals[i];
            break;
        }
    }
    window.onclick = function (event) {
        if (event.target == openedModal || event.target.className == "close") {
            openedModal.style.display = "none";
        }
    }
}

const species = document.getElementById('species');
species.addEventListener('change', function (e) {
    window.location.href = "/Home/Animals?sortOrder=1&species=" + e.currentTarget.value;
});

const pageButtons = document.getElementsByClassName("page-button");

menu.addEventListener('', function (event) {
    nav.classList.toggle('menu-active');

});


//map:

//var map;

//function initialize() {
//    var latlng = new google.maps.LatLng(47.193445480491455, 19.514465109374996);
//    var myOptions = {
//        zoom: 7,
//        center: latlng,
//        mapTypeId: google.maps.MapTypeId.ROADMAP
//    };

//    map = new google.maps.Map(document.getElementById("terkep"), myOptions);
//    //google.maps.event.addDomListener(window, 'load', initialize);
//}

