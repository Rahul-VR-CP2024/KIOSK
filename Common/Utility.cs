using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Exchange.Common
{
    public class Utility
    {
        public static bool DEBUG_MODE = false;


        //to see which thread is printing what
        public static void Log(string message)
        {
            if (DEBUG_MODE)
            {
                string threadName = Thread.CurrentThread.Name;
                Console.WriteLine($"{DateTime.Now.ToString()} [{threadName}]: {message}");
            }
        }

        //Converts an XML document into bytes ready to be sent over TCP
        public static byte[] XmlToBytes(XDocument doc)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                doc.Save(stream);
                string xmlString;
                using (StreamReader reader = new StreamReader(stream))
                {
                    stream.Position = 0;
                    xmlString = reader.ReadToEnd();
                }

                byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlString.ToCharArray());
                byte[] lenHeader = new byte[2];
                Array.Clear(lenHeader, 0, 2);
                lenHeader = BitConverter.GetBytes((Int16)xmlBytes.Count());
                if (BitConverter.IsLittleEndian) Array.Reverse(lenHeader);
                byte[] res = lenHeader.Concat(xmlBytes).ToArray();
                return res;
            }
        }

        //Parses bytes into an XML Document that can be manipulated and accessed
        public static XDocument BytesToXml(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return XDocument.Load(ms);
            }
        }
    }
}
