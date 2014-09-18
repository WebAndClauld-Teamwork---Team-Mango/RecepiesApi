define(['jquery'], function ($) {
    var RESThelper = (function () {

        //define some functions here... 
        function doAJAXRequest(rtype,url,data,dataType){
            return $.ajax({
                type:rtype,
                url:url,
                data:data,
                //dataType:dataType//<-include later
            });
        }
        
        function getJSON(url,data)
        {
            var rtype='get';
            return doAJAXRequest(rtype,url,data,'application/json');
        }
        
        function postJSON(url,data)
        {
            var rtype='post';
            return doAJAXRequest(rtype,url,data,'application/json');
        }
        
        function putJSON(url,data)
        {
            var rtype='put';
            return doAJAXRequest(rtype,url,data,'application/json');
        }
        
        function deleteJSON(url,data)
        {
            var rtype='delete';
            return doAJAXRequest(rtype,url,data,'application/json');
        }
        
        return {
           doAJAXRequest:doAJAXRequest,
            getJSON:getJSON,
            postJSON:postJSON,
            putJSON:putJSON,
            deleteJSON:deleteJSON
        };
    });
	
    return RESThelper;
});
