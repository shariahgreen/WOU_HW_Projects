$("#team").click(function () {
    var n = $("#teamID").val();
    var uri = "/Results/getAthleteByTeamID?Team=" + n;
    $.ajax({
        type: "GET",
        dataType: "json",
        url: uri,
        success: createDropdown,
        error: errorOnAjax
    });
});

$("#athlete").click(function () {
    var n = $("#athleteID").val();
    var m = $("#raceID").val();
    var uri = "/Results/GetResultsByAthlete?Athlete=" + n + "&Race=" + m;
    $.ajax({
        type: "GET",
        dataType: "json",
        url: uri,
        success: displayData,
        error: errorOnAjax
    });
});

function errorOnAjax() {
    console.log("ERROR in ajax request.");
}

function createDropdown(data) {

    var markup = "<option value='0'>Select Athlete</option>";
    for (var x = 0; x < data.length; x++) {
        markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
    }
    $("#athleteID").html(markup).show();
    console.log(data);
}

function displayData(data) {
    console.log(data);
    var markup = "";
    var X = [];
    var Y = [];
    if (data == []) { markup += "<p>No race results for this athlete</p>"; }
    console.log(markup);
    for (var x = 0; x < data.length; x++) {
        X.push(data[x].num);
        Y.push(data[x].raceTime);
        console.log(data[x].meetDate + " " + data[x].meetLocation + " " + data[x].raceTime);
        markup += "<tr><td>" + data[x].meetDate.toString() + "</td>";
        markup += "<td>" + data[x].meetLocation + "</td>";
        markup += "<td>" + data[x].raceTime + "</td></tr>";
    }
    $("#resultstable").html(markup).show();

    var trace = {
        x: X,
        y: Y,
        mode: 'lines',
        type: 'scatter'
    };
    var plotData = [trace];
    var layout = {};
    Plotly.newPlot('RaceResults', plotData, layout);
}