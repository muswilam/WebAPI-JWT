using AuthorizationServer.Api.Entities;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace AuthorizationServer.Api
{
    public class AudiencesStore
    {
        public static ConcurrentDictionary<string, Audience> audienceList = new ConcurrentDictionary<string, Audience>();

        public AudiencesStore()
        {
            //fixed audience for the demo purpose.
            audienceList.TryAdd("099153c2625149bc8ecb3e85e03f0022",
                                new Audience
                                {
                                    ClientId = "099153c2625149bc8ecb3e85e03f0022",
                                    Base64Secret = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw",
                                    Name = "ResourceServer.Api 1"
                                });
        }

        public static Audience AddAudience(string name)
        {
            //Generating random string of 32 characters as an identifier for the audience (client id).
            var clientId = Guid.NewGuid().ToString("N");

            //Generating 256 bit random key using the “RNGCryptoServiceProvider” class then base 64 URL encode it, 
            //this key will be shared between the Authorization server and the Resource server only.
            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var base64Secret = TextEncodings.Base64Url.Encode(key);

            //Add the newly generated audience to the in-memory “AudiencesList”.
            var newAudience = new Audience { ClientId = clientId, Base64Secret = base64Secret, Name = name };
            audienceList.TryAdd(clientId, newAudience);
            return newAudience;
        }

        public static Audience FindAudience(string clientId)
        {
            Audience existAudience = null;
            if (audienceList.TryGetValue(clientId, out existAudience))
                return existAudience;

            return null;
        }
    }
}