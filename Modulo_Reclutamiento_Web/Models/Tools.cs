using System.Security.Cryptography;
using System.Text;

namespace Modulo_Reclutamiento_Web.Models
{
    public class Tools
    {
        public static string Decode(string data)
        {
            var encoder = new UTF8Encoding();
            var decode = encoder.GetDecoder();
            var bytes = Convert.FromBase64String(data);
            //var count = decode.GetCharCount(bytes, 0, bytes.Length);
            //var decodechar = new char [count - 1];
            //for (int i = 0; i <= decodechar.Length; i++)
            //{
            //    decodechar[i] = '';
            //}
            //decode.GetChars(bytes, 0, bytes.Length, decodechar, 0);
            //var result = new string(decodechar);

            string returntext = System.Text.Encoding.UTF8.GetString(bytes);

            return returntext;
        }
        public static string Encode(string data)
        {
            try
            {
                //Convert.ToBase64String(YourByteArray);
                //Convert.FromBase64String(YourString);

                var encrypt = new byte[254];
                encrypt = Encoding.UTF8.GetBytes(data);

                var encodedata = Convert.ToBase64String(encrypt);
                return encodedata;
            }
            catch (Exception ex)
            {
                throw new Exception("error in exception", ex);
            }
        }

        public static string EncriptacionSHA1(string pwd)
        {
            using var sha1 = SHA1.Create();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            var sb = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        public static string ConvertToBase64(byte[] img)
        {

            using var ms = new MemoryStream(img, 0, img.Length);

            byte[] data = ms.ToArray();


            return "data:image / jpg; base64," + Convert.ToBase64String(data);
        }

        #region Comprobacion de nulos
        public static string IsNULL(DateTime value)
        {
            string date;
            if (value != null)
            {
                date = value.ToString("yyyy-MM-dd");
            }
            else
            {
                date = DateTime.Now.ToString("yyyy-MM-dd");
            }

            return date;
        }
        public static int IsNULL(int value)
        {
            int _value = 0;
            if (value != null)
            {
                _value = value;
            }

            return _value;
        }

        public static decimal IsNULL(decimal value)
        {
            decimal _value = 0;
            if (value != null)
            {
                _value = value;
            }

            return _value;
        }
      

        public static string IsNULL(string value)
        {
            string _value = "";
            if (value != null)
            {
                _value = value;
            }

            return _value;

        }
        #endregion

    }
}
