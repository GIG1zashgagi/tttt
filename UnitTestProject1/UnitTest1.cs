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

        [TestMethod]
        public void Test_EncryptDecrypt_MixedString()
        {
            string original = "Hello Привет 123!@# АБВ abc";
            string encrypted = RsaAlgorithm.EncryptString(original, _publicKey, _modulus);
            string decrypted = RsaAlgorithm.DecryptString(encrypted, _privateKey, _modulus);
            Assert.AreEqual(original, decrypted, "Смешанная строка должна быть корректно расшифрована");
        }

        [TestMethod]
        public void Test_EmptyString()
        {
            string original = "";
            string encrypted = RsaAlgorithm.EncryptString(original, _publicKey, _modulus);
            string decrypted = RsaAlgorithm.DecryptString(encrypted, _privateKey, _modulus);
            Assert.AreEqual("", encrypted, "Пустая строка при шифровании должна оставаться пустой");
            Assert.AreEqual("", decrypted, "Пустая строка при дешифровании должна оставаться пустой");
        }

        [TestMethod]
        public void Test_SingleChar_English()
        {
            string original = "A";
            string encrypted = RsaAlgorithm.EncryptString(original, _publicKey, _modulus);
            string decrypted = RsaAlgorithm.DecryptString(encrypted, _privateKey, _modulus);
            Assert.AreEqual(original, decrypted);
        }

        [TestMethod]
        public void Test_SingleChar_Russian()
        {
            string original = "Я";
            string encrypted = RsaAlgorithm.EncryptString(original, _publicKey, _modulus);
            string decrypted = RsaAlgorithm.DecryptString(encrypted, _privateKey, _modulus);
            Assert.AreEqual(original, decrypted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_InvalidP()
        {
            BigInteger temp1, temp2, temp3;
            RsaAlgorithm.GenerateKeys(4, 19, out temp1, out temp2, out temp3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_InvalidQ()
        {
            BigInteger temp1, temp2, temp3;
            RsaAlgorithm.GenerateKeys(17, 21, out temp1, out temp2, out temp3);
        }

        [TestMethod]
        public void Test_DecryptInvalidData()
        {
            string result = RsaAlgorithm.DecryptString("abc 123 xyz", _privateKey, _modulus);
            Assert.AreEqual("", result, "Мусорные данные должны возвращать пустую строку");
        }

        [TestMethod]
        public void Test_LongString()
        {
            string original = new string('A', 100) + new string('Б', 100);
            string encrypted = RsaAlgorithm.EncryptString(original, _publicKey, _modulus);
            string decrypted = RsaAlgorithm.DecryptString(encrypted, _privateKey, _modulus);
            Assert.AreEqual(original, decrypted, "Длинная строка должна корректно обрабатываться");
        }

        [TestMethod]
        public void Test_SpecialCharacters()
        {
            string original = "!@#$%^&*()_+{}[]|\\:;\"'<>,.?/~`";
            string encrypted = RsaAlgorithm.EncryptString(original, _publicKey, _modulus);
            string decrypted = RsaAlgorithm.DecryptString(encrypted, _privateKey, _modulus);
            Assert.AreEqual(original, decrypted, "Специальные символы должны корректно обрабатываться");
        }

        [TestMethod]
        public void Test_Numbers()
        {
            string original = "01234567890123456789";
            string encrypted = RsaAlgorithm.EncryptString(original, _publicKey, _modulus);
            string decrypted = RsaAlgorithm.DecryptString(encrypted, _privateKey, _modulus);
            Assert.AreEqual(original, decrypted);
        }

        [TestMethod]
        public void Test_NewLine()
        {
            string original = "Первая строка\nВторая строка\r\nТретья строка";
            string encrypted = RsaAlgorithm.EncryptString(original, _publicKey, _modulus);
            string decrypted = RsaAlgorithm.DecryptString(encrypted, _privateKey, _modulus);
            Assert.AreEqual(original, decrypted);
        }
    }
}