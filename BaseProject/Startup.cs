using System;
using SIS.Framework;
using SIS.Framework.Api;

namespace TORSHIA_NEW
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var app = new App();
            WebHost.Start(app);
        }
    }
}