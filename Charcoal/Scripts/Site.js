
//Enable dropdown with bootstrap
jQuery(function ($) {

    $('.dropdown-toggle').dropdown();

});

function DeleteStory(url,storyId) {

    var answer = confirm("Are you sure you want to delete this story?");
    if (answer) {
        $.ajax({
            url: url,
            type: "POST",
            data: 'storyId=' + storyId,
            success: function (data) {
                if (data == "success") {
                    $("#storyRow-" + storyId.toString()).remove();
                }
                else {
                    alert(data);
                }
            },
            error: function (xhr, textStatus, error) {
                alert('ajax:failure', [xhr, status, error]);
            }
        });
    }
}

function ToggleTaskStatus(url,taskId) {
    $.ajax({
        url: url,
        type: "POST",
        data: 'taskId=' + taskId,
        success: function (data) {
            $("#StoryTask-" + taskId.toString()).replaceWith(data);
        },
        error: function (xhr, textStatus, error) {
            alert(error, [xhr, status, error]);
        }
    });
}

function DeleteTask(url,taskId) {
    $.ajax({
        url: url,
        type: "POST",
        data: 'taskId=' + taskId,
        success: function (data) {
            if (data == "success") {
                $("#StoryTask-" + taskId.toString()).remove();
            }
            else {
                alert(data);
            }
        },
        error: function (xhr, textStatus, error) {
            alert(error, [xhr, status, error]);
        }
    });
}