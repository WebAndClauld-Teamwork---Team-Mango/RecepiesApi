var templates=[
	recipe-thumb:"{{#recipes}}<div class="recipe-thumbnail-box">
                <img src="{{thumb_image}}" alt="{{thumb_image_alt}}"/>
                <strong>Image Title</strong>

                <p>{{recipe_desc}}</p>

                <div class="{{time_consuming}}">{{recipe_time}}</div>
                <a href="#/recipe/{{id}}"><button class="{{btn_details}}" >DETAILS</button></a>
            </div>{{/recipes}}",
];

[
	"recipes":[
	{
		'thumb_image':'',
		'thumb_image_alt':''
	},
	]
]