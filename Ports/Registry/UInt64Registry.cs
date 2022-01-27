using System;

namespace Ports.Registry
{
    public class UInt64Registry : IRegistry
    {
		public UInt64 Value { get; set; }
		private int _currentBit = 0;

		public string BinString
		{
			get
			{
				UInt64 val = Value;
				string res = "";
				for (int i =0; i < _currentBit; i++)
				{
					if (val % 2 == 1)
						res = "1" + res;
					else
						res = "0" + res;
					val /= 2;
				}
				return res;
			}
		}


		private bool _testBit(int bit)
		{
			UInt64 val = Value;
			val = val >> bit;
			return (val % 2) == 1;
		}

		public void copy(int a, int b)
		{
			if (_testBit(a))
			{
				if (_testBit(b))
					return;
				else
				{
					UInt64 l = (UInt64)Math.Pow(2, b);
					Value += l;
				}
			}
			else
			{
				if (!_testBit(b))
					return;
				else
				{
					UInt64 l = (UInt64)Math.Pow(2, b);
					Value -= l;
				}

			}

		}


		public void Clr(int a)
		{
			if (_testBit(a))
			{
				UInt64 l = (UInt64)Math.Pow(2, a);
				Value -= l;
			}

		}

		public void ApplyCsNot(int[] controlBits, int notBit)
        {
			var controlValue = true;
			for (int i = 0; i < controlBits.Length; i++)
            {
				if (!_testBit(controlBits[i]))
					controlValue = false;
            }
			if (controlValue)
			{
				UInt64 l = (UInt64)Math.Pow(2, notBit);
				if (_testBit(notBit))
				{
					Value -= l;
				}
				else
				{
					Value += l;
				}
			}

		}

        public byte Get(int bitIndex)
        {
			return _testBit(bitIndex) ? (byte) 1 : (byte)0;
        }

        public void Set(int bitIndex, byte value)
        {
			if (_testBit(bitIndex))
			{
				if (value == 1)
					return;
				else
				{
					UInt64 l = (UInt64)Math.Pow(2, bitIndex);
					Value -= l;
				}
			}
			else
			{
				if (value == 0)
					return;
				else
				{
					UInt64 l = (UInt64)Math.Pow(2, bitIndex);
					Value += l;
				}

			}
		}

        public int AllocateBit()
        {
			_currentBit++;
			return _currentBit - 1;
        }
    }
}
