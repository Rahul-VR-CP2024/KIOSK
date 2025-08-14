using System.Xml.Linq;

namespace Exchange.Knet
{
    public class XmlRequest
    {

        static XNamespace Esp = "http://www.mosaicsoftware.com/Postilion/eSocket.POS/";

        public static XDocument Initialization(string TerminalId)
        {
            XElement registerCallback = new XElement(Esp + "Register",
                new XAttribute("Type", "CALLBACK"),
                new XAttribute("EventId", "DATA_REQUIRED"));

            XElement registerEvent = new XElement(Esp + "Register",
                new XAttribute("Type", "EVENT"),
                new XAttribute("EventId", "DEBUG_ALL"));

            XElement admin = new XElement(Esp + "Admin",
                new XAttribute("TerminalId", TerminalId),
                new XAttribute("Action", "INIT"));

            XElement Interface = new XElement(Esp + "Interface",
                new XAttribute(XNamespace.Xmlns + "Esp", Esp),
                new XAttribute("Version", "1"));

            admin.Add(registerCallback);
            admin.Add(registerEvent);

            Interface.Add(admin);

            XDeclaration dec = new XDeclaration("1.0", "utf-8", "");

            return new XDocument(dec, Interface);
        }

        public static XDocument Close(string TerminalId)
        {

            XElement admin = new XElement(Esp + "Admin",
                new XAttribute("TerminalId", TerminalId),
                new XAttribute("Action", "CLOSE"));

            XElement Interface = new XElement(Esp + "Interface",
                new XAttribute(XNamespace.Xmlns + "Esp", Esp),
                new XAttribute("Version", "1"));

            Interface.Add(admin);

            XDeclaration dec = new XDeclaration("1.0", "utf-8", "");

            return new XDocument(dec, Interface);
        }

        public static XDocument Payment(string transactionAmount, string TerminalId, string TransactionId)
        {
            XElement Transaction = new XElement(Esp + "Transaction",
                new XAttribute("TerminalId", TerminalId),
                new XAttribute("TransactionId", TransactionId),
                new XAttribute("Type", "PURCHASE"),
                new XAttribute("TransactionAmount", transactionAmount)
                );

            XElement Interface = new XElement(Esp + "Interface",
                new XAttribute(XNamespace.Xmlns + "Esp", Esp),
                new XAttribute("Version", "1"));

            Interface.Add(Transaction);

            XDeclaration dec = new XDeclaration("1.0", "utf-8", "");

            return new XDocument(dec, Interface);
        }
    }
}
