
//Enable dropdown with bootstrap
jQuery(function ($) {

    $('.dropdown-toggle').dropdown();

});

function DeleteStory(storyId) {

    var answer = confirm("Are you sure you want to delete this story?");
    if (answer) {
        $.ajax({
            url: "/Stories/DeleteStory",
            type: "DELETE",
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

function DeleteTask(taskId) {

    var answer = confirm("Are you sure you want to delete this task?");
    if (answer) {
        $.ajax({
            url: "/Stories/DeleteTask",
            type: "DELETE",
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
                alert('ajax:failure', [xhr, status, error]);
            }
        });
    }
}