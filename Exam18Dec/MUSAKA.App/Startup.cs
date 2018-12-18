using System;
using SIS.Framework;

namespace MUSAKA.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var app = new MusakaApp();
            WebHost.Start(app);
        }
    }
}
