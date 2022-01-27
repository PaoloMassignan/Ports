using System;
using System.Collections.Generic;
using System.Text;

namespace Ports.Registry
{
    public static class RegistryExtension
    {
        public static UInt64 GetUInt64(this IRegistry registry)
        {
            UInt64 value = 0;
            int mult = 1;
            for (int i = 0; i < 64; i++)
            {
                value += (UInt64)(registry.Get(i) * mult);
                mult <<= 1;
            }
            return value;
        }

        public static void Initialize(this IRegistry registry, UInt64 value)
        {
            UInt64 mask = 1;
            for (int i =0; i < 64; i++)
            {
                if ((value&mask) > 0)
                {
                    registry.Set(i, 1);
                }
                mask <<= 1;
            }
        }

    }
}
