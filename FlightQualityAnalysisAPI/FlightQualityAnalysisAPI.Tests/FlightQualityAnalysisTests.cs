using FlightQualityAnalysisAPI.Models;
using FlightQualityAnalysisAPI.Services;

namespace FlightQualityAnalysisAPI.Tests;

public class FlightQualityAnalysisTests
{
    [Fact]
    public void CheckInconsistenciesFlightsDetails_Correct()
    {
        var fakeFlightService = new FakeFlightQualityAnalysisService();

        var flightInconsistencies = fakeFlightService.GetInconsistentFlightsDetails().ToList();

        Assert.Single(flightInconsistencies);
        Assert.Equal("AY120", flightInconsistencies[0].flight_number);
    }
    [Fact]
    public void CheckFlightsDetails_NoInconsistencies()
    {
        var fakeFlightService = new FakeFlightQualityAnalysisService_NoInconsistencies();

        var flightInconsistencies = fakeFlightService.GetInconsistentFlightsDetails().ToList();

        Assert.Empty(flightInconsistencies); // Should be empty since there are no inconsistencies
    }

    // Adding the Fake Flight Quality Analysis Class
    private class FakeFlightQualityAnalysisService : IFlightService
    {
        // Added test method for fetching the flight details
        public IEnumerable<FlightDetails> GetAllFlightsDetails()
        {
            return new List<FlightDetails>
            {
                new FlightDetails
                {
                    flight_number = "AY120",
                    aircraft_registration_number = "G-DIX",
                    departure_airport = "HEL",
                    arrival_airport = "OUL",
                    departure_datetime = DateTime.Parse("2024-01-30T14:00:00"),
                    arrival_datetime = DateTime.Parse("2024-01-30T15:30:00")
                },
                // Inconsistent flight detail
                new FlightDetails
                {
                    flight_number = "AY120",
                    aircraft_registration_number = "G-DIX",
                    departure_airport = "HEL",
                    arrival_airport = "OUL",
                    departure_datetime = DateTime.Parse("2024-01-30T17:00:00"),
                    arrival_datetime = DateTime.Parse("2024-01-01T18:30:00")
                }
            };
        }
        // Added test method for fetching the GetInconsistentFlightsDetails
        // Implemented logic is same as original GetInconsistentFlightsDetails method
        public IEnumerable<FlightDetails> GetInconsistentFlightsDetails()
        {
            return GetAllFlightsDetails().GroupBy(f => f.aircraft_registration_number)
                .SelectMany(g =>
                {
                    var sortedFlights = g.OrderBy(f => f.departure_datetime).ToList();
                    var inconsistentFlights = new List<FlightDetails>();
                    for (int i = 1; i < sortedFlights.Count; i++)
                    {
                        if (sortedFlights[i - 1].arrival_airport != sortedFlights[i].departure_airport)
                            inconsistentFlights.Add(sortedFlights[i]);
                    }
                    return inconsistentFlights;
                });
        }
    }
    private class FakeFlightQualityAnalysisService_NoInconsistencies : IFlightService
    {
        // Added test method for fetching the flight details
        public IEnumerable<FlightDetails> GetAllFlightsDetails()
        {
            return new List<FlightDetails>
            {
                new FlightDetails
                {
                    flight_number = "AY120",
                    aircraft_registration_number = "G-DIX",
                    departure_airport = "HEL",
                    arrival_airport = "OUL",
                    departure_datetime = DateTime.Parse("2024-01-30T08:00:00"),
                    arrival_datetime = DateTime.Parse("2024-01-30T09:30:00")
                },
                new FlightDetails
                {
                    flight_number = "AY120",
                    aircraft_registration_number = "G-DIX",
                    departure_airport = "OUL",
                    arrival_airport = "HEL",
                    departure_datetime = DateTime.Parse("2024-01-30T11:00:00"),
                    arrival_datetime = DateTime.Parse("2024-01-01T12:30:00")
                }
            };
        }
        // Added test method for fetching the GetInconsistentFlightsDetails
        // Implemented logic is same as original GetInconsistentFlightsDetails method
        public IEnumerable<FlightDetails> GetInconsistentFlightsDetails()
        {
            return GetAllFlightsDetails().GroupBy(f => f.aircraft_registration_number)
                .SelectMany(g =>
                {
                    var sortedFlights = g.OrderBy(f => f.departure_datetime).ToList();
                    var inconsistentFlights = new List<FlightDetails>();
                    for (int i = 1; i < sortedFlights.Count; i++)
                    {
                        if (sortedFlights[i - 1].arrival_airport != sortedFlights[i].departure_airport)
                            inconsistentFlights.Add(sortedFlights[i]);
                    }
                    return inconsistentFlights;
                });
        }
    }
}