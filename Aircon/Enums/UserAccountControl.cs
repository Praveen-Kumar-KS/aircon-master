using System;

namespace Aircon.Enums
{
    [Flags]
    public enum UserAccountControl : int
    {
        AccountDisabled = 0x00000002,
        LockOut = 0x00000010,
    }
}
