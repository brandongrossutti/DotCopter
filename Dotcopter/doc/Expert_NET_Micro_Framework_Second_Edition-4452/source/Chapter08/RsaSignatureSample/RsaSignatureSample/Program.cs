using System.Text;
using Microsoft.SPOT;
using Microsoft.SPOT.Cryptography;

namespace RsaSignatureSample
{
    public class Program
    {
        public static void Main()
        {
            //the keys are generated by the metadataprocessor.exe in tools directory of the MF
            byte[] modulus = new byte[] { 0x15, 0x74, 0x57, 0xb7, 0xb5, 0x9e, 0x62, 0xd9, 0x4f, 0xa2, 0x75, 0x67, 0x60, 0x6b, 0x90, 0xc3, 0x78, 0xb5, 0x23, 0x15, 0x64, 0x1c, 0x45, 0x4e, 0x4f, 0xcd, 0xb3, 0xed, 0x8e, 0x13, 0x01, 0xb1, 0x84, 0xbb, 0xf0, 0x66, 0x5e, 0x7a, 0xe3, 0xfc, 0x13, 0x95, 0x17, 0x5c, 0x63, 0x23, 0xef, 0x0e, 0xa0, 0xdd, 0xc3, 0x51, 0x1c, 0x47, 0x03, 0xed, 0x24, 0x65, 0xe1, 0x16, 0xc9, 0x9d, 0x79, 0x9b, 0x60, 0x2e, 0xb1, 0x8b, 0x6b, 0x7d, 0x8d, 0xdf, 0x23, 0xd8, 0x41, 0xc6, 0x70, 0x8d, 0x86, 0x11, 0x0b, 0x4b, 0x18, 0xc4, 0xbd, 0x75, 0x30, 0x17, 0xfc, 0x2f, 0x36, 0x83, 0x2a, 0x4e, 0x48, 0x41, 0xe3, 0x00, 0x66, 0xc6, 0x2e, 0x32, 0x80, 0x03, 0x11, 0x95, 0xbb, 0x31, 0x66, 0x4e, 0x4d, 0x15, 0xbf, 0xdf, 0x5a, 0xb4, 0xaf, 0xcf, 0xc7, 0xd9, 0xf4, 0x87, 0xd1, 0x1a, 0xa0, 0x77, 0x73, 0xc9 };
            //byte[] publicExponent = new byte[] { 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            byte[] publicExponent = new byte[128];
            publicExponent[0] = 0x01;
            publicExponent[2] = 0x01;
            byte[] signature = Resources.GetBytes(Resources.BinaryResources.RsaSignature);

            string messageText = "Hello World!";
            byte[] messageBytes = Encoding.UTF8.GetBytes(messageText);

            //Verification of a signature
            Key_RSA rsa = new Key_RSA(modulus, publicExponent);
            bool valid = rsa.VerifySignature(messageBytes, 0, messageBytes.Length, signature, 0, signature.Length);
            if (valid)
                Debug.Print("Signature is valid.");
            else
                Debug.Print("Signature is NOT valid.");
        }
    }
}
