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

// 1 . Simple call
Console.WriteLine(await agent.RunAsync("Merhaba, bugün hava ne kadar da güzel değil mi? Keşke bu güneşli havada bir müze turu yapsaydık."));

// 2. Streaming call
await foreach (var update in agent.RunStreamingAsync("Merhaba, bugün hava ne kadar da güzel değil mi? Keşke bu güneşli havada bir müze turu yapsaydık."))
{
    Console.WriteLine(update);
}

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