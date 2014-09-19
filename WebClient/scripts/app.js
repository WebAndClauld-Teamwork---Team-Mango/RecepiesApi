(function () {
    require.config({
        paths: {
            //libs
            'jquery' : 'libs/jquery-2.1.1.min',
            'sammy' : 'libs/sammy-latest.min',
            'handlebars':'libs/handlebars',
            'requestModule' : 'libs/requestModule',            
			'pubnub':'libs/pubnub',
            //controllers
            'mainController' : 'controllers/mainController',
            'recipesController':'controllers/recipesController',
            //configs
            'constants':'configs/constants',
            //persisters
            'recepiesPersister':'persisters/recepiesPersister',
            'tagsPersister':'persisters/tagsPersister',
            'usersPersister':'persisters/usersPersister',
            //entities
            'recipe':'entities/recipe',
            //helpers
            'restHelper':'helpers/restHelper',
            'requestModule':'helpers/requestModule',
            'fileHelper':'helpers/fileHelper'
        },
        shim: {
            
            'libs/underscore': {
                exports: '_'
            }
        }
    });

    require(['recepiesPersister',
        'usersPersister',
        'recipesController',
        'sammy',
        'recipe','restHelper','libs/underscore',
        'handlebars',
        'constants','pubnub'], 
        function (RecepiesPersister,UsersPersister,RecipesController,sammy,Recipe,RESThelper) {
        //enable cors for app
        $.support.cors=true;
        
		//
		var PUBNUB_demo = PUBNUB.init({
			publish_key: "pub-c-e9356020-7135-4618-b2fc-b9cac8473e7a",
			subscribe_key: "sub-c-f019d17c-3f3c-11e4-9bf1-02ee2ddab7fe"			
		});		
		
		//console.log(PUBNUB_demo);

		
        //main content containers
        var pageContent='#page-content';
        var contentSelector='#content-box';	
		var bigPageContent='#dedovia';
		//
		var logedUserKey='logedUser';
						
		var logedUserss=JSON.parse(localStorage.getItem(logedUserKey));
		
		PUBNUB_demo.subscribe({
		  channel: logedUserss.Nickname,
		  message: function(m){alert(m)}
		});
		
		
		function getLogedUser(){
			var logedUserKey='logedUser';					
			var logedUserss=JSON.parse(localStorage.getItem(logedUserKey));
			return logedUserss;
		}
		
		
		function createBreadCrum(name,href){
			return {
				'Name':name,
				'href':href
			};
		}
		
		function fixLoginBar()
		{
			var logedUserData=localStorage.getItem(logedUserKey);			
			var logedCondition=logedUserData!==null;
			var parsedData=JSON.parse(logedUserData);
			var logedInfo={
			'LogedIn':logedCondition,
			'Id':(logedCondition)?parsedData.Id:'',
			'Nickname':(logedCondition)?parsedData.Nickname:''
			};
			
			var recipesController=new RecipesController(RECEPIES_ENDPOINT);
			recipesController.generateLoginBar(logedInfo);
		}
		
		function hideBigPagePanel()
		{
			fixLoginBar();
			$(bigPageContent).hide();
			$(pageContent).show();
		}
		
		function loginUser(username,sessionKey)
		{
			localStorage.setItem(logedUserKey,JSON.stringify({
				'Nickname':username,
				'SessionKey':sessionKey
			}));
		}

		function logoutUser(username,password){
			localStorage.removeItem(logedUserKey);
		}
 		
		function goHome(){
			fixLoginBar();
			window.location.href='#/recipes';
		}
		
		function showBigPagePanel()
		{
			fixLoginBar();
			$(pageContent).hide();
			$(bigPageContent).show();			
		}
		
        function buildBreadCrums(breadcrums){
			var recepiesController=new RecipesController(RECEPIES_ENDPOINT);
			//
			var data={'breadCrums':breadcrums};
			recepiesController.generateBreadCrums(data);			
        }

        function loadUsers(onSuccess,onFail){
            var usersPersister=new UsersPersister(USERS_ENDPOINT);
                usersPersister.loadAllUsers(function(users){ 
                    if(onSuccess!==undefined)
                    {
                        onSuccess(users);
                    }                   
                },function(errors){
                    if(onFail!==undefined)
                    {
                        onFail(errors);
                    }
                });
        }

        function showRecipesPage(filterObj)
        {
            var recepiesController=new RecipesController(RECEPIES_ENDPOINT);                
            //
            loadUsers(function(users){
                //                    
                //console.log(data);     
                var recepiesPersister=new RecepiesPersister(RECEPIES_ENDPOINT);
                //
                recepiesController.generateUsersList({'users':users});
                //
                recepiesPersister.loadAllRecepies(function(results){
                    var readyResults=results;
                    //
					var breadCrums=[];
					
                    if(filterObj!==undefined)
                    {
                        //user filter
                        if(filterObj.userId!==undefined){
                            readyResults=_.filter(results,function(result){return(result.UserInfoId+''===''+filterObj.userId)});
							breadCrums.push(createBreadCrum('user/'+filterObj.userId,'#/recipes/user/'+filterObj.userId));
                        }
                        //search filter
                        if(filterObj.filterQuery!==undefined){
                            readyResults=_.filter(results,function(result){     
								var queryStr=filterObj.filterQuery;
								var nameCond=result.Name.indexOf(queryStr)>-1;
								var userCond=result.Nickname.indexOf(queryStr)>-1;
								var descCond=result.Description.indexOf(queryStr)>-1;
								return (nameCond || userCond || descCond);
							}); 
							//
							breadCrums.push(createBreadCrum('search for: '+filterObj.filterQuery,'#/recipes/filter/'+filterObj.filterQuery));						
                        }
                    }         
                    //
                    var data={'recepies':readyResults};           
                    recepiesController.generateThumbnails(data);      
					buildBreadCrums(breadCrums);	
                });
            }); 
        }

        var app = sammy(contentSelector, function() {

            //recepies page
            this.get("#/recipes", function() { 
				hideBigPagePanel();
                showRecipesPage();
            });

            //users filter
            this.get("#/recipes/user/:id",function(){
				hideBigPagePanel();
                var userId=this.params['id'];
                showRecipesPage({'userId':userId});
            });

            //search filter
            this.get("#/recipes/filter/:query",function(){
				hideBigPagePanel();
                var query=this.params['query'];
                showRecipesPage({'filterQuery':query});                
            });


            //show user info
            this.get("#/user/:id",function(){
                function fillUserInfoPage(data)
                {
                    loadUsers(function(users){
                        var recipesController=new RecipesController(RECEPIES_ENDPOINT);
                        //
                        recipesController.generateUsersList({'users':users});
                        recipesController.generateSingleUser(data);
                    });
                }

                var userId=this.params['id'];
                //
                var usersPersister=new UsersPersister(USERS_ENDPOINT);
                usersPersister.loadUser(userId,function(user){
                    var data={'user':user};
                    fillUserInfoPage(data);
					showBigPagePanel();
                },function(error){                    
                    //log errors here...
                    console.log(error);
                });
            });

			//add to favourite
			this.get('#/recipe/addtofavourite/:id',function(){
				
				var recipeId=this.params['id'];
				var logedUser=getLogedUser();
				
				function favouriteRecipe(pesho)
				{
					var rest=new RESThelper();
					console.log(pesho);
					console.log(logedUser);
					var nicknames=pesho.Nickname;
					var sessionKeys=logedUser.SessionKey;
					var id=pesho.Id;
					var root='http://mango.apphb.com/api/Favourites/';
					var url=root+'Add/?nickname='+nicknames+'&sessionKey='+sessionKeys;
					var data={
						'RecipeId':recipeId,
						'UserInfoId':id
					};
					var shit='application/x-www-form-urlencoded';
					//
					rest.postJSON(url,shit,data,function(responce){
						console.log(responce);
					},function(errors){
						console.log(errors);
					});					
				}
				
				function filterFunc(user)
				{
					return(user.Nickname==logedUser.Nickname);
				}
				
				var userPersister=new UsersPersister(USERS_ENDPOINT);
				userPersister.loadAllUsers(function(users){
					var logedUser=_.filter(users,filterFunc);
					favouriteRecipe(logedUser[0]);					
				});				
			});
			
            //show recipe
            this.get("#/recipe/:id",function(){
                hideBigPagePanel();
				//get recipe id
                var recipeId=this.params['id'];

                function loadRecipePage(recipeObj,users)
                {
                    $(contentSelector).load(RECIPE_PAGE,function(){          
                        //                              
                        var recipesController=new RecipesController(RECEPIES_ENDPOINT);
                        recipesController.generateUsersList({'users':users});
                        recipesController.generateSingleRecipe(recipeObj);
						//deal with breadCrums     
						var breadcrums=[];
						breadcrums.push(createBreadCrum('recipe/'+recipeId,'#/recipe/'+recipeId));
						buildBreadCrums(breadcrums);
                    });
                }

                var recepiesPersister=new RecepiesPersister(RECEPIES_ENDPOINT);
                //
                loadUsers(function(users){
                    //
                    recepiesPersister.loadRecipe(recipeId,function(data){                               
                        loadRecipePage(data,users);
                    },function(error){
                        //report errors here
                        console.log(error);
                    });                
                });
            });
            
            //login or register
            this.get("#/login",function(){
                loadUsers(function(users){
                    var recipesController=new RecipesController(RECEPIES_ENDPOINT);
                    //
                    recipesController.generateUsersList({'users':users});
					//
                    $(bigPageContent).load(LOGIN_REGISTER_PAGE,function(){
						showBigPagePanel();
						//
						$('#login-form').submit(function(){
							//login procedure here...
							var usersPersister=new UsersPersister(USERS_ENDPOINT);
							var $form=$(this);
							var nickname=$form.find('#nickname-input').first().val();
							var password=$form.find('#password-input').first().val();
							//
							usersPersister.loginUser(nickname,password,function(responce){								
								//save credentials								
								loginUser(nickname,responce);
								goHome();
							});	
							return false;
						});
						//register
						$('#register-form').submit(function(){							
							//login procedure here...
							var usersPersister=new UsersPersister(USERS_ENDPOINT);
							var $form=$(this);
							var nickname=$form.find('#nickname-input').first().val();
							var password=$form.find('#password-input').first().val();
							//
							usersPersister.registerUser(nickname,password,function(responce){								
								loginUser(nickname,responce+'');
								goHome();							
							});	
							
							return false;
						});
                    });
                });
            });
			
			this.get("#/logout",function(){
				//do logout	
				logoutUser();
				goHome();
			});

            //new recipe page
            this.get("#/recipes/new", function() {                     
                $(contentSelector).load(INSERT_RECIPE_PAGE,function(){                                        
                    //get recipe from rest by id                    
                });       
            });

            //about page
            this.get("#/about",function(){
                alert("about!!!");
                //              
            });                                            
        });

        //run application
        $(document).ready(function(){
        app.run("#/recipes");     
        });        

        //events   
        $('#search-submit').click(function(){
            var queryStr=$('#tb-search').val();
            if((queryStr+'').length>0)
            {
                window.location.href='#/recipes/filter/'+escape(queryStr);
            }
            else
            {
                window.location.href='#/recipes';
            }
        });  
    });
}());