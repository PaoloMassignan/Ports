using System;
using System.Collections.Generic;
using System.Text;

namespace Ports
{
    public interface IRegistry
    {
        void ApplyCsNot(int[] controlBits, int notBit);
        byte Get(int bitIndex);
        void Set(int bitIndex, byte value);
        int AllocateBit();
    }
}
