using System;

namespace Charcoal.Core.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public bool IsNew { get { return Id == 0; } }
    }

    [Flags]
    public enum Privilege
    {
        Undefined = 0,
        Admin,
        Product,
        Developer,
        Tester,
        All
    }
}
