using Exchange.Common;
using Exchange.Pages;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace Exchange.Knet
{    
    public class eSocketClient: IDisposable
    {
        private const int portNumber = 25000;
        private const string hostAddress = "localhost";
        private TcpClient tcpClient;


        private CancellationTokenSource _cts = new CancellationTokenSource();


        /* 
         * IMPORTANT NOTE: In production messages shouldn't be stored in continually growing list
         * or if it is stored in a list, the list should be cleared every once in a while, or when it 
         * reaches a certain size. Otherwise it'll cause memory bloat.
         */
        public List<byte[]> ReceivedMessages = new List<byte[]>();

        public byte[] LastMessage
        {
            get
            {
                return ReceivedMessages.Last();
            }
        }

        public eSocketClient()
        {
            Utility.Log("Starting TCP client..");
            tcpClient = new TcpClient(hostAddress, portNumber);
            Utility.Log("TCP Client Connected!");
        }

        public void TcpWrite(byte[] message)
        {
            try
            {                
                NetworkStream ns = tcpClient.GetStream();
                ns.Write(message, 0, message.Count());
                ns.Flush();
            }
            catch (Exception e)
            {
                Utility.Log(e.ToString());
                RichMessageBox.Show($"TcpWrite-Error-{e.Message} \n {JsonConvert.SerializeObject(e)}");
            }
        }

        public void StartReading()
        {
            Utility.Log("Starting reading task...");

            // Create a new CancellationTokenSource for each new task
            _cts = new CancellationTokenSource();

            // Run the TcpRead loop as a background task
            _ = Task.Run(() => TcpRead(_cts.Token), _cts.Token).ConfigureAwait(false);
        }
        public void StopReading()
        {
            if (_cts != null)
            {
                _cts.Cancel();  // Cancels the task
            }
        }
        public void TcpRead(CancellationToken token)
        {
            Utility.Log("Waiting for messages..");
            try
            {
                NetworkStream ns = tcpClient.GetStream();
                while (!token.IsCancellationRequested)
                {
                    //Read message length
                    byte[] lenBytes = new byte[2];
                    int lenBytesRead = ns.Read(lenBytes, 0, lenBytes.Length);
                    if (BitConverter.IsLittleEndian) Array.Reverse(lenBytes);
                    var len = BitConverter.ToUInt16(lenBytes, 0);

                    Utility.Log("Received message of length: " + len.ToString());

                    //Read message
                    byte[] messageBuf = new byte[len];
                    Array.Clear(messageBuf, 0, len);
                    int msgBytesRead = ns.Read(messageBuf, 0, len);
                    Utility.Log("------------------------------------");
                    Utility.Log(Encoding.UTF8.GetString(messageBuf, 0, msgBytesRead));
                    Utility.Log("------------------------------------");
                    ns.Flush();

                    //avoid race conditions between list access and list modification
                    lock (ReceivedMessages)
                    {
                        ReceivedMessages.Add(messageBuf);
                    }
                }
            }
            catch (Exception e)
            {
                Utility.Log(e.ToString());
            }            
        }
        public void Dispose()
        {
            try
            {
                _cts.Cancel();
                tcpClient?.Dispose();
                ReceivedMessages.Clear();
            }
            catch(Exception ex)
            {
                Utility.Log("Error during Dispose: " + ex.ToString());
            }
        }
    }
}
