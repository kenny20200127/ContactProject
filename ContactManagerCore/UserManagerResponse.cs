﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AspnetIdentityCore
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public bool isSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
