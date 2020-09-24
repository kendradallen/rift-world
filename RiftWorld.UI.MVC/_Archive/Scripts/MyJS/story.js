function openStory(evt, storyID) {
    // Declare all variables
    var i, tabcontent, tablinks;

    // Get all elements with class="tabcontent" and hide them
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Get all elements with class="tablinks" and remove the class "active"
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // Show the current tab, and add an "active" class to the button that opened the tab
    document.getElementById(storyID).style.display = "block";
    evt.currentTarget.className += " active";
}
$(document).ready(function () {
    // Configure/customize these variables.
    var showChar = 300;  // How many characters are shown by default
    var ellipsestext = "...";
    var moretext = "Show more >";
    var lesstext = "Show less";


    $('.more').each(function () {
        var contentLil = $(this).children().first().text();
        var content2 = $(this).text();
        var content = $(this).html();
        //console.log(contentLil);
        if (content2.length > showChar) {

            var c = contentLil.substr(0, showChar);
            var h = contentLil.substr(showChar, content.length - showChar);

            var html = '<p>' + c + '<span class="moreellipses">' + ellipsestext + '</span></p><span class="morecontent"><span>' + h + '</span><span>' + content + '</span>&nbsp;&nbsp;<a href="" class="morelink">' + moretext + '</a></span>';
            $(this).html(html);
        }

    });

    $(".morelink").click(function () {
        if ($(this).hasClass("less")) {
            $(this).removeClass("less");
            $(this).html(moretext);
        } else {
            $(this).addClass("less");
            $(this).html(lesstext);
        }
        $(this).parent().prev().toggle();
        $(this).prev().toggle(500);
        return false;
    });
});

$(document).ready(function () {
    document.getElementById("defaultOpen").click();
});

