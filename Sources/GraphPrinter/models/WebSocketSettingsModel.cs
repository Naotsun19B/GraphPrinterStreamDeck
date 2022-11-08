// Copyright 2022 Naotsun. All Rights Reserved.

namespace GraphPrinter.Models
{
    // The IDE recommends adding abstract, but if we do, it will not work properly.
    public class WebSocketSettingsModel
    {
        public string ServerURL { get; set; } = "ws://127.0.0.1:3000";
    }
}
