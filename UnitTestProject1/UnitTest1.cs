using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ttt;

namespace ttt.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private const int TestP = 17;
        private const int TestQ = 19;
        private BigInteger _publicKey;
        private BigInteger _privateKey;
        private BigInteger _modulus;

        [TestInitialize]
        public void Init()
        {
            RsaAlgorithm.GenerateKeys(TestP, TestQ, out _publicKey, out _privateKey, out _modulus);
        }

        [TestMethod]
        public void Test_IsPrime()
        {
            Assert.IsTrue(RsaAlgorithm.IsPrime(17), "17 должно быть простым");
            Assert.IsTrue(RsaAlgorithm.IsPrime(19), "19 должно быть простым");
            Assert.IsTrue(RsaAlgorithm.IsPrime(2), "2 должно быть простым");
            Assert.IsTrue(RsaAlgorithm.IsPrime(997), "997 должно быть простым");

            Assert.IsFalse(RsaAlgorithm.IsPrime(1), "1 не является простым");
            Assert.IsFalse(RsaAlgorithm.IsPrime(4), "4 не является простым");
            Assert.IsFalse(RsaAlgorithm.IsPrime(15), "15 не является простым");
            Assert.IsFalse(RsaAlgorithm.IsPrime(100), "100 не является простым");
        }

        [TestMethod]
        public void Test_EncryptDecrypt_EnglishString()
        {
            string original = "Hello World!";
            string encrypted = RsaAlgorithm.EncryptString(original, _publicKey, _modulus);
            string decrypted = RsaAlgorithm.DecryptString(encrypted, _privateKey, _modulus);
            Assert.AreEqual(original, decrypted, "Английская строка должна быть корректно расшифрована");
        }

        [TestMethod]
        public void Test_EncryptDecrypt_RussianString()
        {
            string original = "Привет мир!";
            string encrypted = RsaAlgorithm.EncryptString(original, _publicKey, _modulus);
            string decrypted = RsaAlgorithm.DecryptString(encrypted, _privateKey, _modulus);
            Assert.AreEqual(original, decrypted, "Русская строка должна быть корректно расшифрована");
        }
    }
}