using System;

namespace Cmt.Dal.Entities
{
    public class Entity<TId>: IIdEntity<TId>
    {
        public TId Id { get; set; }
        public TId CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
