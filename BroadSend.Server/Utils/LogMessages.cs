using System;

namespace BroadSend.Server.Utils
{
    public class LogMessages
    {
        public string ErrorMessage(Exception e, string userName, string message = "")
        {
            var innerMessage = string.Empty;
            if (message != string.Empty)
            {
                innerMessage = message + " | ";
            }
            return $"{innerMessage}User Name: {userName} | {e.Message} | Inner exception: {e.InnerException}";
        }
    }
}