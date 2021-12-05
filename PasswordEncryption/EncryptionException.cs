using System;

namespace PasswordEncryption
{
    public class EncryptionException : Exception
    {
        private static readonly string DefaultMessage = "Error";

        public EncryptionException() : base(DefaultMessage)
        {
        }

        public EncryptionException(string message) : base(message)
        {
        }
    }
}