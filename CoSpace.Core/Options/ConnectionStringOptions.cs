namespace CoSpace.Core.Options
{
    public class ConnectionStringOptions
    {
        public const string SectionName = "ConnectionStrings";

        public string Local { get; set; } = null!;
        public string Prod { get; set; } = null;
    }
}
