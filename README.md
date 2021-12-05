# PasswordEncryption

Class library has two methods abot password encryption with Rfc2898DeriveBytes

One of them returns a byte[] key using only data and password. While the user is logging in, the user's only information registered in the database should be retrieved. The user enters the screen. This method should be called using the password and the user's only information registered in the database. The value returned as a result of this method and the key value registered in the user's database. If same, login should be allowed
The other one generates byte[] type key and pure data for the entered password. These two data must be saved in the database when creating a new user. Columns must be generated as blobs
        
