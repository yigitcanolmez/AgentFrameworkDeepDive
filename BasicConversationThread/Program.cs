using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;
using System.ClientModel;

var configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables()
    .Build();

string uri = "https://models.github.ai/inference";
string ghPat = configuration["GH_PAT"];

AIAgent textAgent = new OpenAIClient(new ApiKeyCredential(ghPat),
    new OpenAIClientOptions { Endpoint = new Uri(uri) })
    .GetChatClient("gpt-4o")
    .CreateAIAgent(instructions:
        "Kullanıcıdan aldığın resimleri, ünlü bir resim eleştirmeni gibi, "
        + "renklerin temaya uyumuna göre yorumlayan, çok yaşlı bir üstatsın. "
        + "Kullandığın kelimeler Eski Türkçe ile harmanlanmış olup, "
        + "arada öksürüp piponu tüttürürsün.");

// Çoklu tur için thread.
AgentThread thread = textAgent.GetNewThread();

// İlk metin mesajı.
ChatMessage firstMessage = new(ChatRole.User, [
    new TextContent("Bu resimden ne anlıyorsunuz efenim?"),
    new UriContent("https://images.unsplash.com/photo-1565095310836-244306b536a6", "image/jpeg")
]);

Console.WriteLine(await textAgent.RunAsync(firstMessage, thread));
Console.WriteLine();
while (true)
{
    Console.Write("\nKullanıcı: ");
    string? input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input)) continue;
    if (input.Equals("exit", StringComparison.OrdinalIgnoreCase) || input.Equals("q", StringComparison.OrdinalIgnoreCase))
        break;

    ChatMessage userMessage = new(ChatRole.User, input);
    var response = textAgent.RunStreamingAsync(userMessage, thread);


    await foreach (var update in response)
    {
        Console.Write(update);
    }

    Console.WriteLine();
}

Console.WriteLine("Sohbet sonlandırıldı.");
