namespace GraphPrinter.Models
{
    // The IDE recommends adding abstract, but if we do, it will not work properly.
    public class WebSocketSettingsModel
    {
        public string ServerURL { get; set; }
        public string ServerProtocol { get; set; }
    }
}
