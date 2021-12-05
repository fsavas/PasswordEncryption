using System.Linq;
using System.Security.Cryptography;

namespace PasswordEncryption
{
    public static class Encryption
    {
        //Returns a byte[] key using only data and password. While the user is logging in, the user's only information registered in the database should be retrieved. The user enters the screen
        //This method should be called using the password and the user's only information registered in the database. The value returned as a result of this method and the key value registered in the user's database
        // if same, login should be allowed
        public static byte[] GetKey(string password, byte[] salt)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new EncryptionException("Password cannot be empty");
            }

            using (var deriveBytes = GetDeriveBytes(password, salt))
            {
                return deriveBytes.GetBytes(20);//Derive a 20-byte key
            }
        }

        // Generates byte[] type key and pure data for the entered password. These two data must be saved in the database when creating a new user. Columns must be generated as blobs
        public static EncryptedData CreateSaltKey(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new EncryptionException("Password cannot be empty");
            }

            var salt = GenerateSalt();
            var encryptedData = new EncryptedData();

            using (var deriveBytes = GetDeriveBytes(password, salt))
            {
                byte[] key = deriveBytes.GetBytes(20);

                encryptedData.Key = key;
                encryptedData.Salt = salt;
            }

            return encryptedData;
        }

        private static byte[] GenerateSalt()
        {
            var salt = new byte[20];

            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }

        private static Rfc2898DeriveBytes GetDeriveBytes(string password, byte[] salt)
        {
            return new Rfc2898DeriveBytes(password, HashWithDefault(salt));
        }

        private static byte[] HashWithDefault(byte[] salt)
        {
            byte[] bytes = { 115, 0, 33, 0, 119, 0, 55, 0, 121, 0, 84, 0, 71, 0, 51, 0, 98, 0, 75 };
            SHA256Managed hashstring = new SHA256Managed();

            return hashstring.ComputeHash(bytes.Select(x => (byte)(x ^ 79)).Concat(salt).ToArray());
        }
    }
}