using FunctionCallingWithDI.Services;
using System.ComponentModel;

namespace FunctionCallingWithDI;

public class WeatherTools(IWeatherService weatherService)
{

    [Description("Şehire göre hava durumu bilgisi getirir.")]
    public async Task<string> GetWeatherAsync([Description("Kullanıcının hava durumu bilgisini istediği şehir. Örn. 'İstanbul', 'Ankara'...")] string city)
        => await weatherService.GetWeatherReportAsync(city);
}