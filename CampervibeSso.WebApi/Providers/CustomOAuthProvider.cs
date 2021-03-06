﻿using CampervibeSso.WebApi.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace CampervibeSso.WebApi.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            string symmetricKeyAsBase64 = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return Task.FromResult<object>(null);
            }

            using (AudienceRepository audienceRepsotiory = new AudienceRepository())
            {
                //var audience = AudiencesStore.FindAudience(context.ClientId);
                var audience = audienceRepsotiory.Find(context.ClientId);

                if (audience == null)
                {
                    context.SetError("invalid_clientId", string.Format("Invalid client_id '{0}'", context.ClientId));
                    return Task.FromResult<object>(null);
                }

                context.Validated();
                return Task.FromResult<object>(null);
            }
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (AuthRepository _repo = new AuthRepository())
            {
                IdentityUser user = await _repo.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
                else
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim("sub", context.UserName));
                    identity.AddClaim(new Claim("role", "user"));
                    identity.AddClaim(new Claim("userId", user.Id));
                    context.Validated(identity);

                    var props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        {
                             "audience", (context.ClientId == null) ? string.Empty : context.ClientId
                        }
                    });

                    var ticket = new AuthenticationTicket(identity, props);
                    context.Validated(ticket);
                    return;
                }
            }
        }
    }
}