 /*************\
*   DB Setup:   *
 \*************/

I have done the data storage for this project as code first. I have used EF for SQL. I used SQL express while building this project, but any version of MSSQL should work.
Below are the instructions to get the Database ready for the solution to run:

	1.	Replace the 'DefaultConnection' var in the 'appsettings.json' with your connection string.
	2.	Run 'Update-Database' from the Nuget pacage manager console, or 'dotnet ef database update' in the CLI, this should setup all the tables that are needed for the project.


add-Migration "MakingSureCodeFirstWorks"
