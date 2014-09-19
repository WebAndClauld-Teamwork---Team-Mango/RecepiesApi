(function () {
    require.config({
        paths: {
            //libs
            'jquery' : 'libs/jquery-2.1.1.min',
            'sammy' : 'libs/sammy-latest.min',
            'handlebars':'libs/handlebars',
            'requestModule' : 'libs/requestModule',            
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
        'recipe','libs/underscore',
        'handlebars',
        'constants'], 
        function (RecepiesPersister,UsersPersister,RecipesController,sammy,Recipe) {
        //enable cors for app
        $.support.cors=true;
        
        //main content containers
        var pageContent='#page-content';
        var contentSelector='#content-box';	
		var bigPageContent='#dedovia';
		//
		var logedUserKey='logedUser';
		
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
								loginUser(nickname,responce+'');
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