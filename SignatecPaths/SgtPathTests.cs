using System;
using NUnit.Framework;

namespace SignatecPaths
{
    [TestFixture]
    public class SgtPathTests
    {
        [Test]
        public void InvalidEmptyCtorArg()
        {
            Assert.Throws<ArgumentException>(() => new SgtPath(""));
        }

        [Test]
        public void InvalidFormatCtorArg1()
        {
            Assert.Throws<ArgumentException>(() => new SgtPath("abc"));
        }

        [Test]
        public void InvalidFormatCtorArg2()
        {
            Assert.Throws<ArgumentException>(() => new SgtPath("//"));
        }

        [Test]
        public void InvalidFormatCtorArg3()
        {
            Assert.Throws<ArgumentException>(() => new SgtPath("abcd/"));
        }

        [Test]
        public void InvalidFormatCtorArg4()
        {
            Assert.Throws<ArgumentException>(() => new SgtPath("abc1"));
        }

        [Test]
        public void InvalidEmptyCdArg()
        {
            var path = new SgtPath("/");

            var ex = Assert.Throws<ArgumentException>(() => path.Cd(""));

            Assert.That(ex.Message == SgtPath.InvalidArgumentFormatMessage, () => ex.Message);
        }

        [Test]
        public void InvalidFormatCdArg1()
        {
            var path = new SgtPath("/");

            var ex = Assert.Throws<ArgumentException>(() => path.Cd("../"));

            Assert.That(ex.Message == SgtPath.InvalidArgumentFormatMessage, () => ex.Message);
        }

        [Test]
        public void InvalidFormatCdArg2()
        {
            var path = new SgtPath("/");

            var ex = Assert.Throws<ArgumentException>(() => path.Cd("//"));

            Assert.That(ex.Message == SgtPath.InvalidArgumentFormatMessage, () => ex.Message);
        }

        [Test]
        public void InvalidFormatCdArg3()
        {
            var path = new SgtPath("/");

            var ex = Assert.Throws<ArgumentException>(() => path.Cd("abc/"));

            Assert.That(ex.Message == SgtPath.InvalidArgumentFormatMessage, () => ex.Message);
        }

        [Test]
        public void InvalidFormatCdArg4()
        {
            var path = new SgtPath("/");

            var ex = Assert.Throws<ArgumentException>(() => path.Cd("/abc1"));

            Assert.That(ex.Message == SgtPath.InvalidArgumentFormatMessage, () => ex.Message);
        }

        [Test]
        public void InvalidFormatCdArg5()
        {
            var path = new SgtPath("/");

            var ex = Assert.Throws<ArgumentException>(() => path.Cd("./"));

            Assert.That(ex.Message == SgtPath.InvalidArgumentFormatMessage, () => ex.Message);
        }

        [Test]
        public void InvalidFormatCdArg6()
        {
            var path = new SgtPath("/");

            var ex = Assert.Throws<ArgumentException>(() => path.Cd("abc"));

            Assert.That(ex.Message == SgtPath.InvalidArgumentFormatMessage, () => ex.Message);
        }

        [Test]
        public void InvalidNestingCdArg()
        {
            var path = new SgtPath("/");

            var ex = Assert.Throws<ArgumentException>(() => path.Cd(".."));

            Assert.That(ex.Message == SgtPath.InvalidArgumentNestingMessage, () => ex.Message);
        }

        [Test]
        public void CdUpToRootL1()
        {
            var path = new SgtPath("/abc");

            Assert.AreEqual(path.Cd(".."), "/"); 
        }

        [Test]
        public void CdUpToRootL2()
        {
            var path = new SgtPath("/abc/def");

            Assert.AreEqual(path.Cd("../.."), "/");
        }

        [Test]
        public void CdUpToRootL1DownF()
        {
            var path = new SgtPath("/abc");

            Assert.AreEqual(path.Cd("../def"), "/def");
        }

        [Test]
        public void CdUpToRootL2DownF()
        {
            var path = new SgtPath("/abc/def");

            Assert.AreEqual(path.Cd("../../def"), "/def");
        }

        [Test]
        public void CdUpTo1()
        {
            var path = new SgtPath("/abc/def");

            Assert.AreEqual(path.Cd(".."), "/abc");
        }

        [Test]
        public void CdUpTo2()
        {
            var path = new SgtPath("/abc/def/hgj");

            Assert.AreEqual(path.Cd("../.."), "/abc");
        }

        [Test]
        public void CdUpTo1AndDown()
        {
            var path = new SgtPath("/abc/def");

            Assert.AreEqual(path.Cd("../wxy"), "/abc/wxy");
        }

        [Test]
        public void CdUpTo2AndDown()
        {
            var path = new SgtPath("/abc/def/hgj");

            Assert.AreEqual(path.Cd("../../wxy"), "/abc/wxy");
        }

        [Test]
        public void CdDownTo0From0()
        {
            var path = new SgtPath("/");

            Assert.AreEqual(path.Cd("."), "/");
        }

        [Test]
        public void CdDownTo0From1()
        {
            var path = new SgtPath("/abc");

            Assert.AreEqual(path.Cd("."), "/abc");
        }

        [Test]
        public void CdDownTo1From0()
        {
            var path = new SgtPath("/");

            Assert.AreEqual(path.Cd("./xyz"), "/xyz");
        }

        [Test]
        public void CdDownTo1From1()
        {
            var path = new SgtPath("/abc");

            Assert.AreEqual(path.Cd("./xyz"), "/abc/xyz");
        }

        [Test]
        public void CdToAbs1()
        {
            var path = new SgtPath("/aBc");

            Assert.AreEqual(path.Cd("/"), "/");
        }

        [Test]
        public void CdToAbs2()
        {
            var path = new SgtPath("/");

            Assert.AreEqual(path.Cd("/aBc/xyz"), "/aBc/xyz");
        }

        [Test]
        public void CdToAbs3()
        {
            var path = new SgtPath("/abc/xyz");

            Assert.AreEqual(path.Cd("/abcf/xyzf"), "/abcf/xyzf");
        }
    }
        
}
