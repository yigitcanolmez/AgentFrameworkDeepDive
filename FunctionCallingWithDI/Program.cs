using System;

//using FunctionCallingWithDI; // For WeatherTools class
//using FunctionCallingWithDI.Services; // Weather service interface
//using Microsoft.Agents.AI;
//using Microsoft.Extensions.AI;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection; // DI
//using OpenAI;
//using System.ClientModel;

//var configuration = new ConfigurationBuilder()
//    .AddUserSecrets<Program>()
//    .AddEnvironmentVariables()
//    .Build();

//string uri = "https://models.github.ai/inference";
//string ghPat = configuration["GH_PAT"];

//var services = new ServiceCollection();

//services.AddScoped<IWeatherService, WeatherService>();
//services.AddScoped<WeatherTools>(); // ileride bu kodların dinamik olarak kayıt edilebilmesi adına çok güzel reflection tabanlı bir mekanizma paylaşacağım.

//var serviceProvider = services.BuildServiceProvider();

//// Sanki constructor injection yapılıyormuş gibi davranıyoruz.
//var weatherTools = serviceProvider.GetRequiredService<WeatherTools>();

//// Chat options amacı her bir tetikleme sonrasında hangi ayarlar kullanılacak bilgisi vermek: Burada fonksiyon çağrılarının yapılabilmesi için gerekli araçları (tools) ekliyoruz. ilerleyen bölümlerde LLM'i de buradan konfigure edebileceğiz.

//AIAgent agent = new OpenAIClient(new ApiKeyCredential(ghPat),
//                                 new OpenAIClientOptions { Endpoint = new Uri(uri) })
//    .GetChatClient("gpt-4o")
//    .CreateAIAgent(instructions: "Sen 5 yaşında bir çocuksun, bol hayal gücüyle " +
//    "ufuk açabilecek yanıtlar ile beraber konuşuyorsun.");


//// tools dizisi içerisine istediğimiz kadar method ekleyebiliriz.
//// Bilerek DI örneği verdim ki, gerçek hayatta fonksiyon çağırma senaryolarında bu tarz servislerin kullanımı çok daha yaygın olacaktır.
//// kendi dokümanında statik kod örneği üzerinden testler yapılmaktaydı :)
//var chatOptions = new ChatOptions()
//{
//    Tools = [AIFunctionFactory.Create(weatherTools.GetWeatherAsync)]
//};


//// istersek bu tanımı doğrudan agent oluşturulurken de verebiliriz. ama ben burada ayrı tutmayı tercih ettim. Neden? Her bir çağrıda, DI tarafında üst üste binmeyelim :)
//// statik bir metodumuz olsaydı o zaman direkt olarak agent tanımında, 
//     //.CreateAIAgent(instructions: "You are a helpful assistant", tools: [AIFunctionFactory.Create(GetWeather)]); şeklinde çağırabilirdik.

//Console.WriteLine(await agent.RunAsync("Selam, bugün Çanakkale'de hava nasıl?",
//                          options: new ChatClientAgentRunOptions(chatOptions)));

internal class Program
{
    static void Main(string[] args)
    {
        // Örnek kodlar yorum satırı halinde tutuluyor. Bu placeholder derlemeyi başarılı kılar.
        Console.WriteLine("FunctionCallingWithDI örnek projesi placeholder giriş noktası.");
    }
}

