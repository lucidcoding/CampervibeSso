using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace CampervibeSso.WebApi.Entities
{
    public class Audience
    {
        [Key]
        [MaxLength(32)]
        public string ClientId { get; set; }

        [MaxLength(80)]
        [Required]
        public string Base64Secret { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        private static Audience Create(string name)
        {
            var audience = new Audience();
            audience.ClientId = Guid.NewGuid().ToString("N");
            audience.Name = name;
            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            audience.Base64Secret = TextEncodings.Base64Url.Encode(key);
            return audience;
        }
    }
}