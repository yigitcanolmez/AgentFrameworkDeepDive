namespace FunctionCallingWithDI.Services;

public class WeatherService : IWeatherService
{
    public async Task<string> GetWeatherReportAsync(string city)
    {
        await Task.Delay(500);
        return $"The weather in {city} is sunny with a high of 25°C.";
    }
}
