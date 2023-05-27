namespace NewShore.Travel.Application.Contracts.Logger
{
    public interface ICustomLogger<T> where T : class
    {
        public void LogInformation<TInput>(TInput message);
        public void LogInformation(string message);
        public void LogWarning<TInput>(TInput message);
        public void LogWarning(string message);
        public void LogError(string message);
    }
}
