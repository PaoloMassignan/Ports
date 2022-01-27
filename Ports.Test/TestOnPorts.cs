using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Ports;
using Ports.Circuits;
using Ports.Registry;

namespace Ports.Test
{
    [TestClass]
    public class TestOnPorts
    {
        [TestMethod]
        public void TestCNot()
        {
            IRegistry registry = new UInt64Registry();

            registry.Initialize(3);
            registry.ApplyCsNot(new int[] { 0 }, 1);
            registry.GetUInt64().Should().Be(1);
        }

        [TestMethod]
        public void TestCCNot()
        {
            IRegistry registry = new UInt64Registry();

            registry.Initialize(7);
            registry.ApplyCsNot(new int[] { 0,1 }, 2);
            registry.GetUInt64().Should().Be(3);
        }

        [TestMethod]
        public void TestSum2_0()
        {
            int numberOfBits = 3;
            IRegistry registry = new UInt64Registry();
            var sumCircuit = new SumCircuit(numberOfBits, registry);
            IVariable firstOperand = sumCircuit.FirstOperand;
            IVariable secondOperand = sumCircuit.SecondOperand;
            IVariable carry = sumCircuit.Carry;

            firstOperand.SetUInt64(2);
            secondOperand.SetUInt64(0);
            sumCircuit.Run();

            secondOperand.GetUInt64().Should().Be(2);
        }

        [TestMethod]
        public void TestSum2_0_Controlled()
        {
            int numberOfBits = 3;
            IRegistry registry = new UInt64Registry();
            var ctrl = registry.AllocateBit();
            registry.Set(ctrl, 1);

            var sumCircuit = new SumCircuit(numberOfBits, registry, new int[] { ctrl });
            IVariable firstOperand = sumCircuit.FirstOperand;
            IVariable secondOperand = sumCircuit.SecondOperand;
            IVariable carry = sumCircuit.Carry;

            firstOperand.SetUInt64(2);
            secondOperand.SetUInt64(0);
            sumCircuit.Run();

            secondOperand.GetUInt64().Should().Be(2);
        }

        [TestMethod]
        public void TestSum3()
        {
            int numberOfBits = 3;
            IRegistry registry = new UInt64Registry();
            var sumCircuit = new SumCircuit(numberOfBits, registry);
            IVariable firstOperand = sumCircuit.FirstOperand;
            IVariable secondOperand = sumCircuit.SecondOperand;
            IVariable carry = sumCircuit.Carry;

            firstOperand.SetUInt64(1);
            secondOperand.SetUInt64(2);
            sumCircuit.Run();

            secondOperand.GetUInt64().Should().Be(3);
        }

        [TestMethod]
        public void TestMul3()
        {
            int numberOfBits = 3;
            IRegistry registry = new UInt64Registry();
            var mulCircuit = new MulCircuit(numberOfBits, registry);
            IVariable firstOperand = mulCircuit.FirstOperand;
            IVariable secondOperand = mulCircuit.SecondOperand;
            IVariable result = mulCircuit.Result;
            IVariable carry = mulCircuit.Carry;

            firstOperand.SetUInt64(1);
            secondOperand.SetUInt64(2);
            mulCircuit.Run();

            result.GetUInt64().Should().Be(2);
        }



    }
}
