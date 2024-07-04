using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPI.Models
{
    public class User
    {
        public string username { get; set; }
        public string passwordHashed { get; set; }
    }
}