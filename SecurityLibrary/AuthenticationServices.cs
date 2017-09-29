using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Contracts.Repositories;
using Contracts.Services;
using Models.Entities;
using Models.Enums;

namespace SecurityLibrary
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IUserRepository _userRepository;
        private const int PBKDF2_Iterations = 64000;
        public const int SALT_BYTES = 24;
        public const int HASH_BYTES = 18;
        
        public AuthenticationServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RegistrationStatus> RegisterUser(string email, string password)
        {
            //check if email is in use
            var user = await _userRepository.GetUserByEmail(email);
            if (user != null) return RegistrationStatus.EmailAlreadyInUse;
            
            try
            {
                //create salt
                byte[] salt = GetSaltBytes();
                string saltString = Convert.ToBase64String(salt);
                
                //create hash
                byte[] hash = GetHashBytes(password, salt);
                string hashString = Convert.ToBase64String(hash);

                //create user
                var newUser = User.Create(email, hashString, saltString);

                //insert user
                var recordAffected = await _userRepository.InsertUser(newUser);

                if (recordAffected == 1) return RegistrationStatus.Success;
            }
            catch (Exception ex)
            {
                //log
            }
            
            return RegistrationStatus.ServerError;
        }
        
        //helpers
        private static byte[] GetHashBytes(string input, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(input, salt)) {
                pbkdf2.IterationCount = PBKDF2_Iterations;
                return pbkdf2.GetBytes(HASH_BYTES);
            }
        }
        
        private static byte[] GetSaltBytes()
        {
            using (var service = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[SALT_BYTES];
                service.GetBytes(new byte[SALT_BYTES]);
                return bytes;
            }
        }
    }
}