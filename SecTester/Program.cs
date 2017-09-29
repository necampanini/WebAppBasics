//using System.Security.Cryptography;
//using System.Threading.Tasks;
//
//namespace SecTester
//{
//    internal class Program
//    {
//        private static string saltString = "no+KJP9d2hsTT6tkVn4dAA==";
//        private static int iterations = 64000;
//        public const int SALT_BYTES = 24;
//        public const int HASH_BYTES = 18;
//        
//        public static async void Main(string[] args)
//        {
//            await InsertTestUsers();
//        }
//
//        private static Task InsertTestUsers()
//        {
//            
//        }
//
//        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
//        {
//            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt)) {
//                pbkdf2.IterationCount = iterations;
//                return pbkdf2.GetBytes(outputBytes);
//            }
//        }
//    }
//}