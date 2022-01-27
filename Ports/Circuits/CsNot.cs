using System;
using System.Collections.Generic;
using System.Text;

namespace Ports.Circuits
{
    public class CsNot:ControlledCircuit
    {
        public CsNot(int[] controllingBits, int notBit, IRegistry registry) : base(controllingBits, registry)
        {
            NotBit = notBit;
        }

        public int NotBit { get; }

        public override void Run()
        {
            Registry.ApplyCsNot(ControllingBits, NotBit);
        }
    }
}
