using StreamDeckLib;
using StreamDeckLib.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphPrinter
{
    [ActionUuid(Uuid="com.yourcompany.plugin.action.DefaultPluginAction")]
    public class GraphPrinterAction : BaseStreamDeckActionWithSettingsModel<Models.WebSocketSettingsModel>
    {
        public override async Task OnKeyUp(StreamDeckEventPayload args)
        {
            //update settings
            await Manager.SetSettingsAsync(args.context, SettingsModel);
        }

        public override async Task OnDidReceiveSettings(StreamDeckEventPayload args)
        {
            await Manager.SetTitleAsync(args.context, "Test");
        }

        public override async Task OnWillAppear(StreamDeckEventPayload args)
        {
            await base.OnWillAppear(args);
            await Manager.SetTitleAsync(args.context, "Test");
        }

    }
}
