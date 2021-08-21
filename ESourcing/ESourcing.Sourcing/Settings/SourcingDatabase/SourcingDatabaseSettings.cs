namespace ESourcing.Sourcing.Settings.SourcingDatabase
{
    public class SourcingDatabaseSettings : ISourcingDatabaseSettings, ISettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
