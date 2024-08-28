Meeting Minutes App
A web application designed to help you capture and manage meeting minutes efficiently. This app allows you to record meeting details, manage meeting items, and track their statuses.

Table of Contents
Features
Technologies
Setup
Usage
API Endpoints
Contributing
License
Features
Capture Meetings: Record new meetings with details like date, time, and type.
Manage Items: Add and manage meeting items, including descriptions, due dates, responsible persons, and statuses.
View Meetings: Access details of captured meetings and their items.
Carry Over Items: Automatically carry over previous open items to new meetings.
Technologies
Frontend: React, Axios
Backend: ASP.NET Core, Entity Framework Core
Database: SQL Server
Setup
Prerequisites
Before you begin, ensure you have the following installed:

.NET 8 SDK
Node.js (for the frontend)
SQL Server
Backend Setup
Clone the Repository:

git clone https://github.com/yourusername/meeting-minutes-app.git
cd meeting-minutes-app

Navigate to the Backend Project Directory:

cd MeetingMinutesApp

Update the Connection String:

Open appsettings.json and update the connection string to point to your SQL Server instance:

"ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=MeetingMinutesApp;Trusted_Connection=True;MultipleActiveResultSets=true"
}

Run Database Migrations:

Apply the database migrations to set up the database schema:

dotnet ef database update

Run the Backend Application:

Start the backend server:

dotnet run

Frontend Setup
Navigate to the Frontend Project Directory:

cd MeetingMinutesApp/meetingminutesui

Install Dependencies:

Install the necessary npm packages:

npm install

Start the Frontend Application:

Launch the frontend development server:

npm start

Running the Application from Visual Studio
If you prefer using Visual Studio, you can simply open the solution file and click the "Run" button. This will start both the API and the frontend application.

Usage
Access the Application:

Open your browser and navigate to http://localhost:5228 to access the frontend application.

Capture and Manage Meetings:

Use the provided forms to capture new meetings and manage meeting items.

View Meeting Details:

Browse through the details of captured meetings and their items.

API Endpoints
Meeting Types
GET /api/meetings/meetingtypes: Retrieve all meeting types.
Meeting Item Status Types
GET /api/meetings/meetingitemstatustypes: Retrieve all meeting item status types.
Capture New Meeting
POST /api/meetings/captureNewMeeting: Capture a new meeting with its details and items.
Get Meeting Details
GET /api/meetings/getMeeting/{meetingId}: Retrieve details of a specific meeting by its ID.
Previous Open Items
GET /api/meetings/previous-open-items/{meetingTypeId}: Retrieve previous open items for a specific meeting type.
Update Meeting Item Status
PUT /api/meetings/meetingitems/{meetingItemId}/status: Update the status of an existing meeting item.
