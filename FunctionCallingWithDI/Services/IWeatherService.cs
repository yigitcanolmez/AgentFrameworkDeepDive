namespace FunctionCallingWithDI.Services;

public interface IWeatherService
{
    Task<string> GetWeatherReportAsync(string city);
}
