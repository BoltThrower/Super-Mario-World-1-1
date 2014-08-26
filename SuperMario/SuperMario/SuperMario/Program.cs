namespace SuperMario
{
#if WINDOWS || XBOX
    static class Program
    {
       
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)      
        {
            using (SuperMario game = new SuperMario())
            {
                game.Run();
            }
        }
    }
#endif
}