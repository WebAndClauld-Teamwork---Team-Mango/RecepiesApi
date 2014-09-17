define(['jquery','rest_helper'/*dependencies*/], function ($,RESThelper) {
    var RecepiesPersister = (function (endpoint) {
        	
    		this.rest=new RESThelper();
        	this.endpoint=endpoint;
	        
	        function loadAllRecepies(){
	        	var url=endpoint+'/all';
	        	this.rest.getJSON(url,{}).success(function(data){
	        		console.log(data);
	        	}).error(function(responce){
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