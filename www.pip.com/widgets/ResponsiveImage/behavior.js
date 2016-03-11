if (Ektron.PFWidgets === undefined) {
    Ektron.PFWidgets = {};
}

Ektron.PFWidgets.Image =
{

    parentID: "",
    webserviceURL: "/pagebuilder/widgets/Image/ImageHandler.ashx",

    imageFilter: "Thumbnail (*.jpg;*jpeg;*png;*.gif;*.png;*.bmp)|*.jpg;*.jpeg;*png;*.gif;*.png;*.bmp",

    Save: function(el) {

        if ($ektron(".hdnFolderId").val() < 0 || $ektron(".hdnContentId").val() <= 0) {
            alert("Please select an image to display");
            return false;
        }
        else {
            return true;
        }
    },

    toggleMediaManager: function (trigger) {
        var localparent = $ektron(trigger).closest('.ImageWidget'),
            mediaManager = localparent.find('.responsiveImageWidget-mediaManager'),
            imageInfo = localparent.find('.responsiveImageWidget-info'),
            saveButton = localparent.find('.CBSave'),
            cancelButton = localparent.find('.CBCancel'),
            toggleButton = localparent.find('.responsiveImageWidget-toggleMediaManager a'),
            folderId = localparent.find('.responsiveImageWidget-hdnFolderId').val(),
            mediaManagerPath = localparent.find('.responsiveImageWidget-hdnMediaManagerPath').val() + folderId;

        if (mediaManager.is(':visible')) {
            mediaManager.hide();
            imageInfo.fadeIn();
            saveButton.fadeIn();
            cancelButton.fadeIn();
            toggleButton.find('.ui-button-text').text('Select Image');
        }
        else {
            mediaManager.attr('src', mediaManagerPath);
            imageInfo.hide();
            saveButton.hide();
            cancelButton.hide();
            toggleButton.find('.ui-button-text').text('Return to Image Properties');
            mediaManager.fadeIn('slow');
        }
        return false;
    }
};

$ektron(document).on('mediaManagerValueInserted', function (event, data) {
    var mediaManager = $ektron('.responsiveImageWidget-mediaManager:visible'),
        localparent = mediaManager.closest('.ImageWidget'),
        imgThumb = localparent.find(".responsiveImageWidget-thumb");

    if (mediaManager.length > 0) { 
        if (imgThumb.length > 0) {
            imgThumb.attr('src', data.filename);
        }
        else {
            localparent.find(".responsiveImageWidget-thumbWrapper").html('<img class="responsiveImageWidget-thumb" src="' + data.filename + '" />');
        }
        localparent.find(".responsiveImageWidget-imageSource").html(data.filename.split("?")[0]);
        localparent.find(".responsiveImageWidget-hdnFolderId").val(data.folderId);
        localparent.find(".responsiveImageWidget-hdnContentId").val(data.contentId);

        Ektron.PFWidgets.Image.toggleMediaManager(mediaManager);
    }
});