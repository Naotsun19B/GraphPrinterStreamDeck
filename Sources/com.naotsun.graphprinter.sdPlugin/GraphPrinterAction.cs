// Copyright 2023 Naotsun. All Rights Reserved.

using StreamDeckLib;
using StreamDeckLib.Messages;
using System.Threading.Tasks;
using GraphPrinterStreamDeck.Models;
using GraphPrinterStreamDeck.Server;

namespace GraphPrinterStreamDeck
{
    public class GraphPrinterActionBase : BaseStreamDeckActionWithSettingsModel<WebSocketSettingsModel>
    {
        public override Task OnKeyUp(StreamDeckEventPayload args)
        {
            var message = $"UnrealEngine-GraphPrinter-{GetType().Name}";
            ServerManager.GetInstance().Send(SettingsModel.ServerURL, message);
            return Task.CompletedTask;
        }

        public override async Task OnDidReceiveSettings(StreamDeckEventPayload args)
        {
            ServerManager.GetInstance().Remove(SettingsModel.ServerURL);
            await base.OnDidReceiveSettings(args);
            ServerManager.GetInstance().Add(SettingsModel.ServerURL);
        }

        public override async Task OnWillAppear(StreamDeckEventPayload args)
        {
            await base.OnWillAppear(args);
            ServerManager.GetInstance().Add(SettingsModel.ServerURL);
        }
    }
    
    [ActionUuid(Uuid="com.naotsun.graphprinter.clipboard.all")]
    public class CopyAllAreaOfWidgetToClipboard : GraphPrinterActionBase {}
    
    [ActionUuid(Uuid="com.naotsun.graphprinter.clipboard.selected")]
    public class CopySelectedAreaOfWidgetToClipboard : GraphPrinterActionBase {}
    
    [ActionUuid(Uuid="com.naotsun.graphprinter.print.all")]
    public class PrintAllAreaOfWidget : GraphPrinterActionBase {}
    
    [ActionUuid(Uuid="com.naotsun.graphprinter.print.selected")]
    public class PrintSelectedAreaOfWidget : GraphPrinterActionBase {}
    
    [ActionUuid(Uuid="com.naotsun.graphprinter.restore")]
    public class RestoreWidgetFromImageFile : GraphPrinterActionBase {}
    
    [ActionUuid(Uuid="com.naotsun.graphprinter.open")]
    public class OpenExportDestinationFolder : GraphPrinterActionBase {}
}
