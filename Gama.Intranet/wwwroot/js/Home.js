
$(document).ready(function () {


    function LoadSideBar() {
        var SideBar = $('#SideBarOptions'); 
        var obj =
        {
            IdPage : 1,
            Name : "SideBarIndex"
        }
        $.ajax({
            type: "POST",
            url: getHostName() + "/ContentHtml/GetContentHtml",
            dataType: "json",
            contentType: "Application/json",
            data: JSON.stringify(obj),
            success: function (result) {
                if (result.status == 1) {

                    var html = "";
                    result.data.forEach(element => html += element.contenido);
                    SideBar.append(html);
                }
                else {
                    alert(result.message)
                }
            },
            error: function (err) {
                alert("error");
            }
        });
    }
    function LoadContentSideBar() {
        var SideBarContent = $('#v-pills-tabContent');
        var obj =
        {
            IdPage: 1,
            Name: "SideBarIndexContent"
        }
        $.ajax({
            type: "POST",
            url: getHostName() + "/ContentHtml/GetContentHtml",
            dataType: "json",
            contentType: "Application/json",
            data: JSON.stringify(obj),
            success: function (result) {
                if (result.status == 1) {

                    var html = "";
                    result.data.forEach(element => html += element.contenido);
                    SideBarContent.append(html);
                }
                else {
                    alert(result.message)
                }
            },
            error: function (err) {
                alert("error");
            }
        });
    }
    function LoadGenericContentHtml() {
        var dynamic_container = $('#dynamic-container');
        var obj =
        {
            IdPage: 1,
            Name: "ContentIndex"
        }
        $.ajax({
            type: "POST",
            url: getHostName() + "/ContentHtml/GetContentHtml",
            dataType: "json",
            contentType: "Application/json",
            data: JSON.stringify(obj),
            success: function (result) {
                if (result.status == 1) {

                    var html = "";
                    result.data.forEach(element => html += element.contenido);
                    dynamic_container.append(html);
                }
                else {
                    alert(result.message)
                }
            },
            error: function (err) {
                alert("error");
            }
        });
    }

    // Load SideBar
    LoadSideBar();
    LoadContentSideBar();
    LoadGenericContentHtml();

});