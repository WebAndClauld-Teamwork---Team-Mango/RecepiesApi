define(['jquery','restHelper'/*dependencies*/], function ($,RESThelper) {
    var UsersPersister = (function (endpoint) {
        	
    		this.rest=new RESThelper();
        	this.endpoint=endpoint;
        	
        	function loadUser(userId,onSuccess,onFail){
        		var url=this.endpoint+'/select/'+userId;
        		this.rest.getJSON(url,function(data){
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

			function loadAllUsers(onSuccess,onFail){
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
	        	loadUser:loadUser,
        	 	loadAllUsers:loadAllUsers
	        };
	    }

    );
	
    return UsersPersister;
});