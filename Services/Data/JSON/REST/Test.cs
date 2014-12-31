using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickatell.Services.Data.JSON.REST
{
    public class TestRequest
    {
        public class Rootobject
        {
            public string text { get; set; }
            public string[] to { get; set; }
        }
    }
}
