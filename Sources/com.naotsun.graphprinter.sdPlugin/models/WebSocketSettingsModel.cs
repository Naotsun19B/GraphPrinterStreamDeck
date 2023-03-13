// Copyright 2023 Naotsun. All Rights Reserved.

namespace GraphPrinterStreamDeck.Models
{
    // The IDE recommends adding abstract, but if we do, it will not work properly.
    public class WebSocketSettingsModel
    {
        public string ServerURL { get; set; } = "ws://127.0.0.1:3000";
    }
}