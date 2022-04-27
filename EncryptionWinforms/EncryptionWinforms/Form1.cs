using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;

namespace EncryptionWinforms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Random rnd = new Random();
            byte[] iv = new byte[16];
            rnd.NextBytes(iv);

            string password = "N1ceKiyweryHic3E";

            byte[] salt = new byte[16];
            rnd.NextBytes(salt);

            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);

            byte[] inputBytes = GetBytesFromImage("tiger.jpg");

            byte[] keyBytes = pbkdf2.GetBytes(32);

            byte[] encrypted = Encrypt(inputBytes, keyBytes, iv);

            byte[] decrypted = Decrypt(encrypted, keyBytes, iv);

            pictureBox1.Image = GetImageFromBytes(inputBytes);

            pictureBox2.Image = GetImageFromBytes(decrypted);
        }

        static Image GetImageFromBytes(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        static byte[] GetBytesFromImage(string pathToImage)
        {
            Image img = Image.FromFile(pathToImage);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        static byte[] Encrypt(byte[] inputBytes, byte[] keyBytes, byte[] iv)
        {
            IBufferedCipher cipher = CipherUtilities.GetCipher("AES/CTR/NoPadding");

            cipher.Init(true, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES", keyBytes), iv));

            byte[] encryptedBytes = cipher.DoFinal(inputBytes);

            return encryptedBytes;
        }

        static byte[] Decrypt(byte[] dataEncrypted, byte[] keyBytes, byte[] iv)
        {
            var cipher = CipherUtilities.GetCipher("AES/CTR/NoPadding");

            cipher.Init(true, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES", keyBytes), iv));

            var memoryStream = new MemoryStream(dataEncrypted, false);
            var cipherStream = new CipherStream(memoryStream, cipher, null);

            int bufferSize = 1024;
            var buffer = new byte[bufferSize];
            int length = 0;
            var resultStream = new MemoryStream();

            while ((length = cipherStream.Read(buffer, 0, bufferSize)) > 0)
            {
                resultStream.Write(buffer, 0, length);
            }

            return resultStream.ToArray();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {


        }
    }
}
