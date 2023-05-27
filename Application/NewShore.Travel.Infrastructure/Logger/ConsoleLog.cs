using NewShore.Travel.Application.Contracts.Logger;
using System;
using System.Text.Json;

namespace NewShore.Travel.Infrastructure.Logger
{
    internal class ConsoleLog<T> : ICustomLogger<T> where T : class
    {
        public void LogError(string message)
        {
            Console.Write(message);
        }

        public void LogInformation<TInput>(TInput message)
        {
            Console.Write(JsonSerializer.Serialize(message));
        }

        public void LogInformation(string message)
        {
            Console.Write(message);
        }

        public void LogWarning<TInput>(TInput message)
        {
            Console.Write(JsonSerializer.Serialize(message));
        }

        public void LogWarning(string message)
        {
            Console.Write(message);
        }
    }
}
