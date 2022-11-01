// Copyright 2022 Naotsun. All Rights Reserved.

using StreamDeckLib;
using StreamDeckLib.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fleck;

namespace GraphPrinter
{
    [ActionUuid(Uuid="com.naotsun.graphprinter.clipboard.all")]
    public class GraphPrinterAction : BaseStreamDeckActionWithSettingsModel<Models.WebSocketSettingsModel>
    {
        private List<IWebSocketConnection> AllSockets = new();
        private WebSocketServer Server;

        public override async Task OnKeyUp(StreamDeckEventPayload args)
        {
            await Manager.SetTitleAsync(args.context, "OnKeyUp");
        }

        public override async Task OnDidReceiveSettings(StreamDeckEventPayload args)
        {
            await Manager.SetTitleAsync(args.context, "OnDidReceiveSettings");
            
            RestartServer();
        }

        public override async Task OnWillAppear(StreamDeckEventPayload args)
        {
            await base.OnWillAppear(args);
            await Manager.SetTitleAsync(args.context, "OnWillAppear");
            
            RestartServer();
        }

        private void RestartServer()
        {
            Server = new WebSocketServer(SettingsModel.ServerURL);
            Server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    AllSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    AllSockets.Remove(socket);
                    if (AllSockets.Count == 0)
                    {
                        Server.Dispose();
                    }
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
    }
}