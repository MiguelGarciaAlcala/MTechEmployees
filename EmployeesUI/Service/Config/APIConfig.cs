using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Config
{
    public class APIConfig : IAPIConfig
    {
        public string BaseUri 
        { 
            get
            {
                return "https://localhost:7167";
            }
        }
    }
}
