namespace UpgradeProjectSample.Apps
{
    public record UserActionResult(bool Succeeded, string? ErrorMessage);
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