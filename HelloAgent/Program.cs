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

AIAgent agent = new OpenAIClient(new ApiKeyCredential(ghPat),
                                 new OpenAIClientOptions { Endpoint = new Uri(uri) })
    .GetChatClient("gpt-4o")
    .CreateAIAgent(instructions: "Sen bir çevirmensin. Kullanıcıdan gelen yazıları direkt olarak, C2 seviyesinde üst düzey resmi İngilizce diline çevir.");

#region First Calls!
// 1 . Simple call
Console.WriteLine(await agent.RunAsync("Merhaba, bugün hava ne kadar da güzel değil mi? Keşke bu güneşli havada bir müze turu yapsaydık."));

// 2. Streaming call
await foreach (var update in agent.RunStreamingAsync("Merhaba, bugün hava ne kadar da güzel değil mi? Keşke bu güneşli havada bir müze turu yapsaydık."))
{
    Console.WriteLine(update);
}
#endregion

#region ChatMessages

//3.1 ChatMessages

ChatMessage message = new(ChatRole.User, "Hava ne kadar güzel değil mi?");

Console.WriteLine(await agent.RunAsync(message));

//3.2 ChatMessages

ChatMessage[] messages = [
    new ChatMessage(ChatRole.User, "Hava ne kadar güzel değil mi?"),
    new ChatMessage(ChatRole.Assistant, "Isn't the weather absolutely delightful?"),
    new ChatMessage(ChatRole.User, "Daha resmi bir şekilde çevirebilir miyiz?")
    ];

Console.WriteLine(await agent.RunAsync(messages));
#endregion

AIAgent visionAgent = new OpenAIClient(new ApiKeyCredential(ghPat),
    new OpenAIClientOptions { Endpoint = new Uri(uri) })
    .GetChatClient("gpt-4o")
    .CreateAIAgent(instructions:
        "Kullanıcıdan aldığın resimleri, ünlü bir resim eleştirmeni gibi, "
        + "renklerin temaya uyumuna göre yorumlayan, çok yaşlı bir üstatsın. "
        + "Kullandığın kelimeler Eski Türkçe ile harmanlanmış olup, "
        + "arada öksürüp piponu tüttürürsün.");

ChatMessage messageWithVision = new(ChatRole.User, [
    new TextContent("Bu resimden ne anlıyorsunuz efenim?"),
    new UriContent("https://images.unsplash.com/photo-1565095310836-244306b536a6", "image/jpeg")
]);

Console.WriteLine(await visionAgent.RunAsync(messageWithVision));