namespace Misfit.Console.CommandLineParser
{
    public interface ICommandInfo
    {
        string AltName { get; set; }
        string CommandName { get; }
        string Description { get; set; }
        int MaxArgs { get; set; }
        int MinArgs { get; set; }
        string UsageDescription { get; set; }
        string UsageSummary { get; set; }
    }
}