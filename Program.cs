using System;

namespace HudsonPoller
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Polling...");

            using (var poller = new HudsonPoller(
                "http://demo.binary-studio.org:4444/", 
                "readonly", 
                "readonly"))
            {
                Console.WriteLine(poller.Poll("FIFA"));
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
