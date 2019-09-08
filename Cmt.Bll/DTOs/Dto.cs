using System;

namespace Cmt.Bll.DTOs
{
    public class Dto<TId>
    {
        public TId Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
    }
}
