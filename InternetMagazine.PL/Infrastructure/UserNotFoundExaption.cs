using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetMagazine.PL.Infrastructure
{
    public class UserNotFoundExaption : Exception
    {
        public string Property { get; protected set; }
        public UserNotFoundExaption(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
