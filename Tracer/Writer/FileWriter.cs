using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace NTracer.Writer
{
    public class FileWriter : IWriter
    {
        public void Write(string text)
        {
            using (var fileWriter = new StreamWriter("SerializationResult.txt"))
            {
                fileWriter.Write(text);
            }
        }
    }
}
