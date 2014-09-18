define(['jquery','restHelper'/*dependencies*/], function ($,RESThelper) {
    var RecepiesPersister = (function (endpoint) {
        	
    		this.rest=new RESThelper();
        	this.endpoint=endpoint;
	        
        	function loadRecipe(id,onSuccess,onFail)
        	{        		
        		var url=endpoint+"/select/"+id;        		
        		//
        		this.rest.getJSON(url,function(data){
        			if(onSuccess!==undefined){
        				onSuccess(data);
        			}
        		},function(responce){
        			if(onFail!==undefined){
        				onFail(responce);
        			}
        		});
        	}

	        function loadAllRecepies(onSuccess,onFail){
	        	var url=endpoint+'/all';
	        	this.rest.getJSON(url,function(data){
	        		if(onSuccess!==undefined){
	        			onSuccess(data);
	        		}
	        	}, function(responce){
	        		//log error here...
	        		if(onFail!==undefined)
	        		onFail(responce);
	        		//console.log(responce);
	        	});
	        }               
	        
	        return {
	        	rest:this.rest,
	        	endpoint:this.endpoint,
	        	loadRecipe:loadRecipe,
	           	loadAllRecepies:loadAllRecepies
	        };
	    }

    );
	
    return RecepiesPersister;
});