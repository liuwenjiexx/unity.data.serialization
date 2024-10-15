using System;

namespace Unity
{
    [Flags]
    public enum SerializableOptions
    {
        None = 0,
        Field = 1 << 0,
        Property = 1 << 1,
    }
}
