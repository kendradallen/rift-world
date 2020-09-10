//rich text editor
tinymce.init({
    selector: '.rich',
    plugins: 'code advlist autolink lists link anchor paste help wordcount toc searchreplace visualchars hr fullscreen template visualblocks',
    menubar: 'edit view insert format tools help',
    toolbar: 'fullscreen | undo redo | removeformat | styleselect fontsizeselect | bold italic underline strikethrough | alignleft aligncenter alignright alignjustify | outdent indent | numlist bullist',
    browser_spellcheck: true, // added. Note: use Ctrl + right click to get to the spelling suggestions
    relative_urls: false, //do NOT change this!!!! At very least when testing locally this being true breaks all links made through editor
    document_base_url: "caerithein.com/",
    fontsize_formats: '12px 16px 18px 22px 30px 36px 48px',
    content_style: "@import url('https://fonts.googleapis.com/css2?family=Cinzel:wght@400;500;600&family=Grenze+Gotisch:wght@100;200;300;400;500;600;700;800;900&display=swap');html{font-size:22px;}body{color:darkslategrey;font-family: 'Grenze Gotisch', cursive; height: 100%;text-align: center; background-color:antiquewhite;}h1{font-size:40px;}h2{font-size:36px;}h3{font-size:30px;}h4{font-size:28px;}h5{font-size:24px;}h6{font-size:20px;}h1,h2,h3,h4,h5,h6{font-family:'Cinzel',serif;}",
    templates: [
        { title: "Pretty Bullet List", description: "how you will get a pretty list once it gets displayed in view", content: "<div class='row'><div class='col-auto offset-sm-1' style='text-align:left'><ul><li></li></ul></div></div><p></p>" },
        { title: "Pretty Ordered List", description: "how you will get a pretty list once it gets displayed in view", content: "<div class='row'><div class='col-auto offset-sm-1' style='text-align:left'><ol><li></li></ol></div></div><p></p>" },
        {title: "Get me before table of contents to be pretty", description: "help to make table of contents pretty", content: "<div class='row'><div class='col-auto text-left'><p>Put Table of Contents here, replacing this </p></div></div><p></p>"}
    ]
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

//preview images
function readURL(input, preview) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(preview).attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}



