using System;
using System.Collections.Generic;
using System.Text;

namespace Ports.Circuits
{
    public class ControlledCircuit
    {
        protected List<ControlledCircuit> _subCircuits = new List<ControlledCircuit>();
        public int[] ControllingBits { get; }
        public IRegistry Registry { get; }

        protected int[] _merge(int[] controllingBits, int b)
        {
            int[] tmp = new int[controllingBits.Length + 1];
            Array.Copy(controllingBits, tmp, controllingBits.Length);
            tmp[controllingBits.Length] = b;
            return tmp;
        }
        protected int[] _merge(int[] controllingBits, int b0, int b1)
        {
            int[] tmp = new int[controllingBits.Length + 2];
            Array.Copy(controllingBits, tmp, controllingBits.Length);
            tmp[controllingBits.Length] = b0;
            tmp[controllingBits.Length + 1] = b1;
            return tmp;
        }

        public ControlledCircuit(int[] controllingBits, IRegistry registry)
        {
            this.ControllingBits = controllingBits;
            Registry = registry;
        }
        public virtual void Run()
        {
            foreach (var subCircuit in _subCircuits)
                subCircuit.Run();
        }
    }
}
