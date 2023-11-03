# TaskManagementSystem
 ### .NET and MySQL task for Takamul
<br>
To deploy the project please follow these steps:<br>

1- Please set up the database in your hosting or local machine, then import the database script provided in the DataBase folder.<br>

2- Change the connection string in the appsettings.json file by adjusting the username and password.<be>

3- You need to install the following dependencies: <br>
   MicrosoftAspNetCore.AuthenticationJwtBearer (7.0.13)<br>
   Microsoft.AspNetCore.OpenApi (7.0.13)<br>
   Pomelo.EntityFrameworkCore.MySql (7.0.0)<br>
   Swashbuckle.AspNetCore (6.5.0)<br>
   System.ldentityModel.TokensJwt (7.0.3)<br>

4- Open the project in Visual Studio 2022 then run the project to see the Swagger documentation.<br>

<br>

To utilize the system's APIs please follow these steps:<br>

1-Create an account by using the sign-up API.<br>

2-Login to your account using the credentials that were used to sign up.<br>

3-In the response body of the login API, you will find a field called "accessToken", copy the string in that field.<br>

4-On the top right corner of Swagger UI, you can see an "Authorize" button, click on it type Bearer in the input field, and then paste the access token.<br>

<br>

### Now you should be able to access the Tasks API fully.<br>

