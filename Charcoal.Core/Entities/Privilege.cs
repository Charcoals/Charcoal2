using System;

namespace Charcoal.Core.Entities
{
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