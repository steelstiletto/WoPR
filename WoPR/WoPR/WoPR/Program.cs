using System;

namespace WoPR
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (WoPR game = new WoPR())
            {
                game.Run();
            }
        }
    }
#endif
}

