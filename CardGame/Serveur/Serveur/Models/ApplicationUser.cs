using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serveur.Models
{
    public class ApplicationUser
    {
        public string MyGuid { get; }
        public string MyName { get; set; }

        public ApplicationUser(string id, string name)
        {
            MyGuid = id;
            MyName = name;
        }
    }
}
