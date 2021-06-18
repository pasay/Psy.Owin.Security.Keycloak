using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace Psy.Owin.Security.Keycloak.IdentityModel.Models.Responses
{
    public class AuthorizationResponse : OidcResponse
    {
        public AuthorizationResponse(string query)
        {
            InitFromRequest(ParseQuery(query));
            if (!Validate())
            {
                throw new ArgumentException("Invalid query string used to instantiate an AuthorizationResponse");
            }
        }

        public string Code { get; private set; }
        public string State { get; private set; }

        protected new void InitFromRequest(NameValueCollection authResult)
        {
            base.InitFromRequest(authResult);

            Code = authResult.Get(OpenIdConnectParameterNames.Code);
            State = authResult.Get(OpenIdConnectParameterNames.State);
        }

        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(Code) && !string.IsNullOrWhiteSpace(State);
        }



        /// <summary>
        /// Parse a query string into its component key and value parts.
        /// </summary>
        /// <param name="queryString">The raw query string value, with or without the leading '?'.</param>
        /// <returns>A collection of parsed keys and values.</returns>
        public static NameValueCollection ParseQuery(string queryString)
        {
            var result = ParseNullableQuery(queryString);

            if (result == null)
            {
                return new NameValueCollection();
            }

            return result;
        }


        /// <summary>
        /// Parse a query string into its component key and value parts.
        /// </summary>
        /// <param name="queryString">The raw query string value, with or without the leading '?'.</param>
        /// <returns>A collection of parsed keys and values, null if there are no entries.</returns>
        public static NameValueCollection ParseNullableQuery(string queryString)
        {
            var accumulator = new NameValueCollection();

            if (string.IsNullOrEmpty(queryString) || queryString == "?")
            {
                return null;
            }

            int scanIndex = 0;
            if (queryString[0] == '?')
            {
                scanIndex = 1;
            }

            int textLength = queryString.Length;
            int equalIndex = queryString.IndexOf('=');
            if (equalIndex == -1)
            {
                equalIndex = textLength;
            }
            while (scanIndex < textLength)
            {
                int delimiterIndex = queryString.IndexOf('&', scanIndex);
                if (delimiterIndex == -1)
                {
                    delimiterIndex = textLength;
                }
                if (equalIndex < delimiterIndex)
                {
                    while (scanIndex != equalIndex && char.IsWhiteSpace(queryString[scanIndex]))
                    {
                        ++scanIndex;
                    }
                    string name = queryString.Substring(scanIndex, equalIndex - scanIndex);
                    string value = queryString.Substring(equalIndex + 1, delimiterIndex - equalIndex - 1);
                    accumulator.Add(
                        Uri.UnescapeDataString(name.Replace('+', ' ')),
                        Uri.UnescapeDataString(value.Replace('+', ' ')));
                    equalIndex = queryString.IndexOf('=', delimiterIndex);
                    if (equalIndex == -1)
                    {
                        equalIndex = textLength;
                    }
                }
                else
                {
                    if (delimiterIndex > scanIndex)
                    {
                        accumulator.Add(queryString.Substring(scanIndex, delimiterIndex - scanIndex), string.Empty);
                    }
                }
                scanIndex = delimiterIndex + 1;
            }

            if (accumulator.Count == 0)
            {
                return null;
            }

            return accumulator;
        }
    }
}