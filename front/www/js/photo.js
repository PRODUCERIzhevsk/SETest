var MyPhotos = function()
{
    var photos = ["images/1.jpg", "images/2.jpg"];

    function getRandomInt(min, max)
    {
        return Math.floor(Math.random() * (max - min + 1)) + min;
    }

    var GetRandomPhoto = function()
    {
        var randomIndex = getRandomInt(0,1);
        return photos[randomIndex];
    };

    var PHOTO_SHEMA =
        "<div class=\"row\">"+
            "<div class=\"col-sm-6 col-md-4\">"+
                "<div class=\"thumbnail\">"+
                    "<img src=\"#PhotoSrc#\" alt=\"#PhotoAlt#\">"+
                        "<div class=\"caption\">"+
                            "<h3>#PhotoName#</h3>"+
                            "<p>#PhotoInfo#</p>"+
                        "</div>"+
                    "</div>"+
                "</div>"+
            "</div>";

    var GRID_SHEMA =
    "<div class=\"row\">"+
        "<div class=\"col-md-4\">#Col1#</div>"+
        "<div class=\"col-md-4\">#Col2#</div>"+
        "<div class=\"col-md-4\">#Col3#</div>"+
    "</div>";

    var GetThumbnail = function (count)
    {
        var result = "";
        var index = 0;

        for(var i = 0; i < count; i++)
        {
            var grid = GRID_SHEMA;

            var PhotoSrc = GetRandomPhoto();
            var PhotoName = "Photo: " + index;

            var th = PHOTO_SHEMA;
            th = th.replace("#PhotoSrc#", PhotoSrc);
            th = th.replace("#PhotoAlt#", PhotoName);
            th = th.replace("#PhotoName#", PhotoName);
            th = th.replace("#PhotoInfo#", PhotoName);

            grid = grid.replace("#Col1#", th);
            index++;

            PhotoSrc = GetRandomPhoto();
            PhotoName = "Photo: " + index;

            th = PHOTO_SHEMA;
            th = th.replace("#PhotoSrc#", PhotoSrc);
            th = th.replace("#PhotoAlt#", PhotoName);
            th = th.replace("#PhotoName#", PhotoName);
            th = th.replace("#PhotoInfo#", PhotoName);

            grid = grid.replace("#Col2#", th);
            index++;

            PhotoSrc = GetRandomPhoto();
            PhotoName = "Photo: " + index;

            th = PHOTO_SHEMA;
            th = th.replace("#PhotoSrc#", PhotoSrc);
            th = th.replace("#PhotoAlt#", PhotoName);
            th = th.replace("#PhotoName#", PhotoName);
            th = th.replace("#PhotoInfo#", PhotoName);

            grid = grid.replace("#Col3#", th);
            index++;

            result += grid;
        }

        return result;
    };

    return {
        RandomPhoto: GetRandomPhoto,
        Thumbnail : GetThumbnail
    };
};