using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class CompanyTeam:BaseEntity
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public int TotalExperienceDuration { get; set; }
        public string PhotoUrl { get; set; }

        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
