namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get ; set; }
        public DateTime CreatedUTCDate { get; set; }
        public DateTime UpdatedUTCDate { get; set; }
        public int LastUpdatedByUserId { get; set; }
    }
}
