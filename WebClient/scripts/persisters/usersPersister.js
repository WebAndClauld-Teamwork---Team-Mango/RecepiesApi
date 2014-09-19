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

			function createUserObject(nickname,password){
				return {
					'Nickname':nickname,
					'AuthCode':nickname+password+''
				};				
			}
			
			function loginUser(nickname,password,onSuccess,onFail){
				var url=this.endpoint+'/login/';//http://mango.apphb.com/api/userinfo/register/
				//				
				this.rest.putJSON(url,'application/x-www-form-urlencoded',createUserObject(nickname,password),function(responce){
					if(onSuccess!==undefined){
						onSuccess(responce);
					}
				},function(errors){
					//report errors here!
					console.log(errors);
					if(onFail!==undefined){
						onFail(errors);
					}
				});
			}
			
			function registerUser(nickname,password,onSuccess,onFail){
				var url=this.endpoint+'/register/';//http://mango.apphb.com/api/userinfo/register/
				//				
				this.rest.postJSON(url,'application/x-www-form-urlencoded',createUserObject(nickname,password),function(responce){
					if(onSuccess!==undefined){
						onSuccess(responce);
					}
				},function(errors){
					//report errors here!
					console.log(errors);
					if(onFail!==undefined){
						onFail(errors);
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
				loginUser:loginUser,
				registerUser:registerUser,
	        	loadUser:loadUser,
        	 	loadAllUsers:loadAllUsers
	        };
	    }

    );
	
    return UsersPersister;
});