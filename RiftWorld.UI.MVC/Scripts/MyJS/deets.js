const okButton = document.getElementById('daSubmit');
$('#RumorText').keyup(function (e) {
    var max = 300
    var len = $(this).val().length;
    var char = max - len;
    $('#tc-Rumor').html(char);

    if (char < 0) {
        $('#tcp-Rumor').addClass("invalid");
        okButton.disabled = true;
    }
    else {
        $('#tcp-Rumor').removeClass("invalid");
        okButton.disabled = false;
    }
});

//class RumorData {
//    constructor() {
//        this.RumorOfId = $('#RumorOfId').val();
//        this.RumorText = $('#RumorText').val();
//        this.__RequestVerificationToken = $('input[name="__RequestVerificationToken"]', $('#RumorCreateForm')).val();
//    }
//}

$("#RumorCreateForm").submit(function (e) {
    var formData = $(this).serializeArray();
    e.preventDefault;
    $("#MessageContentRu").html("<div class= 'alert alert-info'>Please Wait...</div>");
    //console.log(formData);
    //todo add little loading message
    $.ajax({
        url: "/Rumors/CreateRumor",
        type: "POST",
        data: formData,
        dataType: "json",
        error: function (e) {
            $("#MessageContentRu").html("<div class= 'alert alert-danger'>There was an error. Please try again or contact site administrator.</div>");
        },
        success: function (data) {
            $("#MessageContentRu").html("<div class= 'alert alert-success'>You've successfully started the seed of a rumor; let's see if it grows.<br/>(Your rumor is awaiting approval)</div>");

            $("#RumorCreateForm")[0].reset();
            $("#make-rumor").hide();
        }
    });
    return false;
});

$('#secret p').click(function () {
    $(this).addClass('revealed');
});

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
    $(evt.currentTarget).addClass("active");
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

//$(document).ready(function () {
//    if (document.getElementById("story")) {
//        document.getElementById("defaultOpen").click();
//    }
//});

