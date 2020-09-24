
$("select").multi({
    enable_search: true,
    search_placeholder: "search..."
});

function ShowAll() {
    $(".selected-wrapper a").each(function () {
        var list = $(this).data("list");
        var id = $(this).data("value");
        $('div[data-value="' + id + '"][data-list="' + list + '"]').show();
    });
}

function HideAll() {
    $(".selected-wrapper a").each(function () {
        var list = $(this).data("list");
        var id = $(this).data("value");
        $('div[data-value="' + id + '"][data-list="' + list + '"]').hide();
    });
}

function on_click(thing, list) {
    $('div[data-value="' + thing + '"][data-list="' + list + '"]').toggle();
}

//Asso edits - validation tools 
const okButton = document.getElementById('daSubmit');
$(function () {
    $('.other-stuff input').keyup(function (e) {
        var type = $(this).attr("type");
        if (type == "number") {
            var max = 255;
            var value = $(this).val();
            if (value > max) {
                $(this).addClass("invalid");
            }
            else {
                $(this).removeClass("invalid");
            }
        }
        else if (type == "text") {
            var name = $(this).data('list') + "-" + $(this).data('name') + "-" + $(this).data('value');
            var max = $(this).data('max');
            var len = $(this).val().length;
            var char = max - len;
            $('#tc_' + name).html(char);
            if (char < 0) {
                $(this).addClass("invalid");
            }
            else {
                $(this).removeClass("invalid");
            }
        }

        CheckValidity();
        //var invalid = false;

        //disable submit if invalid
        //v2 - just the selected inputs
        //$('.other-stuff .yes-man input').each(function () {
        //    if ($(this).hasClass("invalid")) {
        //        invalid = true;
        //    }
        //});

        //v1 all inputs
        //$('.other-stuff input').each(function () {
        //    if ($(this).hasClass("invalid")) {
        //        invalid = true;
        //    }
        //});

        //if (invalid) {
        //    okButton.disabled = true;
        //}
        //else {
        //    okButton.disabled = false;
        //}
    });
});

function CheckValidity() {
    var invalid = false;
    $('.other-stuff .yes-man input').each(function () {
        if ($(this).hasClass("invalid")) {
            invalid = true;
        }
    });

    if (invalid) {
        okButton.disabled = true;
    }
    else {
        okButton.disabled = false;
    }

}
//gathering the data for submition
function GetDataPt(target, name, list) {
    var work = $('[data-list="' + list + '"][data-value="' + target + '"] [data-name="' + name + '"]').val();
    return work;
}

function IsChecked(target, name, list) {
    var work = $('[data-list="' + list + '"][data-value="' + target + '"] [data-name="' + name + '"]').is(":checked");
    return work;
}

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})

$("#tips").click(function (e) {
    e.preventDefault();
    $('#tipsModal').modal('show');
});
