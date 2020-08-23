//rich text editor
tinymce.init({
    selector: '.rich',
    plugins: 'code advlist autolink lists link anchor paste help',
    toolbar: 'undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | outdent indent',
    browser_spellcheck: true, // added. Note: use Ctrl + right click to get to the spelling suggestions
    relative_urls: false //do NOT change this!!!! At very least when testing locally this being true breaks all links made through editor
});



//character counter (for non-asso)
$('.count').keyup(function (e) {
    var name = $(this).attr('id');
    var max = $(this).data('max');
    var len = $(this).val().length;
    var char = max - len;
    $('#tc-' + name).html(char);
    if (char < 0) {
        $('#tcp-' + name).addClass('invalid');
    }
    else {
        $('#tcp-' + name).removeClass('invalid');
    }
});


//confirm modal
function ConfirmModal(nullables) {
    //get what areas are empty currently
    var nullables = nullables;
    var nuliLength = nullables.length;
    var theNulled = [];
    for (var i = 0; i < nuliLength; i++) {
        var nuli = nullables[i];
        var checkThis = tinymce.get(nuli).getContent();
        if (checkThis == "") {
            theNulled.push(nuli);
        }
        //console.log(checkThis.length); <--- test for seperate sheet for getting character count on rich editor. Good news, it works and counts the full character count with the html.
    };
    //fill modal's content
    $(function () {
        const list = $('.modal-body #nulled');
        list.html("");
        list.append('<ul></ul>');
        const list2 = $('.modal-body #nulled ul');
        $(theNulled).each(function () {
            list2.append('<li>' + this + '</li>');
        });
        if (theNulled.length > 0) {
            list.append('<p> all have no value.</p><p>Click publish if this is okay.</p>');
        }
    })
};

//edit quick navs
$('.quick a').on('click', function (e) {

    // Define variable of the clicked »a« element (»this«) and get its href value.
    var href = $(this).attr('href');

    // Run a scroll animation to the position of the element which has the same id like the href value.
    $('html, body').animate({
        scrollTop: $(href).offset().top
    }, '300');

    // Prevent the browser from showing the attribute name of the clicked link in the address bar
    e.preventDefault();

});


