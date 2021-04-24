using System;

namespace LD48
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new TheDeep7())
                game.Run();
        }
    }
}
