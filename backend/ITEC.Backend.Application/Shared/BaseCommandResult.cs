
namespace ITEC.Backend.Application.Shared
{
    public class BaseCommandResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public BaseCommandResult()
        {
            Success = true;
        }
        public BaseCommandResult(string message = null)
        {
            Success = true;
            Message = message;
        }
        public BaseCommandResult(string message = null, bool success = true)
        {
            Success = success;
            Message = message;
        }

    }
}
