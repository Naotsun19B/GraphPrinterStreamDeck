// Copyright 2022 Naotsun. All Rights Reserved.

using StreamDeckLib;
using StreamDeckLib.Messages;
using System.Threading.Tasks;

namespace GraphPrinter
{
    public class GraphPrinterActionBase : BaseStreamDeckActionWithSettingsModel<Models.WebSocketSettingsModel>
    {
        public override Task OnKeyUp(StreamDeckEventPayload args)
        {
            var message = $"UnrealEngine-GraphPrinter-{GetCommand()}";
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

        protected virtual string GetCommand()
        {
            return string.Empty;
        }
    }
    
    [ActionUuid(Uuid="com.naotsun.graphprinter.clipboard.all")]
    public class CopyAllAreaOfWidgetToClipboard : GraphPrinterActionBase
    {
        protected override string GetCommand()
        {
            return "CopyAllAreaOfWidgetToClipboard";
        }
    }
    
    [ActionUuid(Uuid="com.naotsun.graphprinter.clipboard.selected")]
    public class CopySelectedAreaOfWidgetToClipboard : GraphPrinterActionBase
    {
        protected override string GetCommand()
        {
            return "CopySelectedAreaOfWidgetToClipboard";
        }
    }
    
    [ActionUuid(Uuid="com.naotsun.graphprinter.print.all")]
    public class PrintAllAreaOfWidget : GraphPrinterActionBase
    {
        protected override string GetCommand()
        {
            return "PrintAllAreaOfWidget";
        }
    }
    
    [ActionUuid(Uuid="com.naotsun.graphprinter.print.selected")]
    public class PrintSelectedAreaOfWidget : GraphPrinterActionBase
    {
        protected override string GetCommand()
        {
            return "PrintSelectedAreaOfWidget";
        }
    }
    
    [ActionUuid(Uuid="com.naotsun.graphprinter.restore")]
    public class RestoreWidgetFromImageFile : GraphPrinterActionBase
    {
        protected override string GetCommand()
        {
            return "RestoreWidgetFromImageFile";
        }
    }
    
    [ActionUuid(Uuid="com.naotsun.graphprinter.open")]
    public class OpenExportDestinationFolder : GraphPrinterActionBase
    {
        protected override string GetCommand()
        {
            return "OpenExportDestinationFolder";
        }
    }
}