# AdminUI

## Project Setup

Open AdminUI.sln in Visual Studio

In the Package Manager Console, run the following commands:

	EntityFrameworkCore\Update-Database -Context AdminAccountContext
	
	EntityFrameworkCore\Update-Database -Context TimesheetContext

If you see an error along the lines of "Database xxx already exists, pick a different name",
then go to view -> Server Object Explorer, find the database in question, right click, then delete.
Now re-run the command and it should work.

## How to run

Compile with IISExpress

## What it does

This is a Web App that connects to an Azure DB that stores timesheets fron Multnomah County caretakers.
The app loads the timesheets from the database and displays them in a table sortable by each element.
The admin/user can then access each timesheet and process them by either approving or rejecting them.
