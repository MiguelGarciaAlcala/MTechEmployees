using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Config
{
    public interface IAPIConfig
    {
        public string BaseUri { get; }
    }
}
