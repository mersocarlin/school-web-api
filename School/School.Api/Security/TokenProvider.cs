using Microsoft.Owin.Security.Infrastructure;
using School.Domain.Contracts.Services;
using School.Domain.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace School.Api.Security
{
    public class TokenProvider : IAuthenticationTokenProvider
    {
        private TokenType TokenType { get; set; }
        private IUserService userService;

        public TokenProvider(TokenType tokenType, IUserService userService)
        {
            this.TokenType = tokenType;
            this.userService = userService;
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            int userId = 0;

            foreach (KeyValuePair<string, string> vp in context.Ticket.Properties.Dictionary)
            {
                if (!vp.Key.Equals("userId")) continue;

                userId = Convert.ToInt32(vp.Value);
            }

            string token = Guid.NewGuid().ToString("n");

            switch (this.TokenType)
            {
                case TokenType.AccessToken:

                    break;
                case TokenType.RefreshToken:
                    DateTime IssuedUtc = DateTime.UtcNow;
                    DateTime ExpiresUtc = IssuedUtc.AddHours(1);

                    context.Ticket.Properties.IssuedUtc = IssuedUtc;
                    context.Ticket.Properties.ExpiresUtc = ExpiresUtc;

                    this.userService.UpdateRefreshToken(userId, GetHash(token), context.SerializeTicket());
                    break;
            }

            context.SetToken(token);

            return Task.FromResult<object>(null);
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            /* Generates a new accessToken from refreshToken */
            string hashedToken = GetHash(context.Token);

            var user = this.userService.GetByRefreshTokenId(hashedToken);

            if (user == null) return Task.FromResult<object>(null);

            context.DeserializeTicket(user.ProtectedTicket);

            this.userService.UpdateRefreshToken(user.Id, "", "");

            return Task.FromResult<object>(null);
        }

        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}