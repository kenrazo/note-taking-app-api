namespace NoteTakingApi.Common.Hellpers
{
    public static class EncryptionHelper
    {
        public static string EncryptPassword(string password, string salt)
        {
            // add a little hard coded salt from config
            string passwordToHash = password + salt;
            var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(passwordToHash, BCrypt.Net.BCrypt.GenerateSalt());

            return encryptedPassword;
        }

        public static bool ValidateEncryptedPassword(string encryptedPassword, string password, string salt)
        {
            var passwordToHash = password + salt;
            return BCrypt.Net.BCrypt.Verify(passwordToHash, encryptedPassword);
        }
    }
}
