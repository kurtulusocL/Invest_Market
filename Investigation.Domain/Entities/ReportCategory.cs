using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class ReportCategory:BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
