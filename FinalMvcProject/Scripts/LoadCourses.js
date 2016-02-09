$(function () {
    $("#DepartmentId").change(function () {
        var dept = $("#DepartmentId").val();
        $("#Departments").load('@Url.Action("LoadCourses")', { id: dept });

    });
});