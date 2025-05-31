using CsvHelper;
using CsvHelper.Configuration;
using FlightQualityAnalysisAPI.Models;
using System.Globalization;

namespace FlightQualityAnalysisAPI.Services
{
    public class FlightService : IFlightService
    {
        public FlightService() { }
        /** Added GetAllFlightsDetails method to fetch all the details
        of the flights in ascending order from provided flight.csv file **/
        public IEnumerable<FlightDetails> GetAllFlightsDetails()
        {
            var _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Contents", "flights.csv");
            using var streamReader = new StreamReader(_filePath);
            using var csv = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            });
            return csv.GetRecords<FlightDetails>().OrderBy(f => f.id).ToList();
        }
        /** Logic comments
        Added GetInconsistentFlightsDetails method is to fetch the inconsistent flights details
        Condition is to fetch the all records of the flight.csv file and after that filter the aircraft registration number
        Then sorted the flights on the basis of the departure datetime as ascending order
        Finally take the previous flight data and the current flight data and
        if the arrival and departure airports are not matching then adding that flight details into the inconsistent flights details **/
        public IEnumerable<FlightDetails> GetInconsistentFlightsDetails()
        {
            var flightsDetails = GetAllFlightsDetails();
            var inconsistentFlightDetails = new List<FlightDetails>();

            var filterAircraftRegNo = flightsDetails.GroupBy(f => f.aircraft_registration_number);

            foreach (var filter in filterAircraftRegNo)
            {
                var sortedFlights = filter.OrderBy(f => f.departure_datetime).ToList();

                for (int i = 1; i < sortedFlights.Count; i++)
                {
                    var prevFlightData = sortedFlights[i - 1];
                    var currentFlightData = sortedFlights[i];

                    if (prevFlightData.arrival_airport != currentFlightData.departure_airport)
                    {
                        inconsistentFlightDetails.Add(currentFlightData);
                    }
                }
            }

            return inconsistentFlightDetails.OrderBy(f => f.id).ToList();
        }
    }
}
