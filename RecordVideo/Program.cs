using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Captura;

namespace screen_recorder
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args[0];
            var rec = new Recorder(new RecorderParams(path, 10, SharpAvi.KnownFourCCs.Codecs.MotionJpeg, 70));
            Console.WriteLine("Press any key to Stop...");
            Console.ReadKey();

            // Finish Writing
            rec.Dispose();
        }
    }
}
