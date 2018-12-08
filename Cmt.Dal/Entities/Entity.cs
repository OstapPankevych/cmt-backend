using System;

namespace Cmt.Dal.Entities
{
    public class Entity<T>
    {
        public Entity() {}
        public Entity(T id) { Id = id; }

        public T Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }
    }
}
