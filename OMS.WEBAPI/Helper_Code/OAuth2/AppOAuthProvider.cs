//-----------------------------------------------------------------------
// <copyright file="AppOAuthProvider.cs" company="None">
//     Copyright (c) Allow to distribute this code.
// </copyright>
// <author>Sridhar Reddy</author>
//-----------------------------------------------------------------------

namespace OMS.WEBAPI.Helper_Code.OAuth2
{
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OAuth;
    using Areas.HelpPage.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;
    using OMS.Entities;
    using OMS.Business;
    using System.Security.Claims;
    using System.Web.Http;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Security.Principal;

    /// <summary>
    /// Application OAUTH Provider class.
    /// </summary>
    public class AppOAuthProvider : OAuthAuthorizationServerProvider
    {
        #region Private Properties

        /// <summary>
        /// Public client ID property.
        /// </summary>
        private readonly string _publicClientId;

        /// <summary>
        /// Database Store property.
        /// </summary>
         
        private UserBLL userBLL = new UserBLL();

        #endregion

        #region Default Constructor method.

        /// <summary>
        /// Default Constructor method.
        /// </summary>
        /// <param name="publicClientId">Public client ID parameter</param>
        public AppOAuthProvider(string publicClientId)
        {
            //TODO: Pull from configuration
            if (publicClientId == null)
            {
                throw new ArgumentNullException(nameof(publicClientId));
            }

            // Settings.
            _publicClientId = publicClientId;
        }

        #endregion

        #region Grant resource owner credentials override method.

        /// <summary>
        /// Grant resource owner credentials overload method.
        /// </summary> 
        /// <param name="context">Context parameter</param>
        /// <returns>Returns when task is completed</returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // Initialization.
            string usernameValx = context.UserName;
            string passwordVal = context.Password;
            //This is for only testing. So, i am not encripting any userid and password.
            
            var user = await this.userBLL.GetUserLoginInformation(context.UserName, context.Password);
            
            // Verification.
            if (user == null || user.LoginEntityList.Count() <= 0)
            {
                // Settings.
                context.SetError("invalid_grant", "The user name or password is incorrect.");

                // Retuen info.
                return;
            }

            // Initialization.
            var claims = new List<Claim>();
            var userInfo = user.LoginEntityList.FirstOrDefault();

            // Setting
            claims.Add(new Claim(ClaimTypes.Name, userInfo.username));
            
            claims.Add(new Claim(ClaimTypes.Role, userInfo.ROLENAME));
            claims.Add(new Claim("sub", userInfo.username));
            claims.Add(new Claim("RoleID",Convert.ToString(userInfo.LOGINROLEID)));
            claims.Add(new Claim("UserID", Convert.ToString(userInfo.ID)));

            // Setting Claim Identities for OAUTH 2 protocol.
            ClaimsIdentity oAuthClaimIdentity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesClaimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationType);

            // Setting user authentication.
           
            AuthenticationProperties properties = CreateProperties(userInfo.username,Convert.ToString(userInfo.ID),Convert.ToString(userInfo.LOGINROLEID),Convert.ToString(userInfo.ROLENAME));
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthClaimIdentity, properties);

            // Grant access to authorize user.
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesClaimIdentity);

            
        }

         
        #endregion

        #region Token endpoint override method.

        /// <summary>
        /// Token endpoint override method
        /// </summary>
        /// <param name="context">Context parameter</param>
        /// <returns>Returns when task is completed</returns>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                // Adding.
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            // Return info.
            return Task.FromResult<object>(null);
        }

        #endregion

        #region Validate Client authntication override method

        /// <summary>
        /// Validate Client authntication override method
        /// </summary>
        /// <param name="context">Contect parameter</param>
        /// <returns>Returns validation of client authentication</returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                // Validate Authoorization.
                context.Validated();
            }

            // Return info.
            return Task.FromResult<object>(null);
        }

        #endregion

        #region Validate client redirect URI override method

        /// <summary>
        /// Validate client redirect URI override method
        /// </summary>
        /// <param name="context">Context parmeter</param>
        /// <returns>Returns validation of client redirect URI</returns>
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            // Verification.
            if (context.ClientId == _publicClientId)
            {
                // Initialization.
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                // Verification.
                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    // Validating.
                    context.Validated();
                }
            }

            // Return info.
            return Task.FromResult<object>(null);
        }

        #endregion

        #region Create Authentication properties method.

        /// <summary>
        /// Create Authentication properties method.
        /// </summary>
        /// <param name="userName">User name parameter</param>
        /// <returns>Returns authenticated properties.</returns>
        public static AuthenticationProperties CreateProperties(string userName,string ID,string LoginRoleID, string LoginRoleName)
        {
            // Settings.
            IDictionary<string, string> data = new Dictionary<string, string>
                                               {
                                                   { "userName", userName },
                                                   { "ID", ID },
                                                    { "LoginRoleID", LoginRoleID },
                                                     { "LoginRoleName", LoginRoleName }
                                               };
            
            // Return info.
            return new AuthenticationProperties(data);
        }

        #endregion
    }
}