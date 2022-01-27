using System;
using System.Collections.Generic;
using System.Text;

namespace Ports.Circuits
{
    public class MulCircuit : ControlledCircuit
    {
        private int _numberOfBits;
        private IRegistry _registry;

        public IVariable FirstOperand { get; private set; }
        public IVariable SecondOperand { get; private set; }
        public IVariable Result { get; private set; }
        public IVariable Carry { get; private set; }

        public MulCircuit(int numberOfBits, IRegistry registry) : this(numberOfBits, registry, new int[] { })
        {
        }

        public MulCircuit(int numberOfBits, IRegistry registry, int[] controllingBits) :base(controllingBits,registry)
        {
            _numberOfBits = numberOfBits;
            _registry = registry;

            FirstOperand = new Variable(registry);
            for (int i = 0; i < _numberOfBits; i++)
                ((Variable)FirstOperand).AllocateBit(registry.AllocateBit());

            SecondOperand = new Variable(registry);
            for (int i = 0; i < _numberOfBits; i++)
                ((Variable)SecondOperand).AllocateBit(registry.AllocateBit());

            Result = new Variable(registry);
            for (int i = 0; i < 2 * _numberOfBits + 1; i++)
                ((Variable)Result).AllocateBit(registry.AllocateBit());

            Carry = new Variable(registry);
            for (int i = 0; i < _numberOfBits; i++)
                ((Variable)Carry).AllocateBit(registry.AllocateBit());

            for (int i = 0; i < _numberOfBits; i++)
            {
                _subCircuits.Add(new SumCircuit(
                    _numberOfBits, 
                    registry,
                    new int[] { FirstOperand.Bit(i) },
                    SecondOperand,
                    Result.SubVariable(i, _numberOfBits+1),
                    Carry
                    ));
            }
        }

    }
}
