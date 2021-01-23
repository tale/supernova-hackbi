using System;

namespace Supernova.Code {
    public static class Program {
        [STAThread]
        static void Main() {
            using var game = new SupernovaGame();
            game.Run();
        }
    }
}
