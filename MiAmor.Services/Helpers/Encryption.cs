﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using MiAmor.Services;
using MiAmor.Core;

namespace MiAmor.Services
{

    public class Encryption : IEncryption
    {
        private string passPhrase;
        private string saltValue;
        private string hashAlgorithm;
        private int passwordIterations;
        private string initVector;
        private int keySize;

        private ITokenSettingsService _TokenSettingsService;

        public Encryption( ITokenSettingsService TokenSettingsService)
        {
            _TokenSettingsService = TokenSettingsService;
            TokenSettings TokenSettings = _TokenSettingsService.GetById(1);
            passPhrase = TokenSettings.passPhrase;
            saltValue = TokenSettings.saltValue;
            hashAlgorithm = TokenSettings.hashAlgorithm;
            passwordIterations = TokenSettings.passwordIterations;
            initVector = TokenSettings.initVector;
            keySize = TokenSettings.keySize;
                       

        }

        // <summary>
        // Encrypts specified plaintext using Rijndael symmetric key algorithm
        // and returns a base64-encoded result.
        // </summary>
        // <param name="plainText">
        // Plaintext value to be encrypted.
        // </param>
        // <param name="passPhrase">
        // Passphrase from which a pseudo-random password will be derived. The 
        // derived password will be used to generate the encryption key. 
        // Passphrase can be any string. In this example we assume that this 
        // passphrase is an ASCII string.
        // </param>
        // <param name="saltValue">
        // Salt value used along with passphrase to generate password. Salt can 
        // be any string. In this example we assume that salt is an ASCII string.
        // </param>
        // <param name="hashAlgorithm">
        // Hash algorithm used to generate password. Allowed values are: "MD5" and
        // "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
        // </param>
        // <param name="passwordIterations">
        // Number of iterations used to generate password. One or two iterations
        // should be enough.
        // </param>
        // <param name="initVector">
        // Initialization vector (or IV). This value is required to encrypt the 
        // first block of plaintext data. For RijndaelManaged class IV must be 
        // exactly 16 ASCII characters long.
        // </param>
        // <param name="keySize">
        // Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
        // Longer keys are more secure than shorter keys.
        // </param>
        // <returns>
        // Encrypted value formatted as a base64-encoded string.
        // </returns>
        public string Encrypt(string plainText)
        {
           
            // can be 192 or 128
            // Convert strings into byte arrays.
            // Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
            // encoding.
            byte[] initVectorBytes = null;
            initVectorBytes = Encoding.ASCII.GetBytes(initVector);

            byte[] saltValueBytes = null;
            saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our plaintext into a byte array.
            // Let us assume that plaintext contains UTF8-encoded characters.
            byte[] plainTextBytes = null;
            plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // First, we must create a password, from which the key will be derived.
            // This password will be generated from the specified passphrase and 
            // salt value. The password will be created using the specified hash 
            // algorithm. Password creation can be done in several iterations.
            PasswordDeriveBytes password = default(PasswordDeriveBytes);
            password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = null;
            keyBytes = password.GetBytes(keySize / 8);

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = default(RijndaelManaged);
            symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate encryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform encryptor = default(ICryptoTransform);
            encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = default(MemoryStream);
            memoryStream = new MemoryStream();

            // Define cryptographic stream (always use Write mode for encryption).
            CryptoStream cryptoStream = default(CryptoStream);
            cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            // Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            // Finish encrypting.
            cryptoStream.FlushFinalBlock();

            // Convert our encrypted data from a memory stream into a byte array.
            byte[] cipherTextBytes = null;
            cipherTextBytes = memoryStream.ToArray();

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert encrypted data into a base64-encoded string.
            string cipherText = null;
            cipherText = Convert.ToBase64String(cipherTextBytes);

            // Return encrypted string.
            return cipherText;
        }

        // <summary>
        // Decrypts specified ciphertext using Rijndael symmetric key algorithm.
        // </summary>
        // <param name="cipherText">
        // Base64-formatted ciphertext value.
        // </param>
        // <param name="passPhrase">
        // Passphrase from which a pseudo-random password will be derived. The 
        // derived password will be used to generate the encryption key. 
        // Passphrase can be any string. In this example we assume that this 
        // passphrase is an ASCII string.
        // </param>
        // <param name="saltValue">
        // Salt value used along with passphrase to generate password. Salt can 
        // be any string. In this example we assume that salt is an ASCII string.
        // </param>
        // <param name="hashAlgorithm">
        // Hash algorithm used to generate password. Allowed values are: "MD5" and
        // "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
        // </param>
        // <param name="passwordIterations">
        // Number of iterations used to generate password. One or two iterations
        // should be enough.
        // </param>
        // <param name="initVector">
        // Initialization vector (or IV). This value is required to encrypt the 
        // first block of plaintext data. For RijndaelManaged class IV must be 
        // exactly 16 ASCII characters long.
        // </param>
        // <param name="keySize">
        // Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
        // Longer keys are more secure than shorter keys.
        // </param>
        // <returns>
        // Decrypted string value.
        // </returns>
        // <remarks>
        // Most of the logic in this function is similar to the Encrypt 
        // logic. In order for decryption to work, all parameters of this function
        // - except cipherText value - must match the corresponding parameters of 
        // the Encrypt function which was called to generate the 
        // ciphertext.
        // </remarks>
        public string Decrypt(string cipherText)
        {

            // Convert strings defining encryption key characteristics into byte
            // arrays. Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8
            // encoding.
          
            byte[] initVectorBytes = null;
            initVectorBytes = Encoding.ASCII.GetBytes(initVector);

            byte[] saltValueBytes = null;
            saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our ciphertext into a byte array.
            byte[] cipherTextBytes = null;
            cipherTextBytes = Convert.FromBase64String(cipherText);

            // First, we must create a password, from which the key will be 
            // derived. This password will be generated from the specified 
            // passphrase and salt value. The password will be created using
            // the specified hash algorithm. Password creation can be done in
            // several iterations.
            PasswordDeriveBytes password = default(PasswordDeriveBytes);
            password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = null;
            keyBytes = password.GetBytes(keySize / 8);

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = default(RijndaelManaged);
            symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate decryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform decryptor = default(ICryptoTransform);
            decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = default(MemoryStream);
            memoryStream = new MemoryStream(cipherTextBytes);

            // Define memory stream which will be used to hold encrypted data.
            CryptoStream cryptoStream = default(CryptoStream);
            cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold ciphertext;
            // plaintext is never longer than ciphertext.
            byte[] plainTextBytes = null;
            plainTextBytes = new byte[cipherTextBytes.Length + 1];

            // Start decrypting.
            int decryptedByteCount = 0;
            decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert decrypted data into a string. 
            // Let us assume that the original plaintext string was UTF8-encoded.
            string plainText = null;
            plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

            // Return decrypted string.
            return plainText;
        }
      
        public string EncryptStrLink(string StrEnc)
        {

            string StrRet = this.Encrypt(StrEnc);
            StrRet = StrRet.Replace("+", "]]]");
            StrRet = StrRet.Replace("?", "###");
            StrRet = StrRet.Replace("&", "$$$");
            //StrRet = StrRet.Replace("=", "!!!")
            return StrRet;

        }
        public string DecryptStrLink(string StrDec)
        {
            StrDec = StrDec.Replace("]]]", "+");
            StrDec = StrDec.Replace("###", "?");
            StrDec = StrDec.Replace("$$$", "&");
            //StrDec = StrDec.Replace("!!!", "=")
            string StrRet = this.Decrypt(StrDec);

            return StrRet;

        }

    }

  
}