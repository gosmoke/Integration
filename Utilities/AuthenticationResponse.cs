﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public DateTime ExpirationDateTime { get; set; }
    }
}
