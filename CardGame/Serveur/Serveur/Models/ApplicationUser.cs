using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serveur.Models
{
    public class ApplicationUser
    {
        public string UserId { get; }
        public string UserName { get; set; }

        public ApplicationUser(string id, string name)
        {
            UserId = id;
            UserName = name;
        }
    }
}
