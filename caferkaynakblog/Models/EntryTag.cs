namespace caferkaynakblog.Models
{
    public class EntryTag
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
        public int EntryId { get; set; }
        public virtual Entry Entry { get; set; }
    }
}
