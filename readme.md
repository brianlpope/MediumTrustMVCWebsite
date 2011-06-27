# About This Project 
![Home Page](https://github.com/brianlpope/MediumTrustMVCWebsite/raw/master/img/homepage.png) 

This is a medium-trust website project that was done to try out several new   
Microsoft technologies.  It uses the Entity Framework 4.1 with a SQL Server CE  
backend.  This site was hosted at CrystalTech on a shared server.  MySQL was an  
option, but I wanted to see what the standalone CompactEdition database was like  
from a deployment and tooling standpoint.  The view engine used was Razor.  I  
used the repository pattern for data access from the controller so that I could pass  
in a mock database when unit testing.  Because the environment was medium-trust,  
I could not use an IOC which rely heavily on reflection.  Consequently, there is a  
reference to my data layer in my controller.  In hindsight, I should have created  
an interface to handle this appropriately.    