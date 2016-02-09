

$(function () {
    //LoadAllCourses
    var i=0;
    $.ajax({
        type: "POST",
        url: '/Course/DepartmentWiseCourse',
        contentType: "application/json; charset=utf-8",
        datatype: "json",
       // data: JSON.stringify({ id: 1 }),
        success: function (data) {
            
           // $("#course_dropdown").empty();
            $.each(data, function (key, value) {
                i++;
                
                $("#dept").prepend('<li id="a' + i + '"><a>'+value.Name+'</a></li>');
                $("#a" + i).append('<ul id="b' + i + '"></ul>');
                
                $.each(value.b, function (a, b) {
                    $("#b" + i).append('<li><a href="/Files/LoadFiles?id=' + b.CourseId + '">' + b.Name + '</a></li>');
                });
                //+ '<ul><li><a>hafijur</a></li><li><a>hafijur</a></li></ul></li>');


            });
            


        }


    });

    //$("a").hover(function(){

    //   var a= ($(this).attr("href"));
    //   var b = a.split("/");
    //   $(".jumbotron").html(b[b.length-1]);

    //});
    
});


