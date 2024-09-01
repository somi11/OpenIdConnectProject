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
                    SubjectId = "d860efca",
                    Username = "David",
                    Password = "password",
                    Claims =
                    {
                        new Claim("role" , "FreeUser"),
                        new Claim(JwtClaimTypes.GivenName, "David"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        
                    }
                },
                new TestUser
                {
                    SubjectId = "afsdefsa",
                    Username = "Emma",
                    Password = "password",
                    Claims =
                    {  
                        new Claim("role" , "PayingUser"),
                        new Claim(JwtClaimTypes.GivenName, "Emma"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    }
                }
            };
        }
    }
}