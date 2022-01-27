using System;
using System.Collections.Generic;
using System.Text;

namespace Ports
{
    public interface IVariable
    {
        void SetUInt64(UInt64 v);
        UInt64 GetUInt64();
        int Bit(int i);
        IVariable SubVariable(int i, int numberOfBits);
    }
}
