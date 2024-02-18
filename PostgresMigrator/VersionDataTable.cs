using FluentMigrator.Runner.VersionTableInfo;

namespace PostgresMigrator
{
    public class VersionDatTable : IVersionTableMetaData
    {
        public object ApplicationContext { get; set; }
        public bool OwnsSchema => false;
        public string SchemaName => "migration";
        public string TableName => "migration_history";
        public string ColumnName => "version";
        public string DescriptionColumnName => "description";
        public string UniqueIndexName => "uq_main_version";
        public string AppliedOnColumnName => "applied_on";
    }
}