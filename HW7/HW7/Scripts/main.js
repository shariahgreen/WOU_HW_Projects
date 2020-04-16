
$(document).ready(function () {

    $(".request").click(function () {
        //var n = $("#count").val();
        //var owner = "wou-cs";
        //var repo = "CS460-F19-sgreen18";
        var commitInfo = this.name;
        var repo = commitInfo.split(",")[0];
        var owner = commitInfo.split(",")[1];

        console.log("Making a call...");
        console.log(repo);
        console.log(owner);

        var uri = "/Home/GithubCommitInfo/?owner=" + owner + "&repo=" + repo;

        $.ajax({
            type: "GET",
            dataType: "json",
            url: uri,
            success: displayData,
            error: errorOnAjax
        });
    });
});
function errorOnAjax() {
    console.log("Error in ajax request");
}

function displayData(data) {
    console.log(data);

    var tbl = document.getElementById('commits');
    tbl.innerHTML = "";
    
    var header = document.createElement("tr");
    var h1 = document.createElement("td");
    var h1Text = document.createTextNode("SHA");
    h1.appendChild(h1Text);
    header.appendChild(h1);
    var h2 = document.createElement("td");
    var h2Text = document.createTextNode("Commit Date");
    h2.appendChild(h2Text);
    header.appendChild(h2);
    var h3 = document.createElement("td");
    var h3Text = document.createTextNode("Commiter Name");
    h3.appendChild(h3Text);
    header.appendChild(h3);
    var h4 = document.createElement("td");
    var h4Text = document.createTextNode("Commit Message");
    h4.appendChild(h4Text);
    header.appendChild(h4);

    tbl.appendChild(header);

    for (var i = 0; i < data.length; i++) {
        console.log(data[0].charAt(232))
        //var commit = JSON.parse(data[i]);
        //console.log(data);
        console.log(data[i]);
        console.log(data[i]["name"]);
        //console.log(commit);
        //console.log(commit["name"]);

        var name = data[i].search("name");
        var url = data[i].search("url")
        var sha = data[i].search("sha");
        var date = data[i].search("date");
        var message = data[i].search("message");

        var shastr = data[i].substring(sha, url);
        var urlstr = data[i].substring(url, name);
        var namestr = data[i].substring(name, date);
        var datestr = data[i].substring(date, message);
        var messagestr = data[i].substring(message);

        var shavalue = shastr.split(":").join(",").split("\"").join(",").split(",")[3];
        var urlvalue = urlstr.split(":").join(",").split("\"").join(",").split(",")[3].concat(urlstr.split(":").join(",").split("\"").join(",").split(",")[4]);
        var namevalue = namestr.split(":").join(",").split("\"").join(",").split(" ").join(",").split(",")[4];
        var datevalue = datestr.split(":").join(",").split("\"").join(",").split(" ").join(",").split(",")[4];
        var messagevalue = messagestr.split(":").join(",").split("\"").join(",").split(",")[3];

        console.log(shavalue);
        console.log(urlvalue);
        console.log(namevalue);
        console.log(datevalue);
        console.log(messagevalue);

        var row = document.createElement("tr");

        var shatd = document.createElement("td");
        var shatdText = document.createTextNode(shavalue);
        var shaLink = document.createElement("a");
        shaLink.setAttribute("href", "" + urlvalue);
        shaLink.appendChild(shatdText);
        shatd.appendChild(shaLink);
        row.appendChild(shatd);

        var datetd = document.createElement("td");
        var datetdText = document.createTextNode(datevalue);
        datetd.appendChild(datetdText);
        row.appendChild(datetd);

        var nametd = document.createElement("td");
        var nametdText = document.createTextNode(namevalue);
        nametd.appendChild(nametdText);
        row.appendChild(nametd);

        var msgtd = document.createElement("td");
        var msgtdText = document.createTextNode(messagevalue);
        msgtd.appendChild(msgtdText);
        row.appendChild(msgtd);

        tbl.appendChild(row);
    }
}
