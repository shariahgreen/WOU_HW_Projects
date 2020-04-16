var ajax_call = function () {
    console.log("Calling AJAX");
    var n = document.getElementById("eventtitle").innerHTML;
    var m = parseInt(n);
    console.log(m);
    var uri = "/RSVPs/GetRSVP?Event=" + m;
    $.ajax({
        type: "GET",
        dataType: "JSON",
        url: uri,
        success: displayData,
        error: errorOnAjax
    });
};

var interval = 1000 * 5;
window.setInterval(ajax_call, interval);

function errorOnAjax() {
    console.log("ERROR in ajax request.");
}

function displayData(data) {
    console.log("success");
    var num = data.length;
    console.log(num);
    var x = document.getElementById("rsvp_num");
    x.innerHTML = data.length;
}