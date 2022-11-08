// Copyright 2022 Naotsun. All Rights Reserved.

using System.Collections.Generic;
using System.Linq;
using Fleck;

namespace GraphPrinter
{
    public class Server
    {
        private List<IWebSocketConnection> AllSockets = new();
        private WebSocketServer Instance;

        public string Location => Instance.Location;

        public Server(string location)
        {
            Instance = new WebSocketServer(location);
            Instance.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    AllSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    AllSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    foreach (var otherSocket in AllSockets)
                    {
                        if (otherSocket != socket)
                        {
                            otherSocket.Send(message);
                        }
                    }
                };
            });
        }

        public void Close()
        {
            Instance.Dispose();
            AllSockets.Clear();
        }

        public void Send(string message)
        {
            foreach (var socket in AllSockets)
            {
                socket.Send(message);
            }
        }
    }
    
    public class ServerManager
    {
        private static ServerManager Instance = new ServerManager();
        private List<Server> Servers = new();

        private ServerManager()
        {
        }

        public static ServerManager GetInstance()
        {
            return Instance;
        }

        private Server Find(string location)
        {
            return Servers.Find(server => (server.Location == location));
        }
        
        private bool Exist(string location)
        {
            return (Find(location) != null);
        }
        
        public void Add(string location)
        {
            if (Exist(location))
            {
                return;
            }
            
            Servers.Add(new Server(location));
        }

        public void Remove(string location)
        {
            if (!Exist(location))
            {
                return;
            }

            var server = Find(location);
            if (server != null)
            {
                server.Close();
                Servers.Remove(server);
            }
        }

        public void Send(string location, string message)
        {
            var server = Find(location);
            if (server != null)
            {
                server.Send(message);
            }
        }
    }
}
