namespace Charcoal.Core.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public bool IsNew { get { return Id == 0; } }
    }
}
