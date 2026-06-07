using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken
    {
        public string Token { get; set; } //token değeri
        public DateTime Expiration { get; set; } //tokenın ne zaman geçersiz olacağını belirten tarih

    }
}
