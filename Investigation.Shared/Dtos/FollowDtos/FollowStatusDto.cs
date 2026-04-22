using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investigation.Shared.Dtos.FollowDtos
{
    public class FollowStatusDto
    {
        public string? TargetUserId { get; set; }
        public int? TargetCompanyId { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsFollowable { get; set; }
    }
}
