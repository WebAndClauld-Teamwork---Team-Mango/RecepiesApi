define([/*dependencies*/], function () {
    var Recipe = (function (name,description) {

        this.name=name;
        this.description=description;
        
        return {
           name:this.name,
           description:this.description
        };
    });
	
    return Recipe;
});