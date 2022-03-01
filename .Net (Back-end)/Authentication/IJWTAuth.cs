using REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Authentication
{
    public interface IJWTAuth
    {
        string Authentication(string username, string password, List<users> userCredential);
    }
}
