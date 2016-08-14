var Basic = {

    //bind user's information.
    CheckUser: function () {
        $.post("/Home/CurrentUser", "", function (result) {
            if (!result.Status) {
                window.localtion.href = "/Home/Index";
            } else {
                $("#name").html(result.Data.UserName);
            }
        });
    },
    //load user's team information.
    loadTeam:function () {
        window.location.href = "/Team/List";
    }
};

$(function () {
    //login
    $("#loginbtn").on("click", function () {
        var userName = $("#username").val();
        var password = $("#password").val();
        if (userName == "") {
            $("#loginbtn").focus();
            alert("请输入用户名");
            return;
        } else if (password == "") {
            $("#password").focus();
            alert("请输入密码");
            return;
        }
        $.post("/Home/Login", { username: userName, password: password }, function (result) {
            var v = eval(result);
            if (!v.Status) {
                alert(v.ErrorMsg);
            } else {
                //TODO
                $("#loginform").hide();
                $("#forgetpwd").hide();
                $("#dashboard").show();
                $("#name").show();
                $("#name").text(v.Data.Name);
                Basic.loadTeam();
            }
        });
    });

    //registe
    $("#registe").on("click", function () {
        window.location.href = "/Home/Registe";
    });
});