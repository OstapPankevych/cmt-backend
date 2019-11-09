using System;
using Cmt.Common.Types.Audit;

namespace Cmt.Dal.Entities
{
    public class AuditEntity<TId>
    {
        public TId Id { get; set; }
        public DateTime PerformedAt { get; set; }
        public TId PerformedBy { get; set; }
        public AuditType Type { get; set; }
        public ObjectType ObjectType { get; set; }
        public TId ObjectId { get; set;}
    }
}
