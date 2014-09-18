define(['jquery'], function ($) {
    var FileHelper = (function () {

        function loadTextFile(filename,onSuccess,onError)
        {
            $.ajax({
                url : filename,
                dataType: "text",
                success : onSuccess,
                error:onError
            });
        }
        
        return {
            loadTextFile:loadTextFile
        };
    });
	
    return FileHelper;
});
