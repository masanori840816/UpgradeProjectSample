namespace UpgradeProjectSample.Apps
{
    public class UserActionResult
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }

        public UserActionResult()
        {

        }
        public UserActionResult(bool succeeded, string errorMessage)
        {
            this.Succeeded = succeeded;
            this.ErrorMessage = errorMessage;
        }
    }
    public static class ActionResultFactory
    {
        public static UserActionResult GetSucceeded()
        {
            return new UserActionResult(true, null);
        }
        public static UserActionResult GetFailed(string errorMessage)
        {
            return new UserActionResult(false, errorMessage);
        }
    }
}