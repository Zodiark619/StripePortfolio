Grand Archive E-Commerce using Stripe (ASP.NET MVC)

Web application built with ASP.NET Core MVC and Entity Framework Core that allows users to purchase items using Stripe payment gateway and manage card, user inventory, and orders — inspired by Grand Archive. Dataset gathered from https://index.gatcg.com.

Features

-CRUD Operations
Create, read, update, and delete Cards, Cart, Orders, History.
-Authentication & Authorization
Signed-in user can add, edit, and delete data.
Guests can only view data.
-Relational Data Modeling
Elements, Rarities, Sets, Subtypes, and Cardtypes belong to Cards.
-Responsive UI
Built with Razor views and Bootstrap.
-Data Validation & Security
Entity Framework Core handles database operations safely. Database used is SQLite. Identity is used to login and logout as user. Datatables.js, Sweetalert, and AJAX is used as external libraries.

How to Run Locally

-Clone the Repository
git clone https://github.com/Zodiark619/StripePortfolio.git
cd StripePortfolio
-Setup Database
dotnet ef database update
-Run the Application
dotnet run
-Access the App
Go to https://localhost:7237

Admin role isn't mandatory, but you must be registered and logged in to use and access the menu.

Stripe secretkey, publishablekey, and webhooksecret keys must be configured inside appsettings.json file. 
Stripe CLI also needed to be used on cmd with command 'stripe listen --forward-to https://localhost:7237/api/stripe/webhook' while purchasing