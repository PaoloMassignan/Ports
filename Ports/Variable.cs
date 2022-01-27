using System;
using System.Collections.Generic;
using System.Text;

namespace Ports
{
    public class Variable : IVariable
    {
        private IRegistry _registry;
        private List<int> _bitIndexes;

        public Variable(IRegistry registry)
        {
            this._registry = registry;
            this._bitIndexes = new List<int>();
        }

        public Variable(IRegistry registry, int numberOfBits)
        {
            this._registry = registry;
            this._bitIndexes = new List<int>();
            for (int i = 0; i < numberOfBits; i++)
                this.AllocateBit(registry.AllocateBit());
        }


        public UInt64 GetUInt64()
        {
            UInt64 result = 0;
            UInt64 mask = 1;
            for (int i = 0; i < _bitIndexes.Count; i++)
            {
                if (_registry.Get(_bitIndexes[i])> 0)
                {
                    result |= mask;
                }
                mask <<= 1;
            }
            return result;
        }

        public void SetUInt64(UInt64 v)
        {
            UInt64 mask = 1;
            for (int i = 0; i < _bitIndexes.Count; i++ )
            {
                if ((v&mask)>0)
                {
                    _registry.Set(_bitIndexes[i], 1);
                }
                mask <<= 1;
            }
        }

        public void AllocateBit(int bitIndex)
        {
            _bitIndexes.Add(bitIndex);
        }

        public int Bit(int i)
        {
            if (i < 0 || i >= _bitIndexes.Count)
                throw new Exception();
            return _bitIndexes[i];
        }

        public IVariable SubVariable(int offset, int numberOfBits)
        {
            Variable subVar = new Variable(_registry);
            for (int i = 0; i < numberOfBits; i++)
            {
                subVar._bitIndexes.Add(this._bitIndexes[i + offset]);
            }
            return subVar;
        }

        public override string ToString()
        {
            return GetUInt64().ToString();
        }
    }
}
