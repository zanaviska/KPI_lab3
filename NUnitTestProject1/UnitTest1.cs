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
            Assert.IsNotNull(new MultipleBinaryFlag(17179868704, false));
        }
        
        [Test]
        public void GetTypeTest()
        {
            MultipleBinaryFlag flag1 = new MultipleBinaryFlag(10);
            MultipleBinaryFlag flag2 = new MultipleBinaryFlag(10, true);
            MultipleBinaryFlag flag3 = new MultipleBinaryFlag(10, false);

            Assert.AreEqual(flag1.GetType().ToString(), "IIG.BinaryFlag.MultipleBinaryFlag");
            Assert.AreEqual(flag2.GetType().ToString(), "IIG.BinaryFlag.MultipleBinaryFlag");
            Assert.AreEqual(flag3.GetType().ToString(), "IIG.BinaryFlag.MultipleBinaryFlag");
        }

        [Test]
        public void TestEquality()
        {
            MultipleBinaryFlag[] flag = new MultipleBinaryFlag[6];
            
            flag[0] = new MultipleBinaryFlag(10);
            flag[1] = new MultipleBinaryFlag(10, true);
            flag[2] = new MultipleBinaryFlag(10, false);
            flag[3] = new MultipleBinaryFlag(11);
            flag[4] = new MultipleBinaryFlag(11, true);
            flag[5] = new MultipleBinaryFlag(11, false);
            

            //equalty of the same objects(not overrided .Equal())
            for (int i = 0; i < 6; i++)
                for (int j = i + 1; j < 6; j++)
                    Assert.AreNotEqual(flag[i], flag[j]);

            //equality of flags
            for (int i = 0; i < 6; i++)
                for (int j = i + 1; j < 6; j++)
                    if ((j % 3 == 2 && i%3 != 2) || (i%3 == 2 && j%3 != 2))
                        Assert.AreNotEqual(flag[i].GetFlag(), flag[j].GetFlag());
                    else
                        Assert.AreEqual(flag[i].GetFlag(), flag[j].GetFlag());
        }
        [Test]
        public void TestFlagChanging()
        {
            MultipleBinaryFlag mbf = new MultipleBinaryFlag(10, false);

            // Test that flag state if any position is true (through SetFlag method)
            mbf.SetFlag(3);
            Assert.IsFalse(mbf.GetFlag());

            for (ulong i = 0; i < 10; i++)
                mbf.ResetFlag(i);
            Assert.IsTrue(mbf.GetFlag());
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
        [Test]
        public void TestDispose()
        {
            MultipleBinaryFlag mbf = new MultipleBinaryFlag(8);
            mbf.Dispose();
            Assert.IsNotNull(mbf);
        }
    }
}