using System;
using SIS.Framework;

namespace TORSHIA.App
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