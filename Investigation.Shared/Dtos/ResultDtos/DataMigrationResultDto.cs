
namespace Investigation.Shared.Dtos.ResultDtos
{
    public class DataMigrationResultDto
    {
        public int TotalCount { get; set; }
        public int MigratedCount { get; set; }
        public int SkippedCount { get; set; }
        public int FailedCount { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
