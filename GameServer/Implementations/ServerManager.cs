using GameServer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Implementations
{
    public class ServerManager : IServerManager
    {
        private List<IServerCode> _serverCodes;
        private HttpListener _httpListener;
        private bool _listening;

        public ServerManager()
        {
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add("http://localhost:9090/");
        }

        public void StartListening()
        {
            try
            {
                _serverCodes = new List<IServerCode>();
                _listening = true;
                Listen();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to start listening");
                Console.Error.WriteLine(ex);
            }
        }

        public bool CreateServer(Guid serverId)
        {
            if (!_httpListener.IsListening) return false;
            throw new NotImplementedException();
        }

        public IServerCode GenerateServerCode()
        {
            ServerCode newServerCode = new ServerCode();
            newServerCode.GenerateServerCode();
            while(ServerExists(newServerCode.ServerId, newServerCode.ServerConnectCode))
            {
                newServerCode = new ServerCode();
                newServerCode.GenerateServerCode();
            }

            return newServerCode;
        }

        private bool ServerExists(Guid serverId, string serverConnectCode)
        {
            return _serverCodes.FirstOrDefault(serverCode => serverCode.ServerId == serverId ||
                                                             serverCode.ServerConnectCode == serverConnectCode) != null;
        }

        public bool ProcessCommand(Guid serverId, string message)
        {
            if (!_httpListener.IsListening) return false;

            throw new NotImplementedException();
        }

        private void Listen()
        {
            try
            {
                _httpListener.Start();
                while (_listening)
                {
                    IAsyncResult result = _httpListener.BeginGetContext(new AsyncCallback(ListenerCallback), _httpListener);
                    Console.WriteLine("Waiting for request...");
                    result.AsyncWaitHandle.WaitOne();
                    Console.WriteLine("Request processed asyncronously!");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception thrown!");
                Console.Error.WriteLine(ex);
            }

            Console.WriteLine("Finished listening!");
        }

        private void ListenerCallback(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;
            HttpListenerContext context = listener.EndGetContext(result);
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            string requestMessage = ReadMessage(request);
            if (requestMessage == "END")
                _listening = false;

            //Send a response to the user.
            SendAcknowledgeMessage(response);
            response.OutputStream.Close();
        }

        private void SendAcknowledgeMessage(HttpListenerResponse response)
        {
            try
            {
                string acknowledgeText = "ACK";
                byte[] buffer = Encoding.UTF8.GetBytes(acknowledgeText);
                response.ContentLength64 = buffer.Length;
                response.StatusCode = 200;
                Stream outputStream = response.OutputStream;
                outputStream.Write(buffer, 0, buffer.Length);
                outputStream.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error sending acknowledgement!");
                Console.Error.WriteLine(ex);
            }
        }

        private string ReadMessage(HttpListenerRequest request)
        {
            try
            {
                using (StreamReader sr = new StreamReader(request.InputStream))
                    return sr.ReadToEnd();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error reading request message");
                Console.Error.WriteLine(ex);
                return string.Empty;
            }
        }

    }
}
