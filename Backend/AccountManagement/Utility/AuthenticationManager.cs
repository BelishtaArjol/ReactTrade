using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AccountManagement.Utility
{
    public static class AuthenticationManager
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool PasswordValidator(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            var isValidated = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);

            if (isValidated == true)
                return true;

            return false;
        }

        public static bool IsValidEmailAddress(this string address) => address != null && new EmailAddressAttribute().IsValid(address);

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public static string CreateToken(int userId, string username, string email, string configuration)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("UserId" , userId.ToString()),
                new Claim("UserName" , username),
                new Claim("Email" , email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public static int ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);

            var idClaimValue = securityToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (!string.IsNullOrWhiteSpace(idClaimValue))
                return int.Parse(idClaimValue);

            return -1;
        }
    }
}
