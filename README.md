# BingeBuddy

Binge Buddy is an app that allows uses to track what TV shows they watch, what episode they are currently on, and past shows that they have watched. 
It also allows for a list of potential shows to invenstigate earlier.

You will want to run the included SQL queries to build the database. There is also basic seed data for 105 shows in a seperate query. 

Login: Set up using Firebase. Users can register and login with a simple email and password authentication.

Site Experience:
Once a user logs in, they can see a list of options in the navigation bar to access the different categories of shows and add/edit/remove them as wanted.

## All Shows: 
This shows all shows that are current approved in the database. Only logged in admins can see the options for approved/unnapprove and the edit button to change the data
or approved status of each show. All shows created by non-admin users are by default set to not approved. 

![image](https://user-images.githubusercontent.com/93555687/168109438-fbceb7aa-cfae-44bb-970a-77ab54e546ca.png)


## My Shows: 
My Shows, Currently Watching, Caught Up, Completed, Lost Interest, and Potential are all lists of user tracked shows. When a new user tracked show is created,
a new object is created in the database that does not effect the shows primary Show object, which is available for everyone to access.

![image](https://user-images.githubusercontent.com/93555687/168110058-51d67312-e29b-4ec5-8ac5-8208e52af95a.png)

## Create New Show (My Shows):
This allows user input for their personal tracked show information. Last released data is user inputed since it can vary based on what platform you are watching that
particular show on. 

![image](https://user-images.githubusercontent.com/93555687/168110495-3a37b9d7-867d-4440-83aa-50d918851fb1.png)

## Deleting: 

Any user can delete their personal user tracked shows but only admins can delete/edit/approve that primary show objects. 

![image](https://user-images.githubusercontent.com/93555687/168111098-6f24cbf5-f0d0-48bd-8474-f54275d9b4fb.png)


## User Tab:

Only admins can see the user tab on the nav bar and edit/delete user profiles.

![image](https://user-images.githubusercontent.com/93555687/168111252-db72169e-aceb-42f1-ba4a-9eef09db22cf.png)


### Planned Features: 
 - Ability to sort lists based on different criteria. Right now, it defaults to alphabetical by show name.
 - Ability to select new show objects from the IMDB Api
