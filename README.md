## The People Search Demo Application

### Business Requirements

- The application accepts search input in a text box and then displays in a pleasing style a list of people where any part of their first or last name matches what was typed in the search box (displaying at least name, address, age, interests, and a picture). 
- Solution should either seed data or provide a way to enter new users or both
- Simulate search being slow and have the UI gracefully handle the delay

### Technical Requirements
- An ASP.NET MVC Application 
- Use Ajax to respond to search request (no full page refresh) using JSON for both the request and the response
- Use Entity Framework Code First to talk to the database
- Unit Tests for appropriate parts of the application

## About the Solution
The application builds and runs using fresh installations of Microsoft Visual Studio Community 2017.

### Technology 
- HTML/CSS
- Bootstrap
- C#
- ASP.Net MVC
- Entity Framework
- Moq for Unit Testing

### Directory Structure

- **\PeopleSearchDemo\** - contains application project file
- **\PeopleSearchDemo.Tests\** - contains test project file
- **\Packages\** - contains all packages required to the application 
- **\PeopleSearchDemo\SeedData\** - contains sample xml file and image file for data seeding

### Setup and Application Build 

1. Clone the repository locally.  
2. Open **PeopleSearchDemo.sln** in Visual Studio
3. Set **PeopleSearchDemo** as the *Startup Project* 
4. Build (with NuGet package restore)  

### Database

The connection string is set to use **(localdb)\MSSQLLocalDB**. This should be automatically created when the user is created for the first time.  
The connection string can be changed in web.config file - just make sure the user has permissions to create database.

### Next Steps
- Authentication/Authorization
- Edit/Delete action
- Change architect to add web service and set up in farm to increase performance

### Test Data
The database is seeded with the following names:
- Harry Potter
- Hermione Granger
- Ron Weasley
- Albus Dumbledore
- Tom Riddle

### Usage
1. After launching, you'll be navigated to the "Search" page.
2. Click on the "New" in the navigation bar at the top of the screen.
3. Provide the user information and submit. System will create the database and load data from sample xml file when the database is initiated at first time.
4. Switch to the "Search" page. You can search with first names or last names. The search has been simulated to be slow randomly.

### Test Result
<div style="border-top:1px solid #AAA; border-bottom:1px solid #AAA">
     <a href="https://raw.githubusercontent.com/leotone/PeopleSearchDemo/master/TestResults/New-1.png" target="_blank">
      <img src="https://raw.githubusercontent.com/leotone/PeopleSearchDemo/master/TestResults/New-1.png" height="100" alt="new : layout" />
    </a>
    <a href="https://raw.githubusercontent.com/leotone/PeopleSearchDemo/master/TestResults/New-2.png" target="_blank">
      <img src="https://raw.githubusercontent.com/leotone/PeopleSearchDemo/master/TestResults/New-2.png" height="100" alt="new : warning message" />
    </a>
    <a href="https://raw.githubusercontent.com/leotone/PeopleSearchDemo/master/TestResults/New-3.png" target="_blank">
      <img src="https://raw.githubusercontent.com/leotone/PeopleSearchDemo/master/TestResults/New-3.png" height="100" alt="new : provide information" />
    </a>
    <a href="https://raw.githubusercontent.com/leotone/PeopleSearchDemo/master/TestResults/New-4.png" target="_blank">
      <img src="https://raw.githubusercontent.com/leotone/PeopleSearchDemo/master/TestResults/New-4.png" height="100" alt="new : add people" />
    </a>
     <a href="https://raw.githubusercontent.com/leotone/PeopleSearchDemo/master/TestResults/Search-1.png" target="_blank">
      <img src="https://raw.githubusercontent.com/leotone/PeopleSearchDemo/master/TestResults/Search-1.png" height="100" alt="search : layout" />
    </a>
    <a href="https://raw.githubusercontent.com/leotone/PeopleSearchDemo/master/TestResults/Search-2.png" target="_blank">
      <img src="https://raw.githubusercontent.com/leotone/PeopleSearchDemo/master/TestResults/Search-2.png" height="100" alt="search : result" />
    </a>

</div>







