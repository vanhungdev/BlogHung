using BlogHung.Infrastructure.Utilities;
using Serilog;
using System.Text;

namespace BlogHung.Infrastructure.Logging
{
    public static class LoggingHelper
    {
        private static IDiagnosticContext _diagnosticContext;

        public static void Config(IDiagnosticContext diagnosticContext)
        {
            _diagnosticContext = diagnosticContext;
        }

        public static void SetLogStep(string msg)
        {
            CoreUtility.SetCustomLog(msg.ToString());
        }

        public static string GetLogStep()
        {
            return CoreUtility.GetCustomLog();
        }

        public static void SetProperty(string property, object obj, bool destructureObjects = false)
        {
            _diagnosticContext.Set(property, obj, destructureObjects);
        }
    }

    public static class StringBuilderExtensions
    {
        public static void AppendLog(this StringBuilder stringBuilder, string text)
        {
            stringBuilder.AppendLine(text);
            CoreUtility.SetCustomLog(text);
        }
    }
}
