# CRUDRecipeEF
## Console app project to practice CRUD with EF and relationships. 
*App is split up into a presentation layer (Console) and the data layer + business layer are in the same class library*

[UML](https://drive.google.com/file/d/1wK_0AYogciHfWensgVgt9arEkGfge9xd/view?usp=sharing)
#### The idea:
Recipes app (we can start it in a console app and then move it to either mvc or web api)

A recipe has a list of ingredients and a ingredient can belong to many recipes : many-to-many relationship
For that we could use data annotations
We can have a service that is the layer between Menu and the database (we call this service whenever we want to do stuff with our recipes)

When we have made that part - we can expand to making a restaurant. A restaurant has a menu - a menu has a list of recipes

This time we can also setup Dependency injection with appsettings.json (its not hard it will be almost the same)

This app will cover data annotations, CRUD, many to many, one to one, one to many, linq, dependency injection, services


