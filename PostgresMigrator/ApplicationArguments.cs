using CommandLine;

namespace PostgresMigrator
{
    public class ApplicationArguments
    {
        [Option('m', "mode", Required = true, HelpText = "Mode of migration [update/rollback]")]
        public string MigrationMode { get; set; }
        
        [Option('v', "version", Required = false, HelpText = "Version to migrate")]
        public long? MigrationVersion { get; set; }
    }
}