namespace FlightQualityAnalysisAPI.Models
{
    public class FlightDetails
    {
        // I have used snake_case naming convention to maintain the data
        // as .csv file columns are using snake_case convention
        // Otherwise I can use PascalCase or camelCase
        public int id { get; set; }
        public string aircraft_registration_number { get; set; }
        public int aircraft_type { get; set; }
        public string flight_number { get; set; }
        public string departure_airport { get; set; }
        public DateTime departure_datetime { get; set; }
        public string arrival_airport { get; set; }
        public DateTime arrival_datetime { get; set; }
    }
}
