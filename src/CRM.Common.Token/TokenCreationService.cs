using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CRM.Common.Token
{
    public interface ITokenCreationService 
    {
        Task<string> CreateAsync(string username, string password);
    }

    public class TokenCreationService : ITokenCreationService
    {
        private readonly IConfiguration _configuration;

        public TokenCreationService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public async Task<string> CreateAsync(string username, string password) 
        {
            var client = new HttpClient();

            var auth = _configuration.GetValue<string>("IdentityServer:Authority");
            var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _configuration.GetValue<string>("IdentityServer:Authority"),
                Policy =
                {
                    RequireHttps = false
                }
            });

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = _configuration.GetValue<string>("IdentityServer:ClientId"),
                ClientSecret = _configuration.GetValue<string>("IdentityServer:SecretKey"),

                UserName = username,
                Password = password,
                Scope = "CRM.Api"
            });

            if (tokenResponse.IsError)
            {
                throw new Exception("Token couldn't be created");
            }

            return tokenResponse.AccessToken;
        }
    }
}
