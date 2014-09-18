define(['jquery','restHelper'/*dependencies*/], function ($,RESThelper) {
    var UsersPersister = (function (endpoint) {
        	
    		this.rest=new RESThelper();
        	this.endpoint=endpoint;
        	
	        
	        return {
	        	rest:this.rest,
	        	endpoint:this.endpoint,
	        };
	    }

    );
	
    return UsersPersister;
});