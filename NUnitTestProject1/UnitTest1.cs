using NUnit.Framework;
using IIG.BinaryFlag;
using System;

namespace NUnitTestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConstructionFails()
        {
            //arg is less than 2
            Assert.Throws<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(1));
            //arg is too large
            Assert.Throws<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(17179868705));
        }

        [Test]
        public void SuccesfullConstructor()
        {
            //min available lemgth
            Assert.IsNotNull(new MultipleBinaryFlag(2));
            //max available length
            Assert.IsNotNull(new MultipleBinaryFlag(17179868704));
            //with second parameter
            Assert.IsNotNull(new MultipleBinaryFlag(17179868704, true));
            //test another all 3 branch of constructor flow
            Assert.IsNotNull(new MultipleBinaryFlag(17179868704, true));
            Assert.IsNotNull(new MultipleBinaryFlag(17179868704, false));
            Assert.IsNotNull(new MultipleBinaryFlag(50, true));
            Assert.IsNotNull(new MultipleBinaryFlag(50, false));
            Assert.IsNotNull(new MultipleBinaryFlag(10, true));
            Assert.IsNotNull(new MultipleBinaryFlag(10, false));
        }
        
        [Test]
        public void TestFlagChanging()
        {
            MultipleBinaryFlag mbf = new MultipleBinaryFlag(10, false);

            // Test that flag state if any position is true (through SetFlag method)
            mbf.SetFlag(3);
            Assert.IsFalse(mbf.GetFlag());

            for (ulong i = 0; i < 10; i++)
                mbf.SetFlag(i);
            Assert.IsTrue(mbf.GetFlag());


            // Test that flag state if any position is false (through SetFlag method)
            mbf.SetFlag(3);
            Assert.IsTrue(mbf.GetFlag());

            for (ulong i = 0; i < 10; i++)
                mbf.ResetFlag(i);
            Assert.IsFalse(mbf.GetFlag());
        }
        [Test]
        public void TestFlagChangeError()
        {
            MultipleBinaryFlag mbf = new MultipleBinaryFlag(10, false);
            Assert.Throws<ArgumentOutOfRangeException>(() => mbf.SetFlag(11));
        }
        [Test]
        public void TestToString()
        {
            MultipleBinaryFlag mbf = new MultipleBinaryFlag(4, false);
            MultipleBinaryFlag mbt = new MultipleBinaryFlag(4, true);
            Assert.AreEqual(mbt.ToString(), "TTTT");
            Assert.AreEqual(mbf.ToString(), "FFFF");

            //test to string after flag setting
            mbf.SetFlag(1);
            Assert.AreEqual(mbf.ToString(), "FTFF");
            
            for (ulong i = 0; i < 4; i++)
                mbf.SetFlag(i);
            Assert.AreEqual(mbf.ToString(), "TTTT");

            //test to string after flag resettting
            mbf.ResetFlag(1);
            Assert.AreEqual(mbf.ToString(), "TFTT");

            for (ulong i = 0; i < 4; i++)
                mbf.ResetFlag(i);
            Assert.AreEqual(mbf.ToString(), "FFFF");
        }
    }
}
