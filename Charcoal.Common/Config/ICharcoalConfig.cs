namespace Charcoal.Common.Config
{
    public interface ICharcoalConfig {
        string ProductionConnectionString { get; set; }
        string TestConnectionString { get; set; }
        void Parse(string filePath = "user.yaml");
    }
}