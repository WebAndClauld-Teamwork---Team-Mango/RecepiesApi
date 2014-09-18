define(['jquery','restHelper'/*dependencies*/], function ($,RESThelper) {
    var RecepiesPersister = (function (endpoint) {
        	
    		this.rest=new RESThelper();
        	this.endpoint=endpoint;
	        
	        function loadAllRecepies(){
	        	var url=endpoint+'/all';
	        	this.rest.getJSON(url,function(data){
	        		console.log(data);
	        	}, function(responce){
	        		console.log(responce);
	        	});
	        }               
	        
	        return {
	        	rest:this.rest,
	        	endpoint:this.endpoint,
	           	loadAllRecepies:loadAllRecepies
	        };
	    }

    );
	
    return RecepiesPersister;
});