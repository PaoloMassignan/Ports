using System;
using System.Collections.Generic;
using System.Text;

namespace Ports.Circuits
{
    public class SumCircuit : ControlledCircuit
    {
        private int _numberOfBits;
        private IRegistry _registry;

        public IVariable FirstOperand { get; private set; }
        public IVariable SecondOperand { get; private set; }
        public IVariable Carry { get; private set; }

        public SumCircuit(int numberOfBits, IRegistry registry):this(numberOfBits,registry,new int[] { })
        {
        }

        public SumCircuit(int numberOfBits, IRegistry registry, int[] controllingBits) : 
            this(numberOfBits,registry,controllingBits, new Variable(registry,numberOfBits),new Variable(registry,numberOfBits+1),new Variable(registry,numberOfBits) )
        {
        }


        public SumCircuit(int numberOfBits, IRegistry registry, int[] controllingBits, IVariable firstOperand, IVariable secondOperand, IVariable carry) : base(controllingBits, registry)
        {
            this._numberOfBits = numberOfBits;
            this._registry = registry;
            this.FirstOperand = firstOperand;
            this.SecondOperand = secondOperand;
            this.Carry = carry;

            for (int i = 1; i < _numberOfBits; i++)
            {
                _carry(Carry.Bit(i), SecondOperand.Bit(i - 1), FirstOperand.Bit(i - 1), Carry.Bit(i - 1));
            }
            _carry(SecondOperand.Bit(_numberOfBits), SecondOperand.Bit(_numberOfBits - 1), FirstOperand.Bit(_numberOfBits - 1), Carry.Bit(_numberOfBits - 1));

            _registry.ApplyCsNot(new int[] { FirstOperand.Bit(_numberOfBits - 1) }, SecondOperand.Bit(_numberOfBits - 1));

            _sum(SecondOperand.Bit(_numberOfBits - 1), FirstOperand.Bit(_numberOfBits - 1), Carry.Bit(_numberOfBits - 1));

            for (int i = _numberOfBits - 2; i > -1; i--)
            {
                _carry1(Carry.Bit(i + 1), SecondOperand.Bit(i), FirstOperand.Bit(i), Carry.Bit(i));
                _sum(SecondOperand.Bit(i), FirstOperand.Bit(i), Carry.Bit(i));
            }

        }


        public void _sum(int a, int b, int c)
        {
            _subCircuits.Add(new CsNot(_merge(ControllingBits, b), a,Registry));
            _subCircuits.Add(new CsNot(_merge(ControllingBits, c), a, Registry));
        }


        public void _carry(int a, int b, int c, int d)
        {
            _subCircuits.Add(new CsNot(_merge(ControllingBits, c,b), a, Registry));
            _subCircuits.Add(new CsNot(_merge(ControllingBits, c), b, Registry));
            _subCircuits.Add(new CsNot(_merge(ControllingBits, d,b), a, Registry));
        }

        public void _carry1(int a, int b, int c, int d)
        {
            _subCircuits.Add(new CsNot(_merge(ControllingBits, d, b), a, Registry));
            _subCircuits.Add(new CsNot(_merge(ControllingBits, c), b, Registry));
            _subCircuits.Add(new CsNot(_merge(ControllingBits, c, b), a, Registry));
        }

    }
}
