using System;

namespace Cmt.Bll.DTOs
{
    public class Dto<TId> : IIdDto<TId>
    {
        public TId Id { get; set; }
        public TId CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
