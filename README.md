# FlightQualityAnalysisAPI

# Overview
This project provides a RESTful API built with ASP.NET Core (.NET 8) that automates the detection of inconsistencies in aircraft flight records. The goal is to support airlines in improving operational efficiency by eliminating the need for manual data validation processes (previously done via Excel).

# Features
•	Analyse flight data records and automatically detect inconsistencies
•	Clean and structured API endpoints for integration
•	Unit tests included using a fake flight data service for testing core validation logic
•	Designed for extensibility and easy integration with airline data systems

# Inconsistency Detection Logic
The API checks for data anomalies such as:
•	Logical errors in flight times (e.g., arrival before departure)

# Example of Detected Inconsistency
An example of an inconsistent flight record found by the API:
[
{
    "id": 997,
    "aircraft_registration_number": "G-DIX",
    "aircraft_type": 320,
    "flight_number": "AY120",
    "departure_airport": "HEL",
    "departure_datetime": "2024-01-30T17:00:00",
    "arrival_airport": "OUL",
    "arrival_datetime": "2024-01-30T18:30:00"
}
]

# Technology Stack
•	.NET 8
•	ASP.NET Core Web API
•	xUnit for unit testing
•	Custom fake service class for mocking data

# Getting Started
1.	Clone the repository
2.	Restore dependencies using dotnet restore
3.	Run the API using dotnet run
4.	Run tests using dotnet test
