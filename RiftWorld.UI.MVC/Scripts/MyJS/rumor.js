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
