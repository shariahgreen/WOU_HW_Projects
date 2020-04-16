var times = ["08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "01:00", "01:30", "02:00", "02:30", "03:00", "03:30", "04:00", "04:30", "05:00", "05:30"]

$('#scheduleForm').on("submit", function(event){
    event.preventDefault();
    var mon = $('#monday').val();
    var tue = $('#tuesday').val();
    var wed = $('#wednesday').val();
    var thu = $('#thursday').val();
    var fri = $('#friday').val();
    var sat = $('#saturday').val();
    var sun = $('#sunday').val();

    var pattern = /[0-1][0-9]:(0|3){2}-[0-1][0-9]:(0|3){2}/
    
    if (mon==""){}
    else if (!pattern.test(mon)) //check if pattern matches the regex form for the input
    {
        console.log("Time string invalid: " + mon);
        $("#monday").val("Please enter a valid time");
    }
    else
    {
        var eventHours = mon.split('-'); //mon = "08:00-09:00"
        var start = times.indexOf(eventHours[0]) + 2; //start = "08:00" = 2
        var end = times.indexOf(eventHours[1]) + 2; //end = "09:00" = 4
    
        var i = 1; //table row and column indexing starts at 1
        for (time in times) 
        {
            i++; //first row and column in table are the headers
            if (i >= start && i <= end) //if the time is between the start and end times, update table
            {
                $('table#sched tr:nth-child(' + i + ') td:nth-child(2)').css("background-color", "lightblue");
            }
            else
            {
                continue;
            }
        }
    }

    if (tue==""){}
    else if (!pattern.test(tue))
    {
        console.log("Time string invalid: " + tue);
        $("#tuesday").val("Please enter a valid time");
    }
    else
    {
        var eventHours = tue.split('-');
        var start = times.indexOf(eventHours[0]) + 2;
        var end = times.indexOf(eventHours[1]) + 2;
    
        var i = 1;
        for (time in times) 
        {
            i++;
            console.log("i = " + i + ", start = " + start + "end = " + end)
            if (i >= start && i <= end)
            {
                $('table#sched tr:nth-child(' + i + ') td:nth-child(3)').css("background-color", "lightblue");
            }
            else
            {
                continue;
            }
        }
    }

    if (wed==""){}
    else if (!pattern.test(wed))
    {
        console.log("Time string invalid: " + wed);
        $("#wednesday").val("Please enter a valid time");
    }
    else
    {
        var eventHours = wed.split('-');
        var start = times.indexOf(eventHours[0]) + 2;
        var end = times.indexOf(eventHours[1]) + 2;
    
        var i = 1;
        for (time in times) 
        {
            i++;
            console.log("i = " + i + ", start = " + start + "end = " + end)
            if (i >= start && i <= end)
            {
                $('table#sched tr:nth-child(' + i + ') td:nth-child(4)').css("background-color", "lightblue");
            }
            else
            {
                continue;
            }
        }
    }

    if (thu==""){}
    else if (!pattern.test(thu))
    {
        console.log("Time string invalid: " + thu);
        $("#thursday").val("Please enter a valid time");
    }
    else
    {
        var eventHours = thu.split('-');
        var start = times.indexOf(eventHours[0]) + 2;
        var end = times.indexOf(eventHours[1]) + 2;
    
        var i = 1;
        for (time in times) 
        {
            i++;
            console.log("i = " + i + ", start = " + start + "end = " + end)
            if (i >= start && i <= end)
            {
                $('table#sched tr:nth-child(' + i + ') td:nth-child(5)').css("background-color", "lightblue");
            }
            else
            {
                continue;
            }
        }
    }

    if (fri==""){}
    else if (!pattern.test(fri))
    {
        console.log("Time string invalid: " + fri);
        $("#friday").val("Please enter a valid time");
    }
    else
    {
        var eventHours = fri.split('-');
        var start = times.indexOf(eventHours[0]) + 2;
        var end = times.indexOf(eventHours[1]) + 2;
    
        var i = 1;
        for (time in times) 
        {
            i++;
            console.log("i = " + i + ", start = " + start + "end = " + end)
            if (i >= start && i <= end)
            {
                $('table#sched tr:nth-child(' + i + ') td:nth-child(6)').css("background-color", "lightblue");
            }
            else
            {
                continue;
            }
        }
    }

    if (sat==""){}
    else if (!pattern.test(sat))
    {
        console.log("Time string invalid: " + sat);
        $("#saturday").val("Please enter a valid time");
    }
    else
    {
        var eventHours = sat.split('-');
        var start = times.indexOf(eventHours[0]) + 2;
        var end = times.indexOf(eventHours[1]) + 2;
    
        var i = 1;
        for (time in times) 
        {
            i++;
            console.log("i = " + i + ", start = " + start + "end = " + end)
            if (i >= start && i <= end)
            {
                $('table#sched tr:nth-child(' + i + ') td:nth-child(7)').css("background-color", "lightblue");
            }
            else
            {
                continue;
            }
        }
    }

    if (sun==""){}
    else if (!pattern.test(sun))
    {
        console.log("Time string invalid: " + sun);
        $("#sunday").val("Please enter a valid time");
    }
    else
    {
        var eventHours = sun.split('-');
        var start = times.indexOf(eventHours[0]) + 2;
        var end = times.indexOf(eventHours[1]) + 2;
    
        var i = 1;
        for (time in times) 
        {
            i++;
            console.log("i = " + i + ", start = " + start + "end = " + end)
            if (i >= start && i <= end)
            {
                $('table#sched tr:nth-child(' + i + ') td:nth-child(8)').css("background-color", "lightblue");
            }
            else
            {
                continue;
            }
        }
    }
});