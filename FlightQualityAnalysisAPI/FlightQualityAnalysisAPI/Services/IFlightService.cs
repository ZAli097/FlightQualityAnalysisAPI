using FlightQualityAnalysisAPI.Models;

namespace FlightQualityAnalysisAPI.Services
{
    public interface IFlightService
    {
        IEnumerable<FlightDetails> GetAllFlightsDetails();
        IEnumerable<FlightDetails> GetInconsistentFlightsDetails();
    }
}
