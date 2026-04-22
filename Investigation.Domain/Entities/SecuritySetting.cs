using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class SecuritySetting : BaseEntity
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}