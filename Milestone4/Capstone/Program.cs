using OpenTK;
using OpenTK.Windowing.Desktop;


namespace Capstone
{
    public class Program
    {
        public const string WINDOW_TITLE = "Capstone";
        public const int WINDOW_WIDTH = 1000;
        public const int WINDOW_HEIGHT = 800;
        
        public static void Main(string[] args)
        {
            using(Window window = new Window(WINDOW_WIDTH, WINDOW_HEIGHT, WINDOW_TITLE))
            {
                
                window.Run();
            }
        }
    }
}