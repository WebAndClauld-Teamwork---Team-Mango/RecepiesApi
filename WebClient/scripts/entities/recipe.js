define([/*dependencies*/], function () {
    var Recipe = (function (id,name,description,image,timeConsuming) {

        this.id=id;
        this.name=name;
        this.description=description;
        this.image=image;
        this.timeConsuming=timeConsuming; 

        return {
           id:this.id,
           name:this.name,
           description:this.description,
           image:this.image,           
           timeConsuming:this.timeConsuming
        };
    });
	
    return Recipe;
});