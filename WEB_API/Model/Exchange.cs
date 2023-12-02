namespace WEB_API.Model
{
    public class Exchange
    {
        public int Id { get; set; }
        public string? Day { get; set; }
        public string? Instrument { get; set; }
        public int OpenKurs { get; set; }
        public int HighKurs { get; set; }
        public int LowKurs { get; set; }
        public int CloseKurs { get; set; }
        public int Value { get; set; }
    }
}
