// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using IdentityModel;
using System.Security.Claims;
using System.Text.Json;
using Duende.IdentityServer;
using Duende.IdentityServer.Test;
using System.Linq.Expressions;

namespace Cmos.IDP;

public static class TestUsers
{
    public static List<TestUser> Users
    {
        get
        {
            
            return new List<TestUser>
            {
                new TestUser
                {
                    
                    SubjectId = "d860efca-22d9-47fd-8249-791ba61b07c7",
                    Username = "David",
                    Password = "password",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.GivenName, "David"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim("role" , "FreeUser"),
                        new Claim("country", "nl")
                        
                    }
                },
                new TestUser
                {     
                    SubjectId = "b7539694-97e7-4dfe-84da-b4256e1ff5c7",
                    Username = "Emma",
                    Password = "password",
                    Claims =
                    {  
                        new Claim(JwtClaimTypes.GivenName, "Emma"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim("role" , "PayingUser"),
                        new Claim("country", "be")
                    }
                }
            };
        }
    }
}