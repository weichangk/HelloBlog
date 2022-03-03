namespace Blog.Framework.Logger
{
    public interface ILogger
    {
        void Info(string message, Exception exception = null);
        void Warn(string message, Exception exception = null);
        void Debug(string message, Exception exception = null);
        void Error(string message, Exception exception =null);     
    }
}