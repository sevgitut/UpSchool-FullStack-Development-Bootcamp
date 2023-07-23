using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models.Auth
{
    public class JwtDto
    {
        public string AccessToken { get; set; }
        public DateTime ExpiryDate { get; set; }

        public JwtDto(string accessToken, DateTime expiryDate)
        {
            AccessToken = accessToken;
            ExpiryDate = expiryDate;
        }
    }
}