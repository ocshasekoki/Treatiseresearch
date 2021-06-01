using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;

class Text
{
    static void main()
    {
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            byte[] nonce = new byte[16];

            rng.GetBytes(nonce);

            Console.WrieteLine(BitConverter.ToString(nonce));
        }
    }
}

