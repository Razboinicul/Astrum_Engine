using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Astrum;

namespace Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AstrumWindow window = new AstrumWindow();
            window.Setup();
            while (window.running)
            {
                window.PollEvents();
                window.Render();
            }
            window.CleanUp();
        }
    }
}