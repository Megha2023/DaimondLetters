namespace DiamondKata
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Enter a letter");
            
            var characterString = Console.ReadLine();
            
            IDiamond diamond = new Diamond();
            
            var plotDiamond = diamond.PlotDiamond(characterString);
            
            Console.WriteLine(plotDiamond);
            
            Console.WriteLine("Press any key to exit");
            
            Console.ReadKey();
        }
    }
}