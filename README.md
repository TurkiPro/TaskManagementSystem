# TaskManagementSystem
 .NET and MySQL task for Takamul

To deploy the project please follow these steps:
1- Please set up the database in your hosting or local machine, then import the database script provided in the DataBase folder.
2- Change the connection string in the appsettings.json file by adjusting the username and password.
3- Open the project in Visual Studio 2022 then run the project to see the Swagger documentation.

To utilize the system's APIs please follow these steps:
1-Create an account by using the sign-up API.
2-Login to your account using the credentials that were used to sign up.
3-In the response body of the login API, you will find a field called "accessToken", copy the string in that field.
4-On the top right corner of Swagger UI, you can see an "Authorize" button, click on it type Bearer in the input field, and then paste the access token.

Now you should be able to access the Tasks API fully.
