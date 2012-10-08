
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