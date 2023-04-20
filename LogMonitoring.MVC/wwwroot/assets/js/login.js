onload = (event) => {
    setTimeout( function (){
        $("#lock-profile").fadeOut(400, function() {
            $(this).remove();
            $("#image-profile").fadeIn(350);
            $("#home-logout").fadeIn(350);
        });
    }, 1000)
};

document.getElementById("home-button").addEventListener("click", (event) => {
    window.location.href = "/home";
})

document.getElementById("logout-button").addEventListener("click", (event) => {
    window.location.href = "/Session/logout";
})