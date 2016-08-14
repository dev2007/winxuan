var uploader;
var Team = {

    //get files in the team.
    FileList: function (teamId) {
        $.post("/Team/FileList", { teamId: teamId }, function (result) {
            $("#fileList").html(result);
        });
    },

    //file upload.
    UploaderInit: function () {
        uploader = WebUploader.create({

            // swf文件路径
            swf: '/Scripts/webuploader/Uploader.swf',

            // 文件接收服务端。
            server: '/File/Upload',

            // 选择文件的按钮。可选。
            // 内部根据当前运行是创建，可能是input元素，也可能是flash.
            pick: '#picker',

            // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
            resize: false
        });
        uploader.options.formData = {"teamid":$("#teamid").val()};

        //add file.
        uploader.on('fileQueued', function (file) {
            $("#thelist").append('<div id="' + file.id + '" class="item">' +
                '<h4 class="info" style="display:inline;">' + file.name + '</h4>' +
                '<div fileid="' + file.id + '" class="glyphicon glyphicon-remove" style="display:inline;margin-top:10px;margin-bottom:10px;margin-left:20px;margin-right:10px;"></div>' +
                 '<p class="state" style="display:inline;">等待上传...</p>' +
            '</div>');
            Team.BindRemoveFile();
        });

        //start upload event.
        uploader.on("startUpload", function () {
            $('#btnUploader').attr("disabled", true);
        });

        //uploading event.
        uploader.on('uploadProgress', function (file, percentage) {
            var $li = $('#' + file.id),
                $percent = $li.find('.progress .progress-bar');

            // avoid repeat create.
            if (!$percent.length) {
                $percent = $('<div class="progress progress-striped active">' +
                  '<div class="progress-bar" role="progressbar" style="width: 0%">' +
                  '</div>' +
                '</div>').appendTo($li).find('.progress-bar');
            }

            $li.find('p.state').text('上传中');

            $percent.css('width', percentage * 100 + '%');
        });

        //upload success event.
        uploader.on('uploadSuccess', function (file) {
            $('#' + file.id).find('p.state').text('已上传');
        });

        //upload error event.
        uploader.on('uploadError', function (file) {
            $('#' + file.id).find('p.state').text('上传出错');
        });

        //upload complete event.
        uploader.on('uploadComplete', function (file) {
            $('#' + file.id).find('.progress').fadeOut();
            $('#btnUploader').attr("disabled", false);
        });

        //start upload.
        $("#btnUploader").on("click", function () {
            if ($("#thelist").html() != "")
                uploader.upload();
        });
    },

    //remove added file.
    BindRemoveFile: function () {
        $("#thelist>div>div").on("click", function () {
            var fileId = $(this).attr("fileid");
            uploader.removeFile(fileId, true);
            $("#" + fileId).remove();
        });
    }
}

$(function () {
    Team.UploaderInit();

});