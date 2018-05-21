using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdl.common.models.v01
{
    public class CustomFlag
    {
        public Guid JobId { get; set; }
        public string JobTime { get; set; }
        public string Type { get; set; }
        public string CommentText { get; set; }
        public List<CustomFlagAssociatedChannel> AssociatedValues { get; set; }
    }
}
