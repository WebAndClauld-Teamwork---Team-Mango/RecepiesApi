define(['jquery','restHelper'/*dependencies*/], function ($,RESThelper) {
    var TagsPersister = (function (endpoint) {
        	
    		this.rest=new RESThelper();
        	this.endpoint=endpoint;
        	
        	function loadAllTags(onSuccess,onFail){
        		var url=this.endpoint+'/all';
        		this.rest.getJSON(url,function(data){
	        		//call user function
	    			if(onSuccess!==undefined){
	    				onSuccess(data);
	    			}
        		},function(responce){
        			//log errors here...
        			console.log(responce);
        			//call user function
        			if(onFail!==undefined){
        				onFail(responce);
        			}
        		});
        	}
	        
	        return {
	        	rest:this.rest,
	        	endpoint:this.endpoint,
	        	loadAllTags:loadAllTags
	        };
	    }

    );
	
    return TagsPersister;
});