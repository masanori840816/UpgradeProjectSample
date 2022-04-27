namespace UpgradeProjectSample.Users.Dto
{
    public class SigninValue
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public SigninValue()
        {

        }
        public SigninValue(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }
    }
}
