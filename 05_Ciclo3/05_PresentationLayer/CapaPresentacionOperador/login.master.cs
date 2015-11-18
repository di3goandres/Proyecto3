using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Uniandes.Encriptador;

public partial class login : System.Web.UI.MasterPage
{
    private static string _privateKey;
    private static string _publicKey;
    private static UnicodeEncoding _encoder = new UnicodeEncoding();

    protected void Page_Load(object sender, EventArgs e)
    {
       

        var rsa = new RSACryptoServiceProvider();
        _privateKey = rsa.ToXmlString(true);
        _publicKey = rsa.ToXmlString(false);

        EncriptadorTripleDES des = new EncriptadorTripleDES();

        //string text = "1-/Ola/DIego/Dub/aca";

        //var resultadoEncryp = des.Encrypt(text, true);
        //var resultadoDEncryp = des.Decrypt(resultadoEncryp, true);






        //var md5 = MD5Hash(text);
        //var reverMd5 = retornoNormal(md5);

        //var enc = Encrypt(text);
        //Console.WriteLine("RSA // Encrypted Text: " + enc);
        //var dec = Decrypt(enc);
        //Console.WriteLine("RSA // Decrypted Text: " + dec);
    }

    ////public static string Decrypt(string data)
    ////{
    ////    var rsa = new RSACryptoServiceProvider();
    ////    var dataArray = data.Split(new char[] { ',' });
    ////    byte[] dataByte = new byte[dataArray.Length];
    ////    for (int i = 0; i < dataArray.Length; i++)
    ////    {
    ////        dataByte[i] = Convert.ToByte(dataArray[i]);
    ////    }

    ////    rsa.FromXmlString(_privateKey);
    ////    var decryptedByte = rsa.Decrypt(dataByte, false);
    ////    return _encoder.GetString(decryptedByte);
    ////}

    ////public static string Encrypt(string data)
    ////{
    ////    var rsa = new RSACryptoServiceProvider();
    ////    rsa.FromXmlString(_publicKey);
    ////    var dataToEncrypt = _encoder.GetBytes(data);
    ////    var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
    ////    var length = encryptedByteArray.Count();
    ////    var item = 0;
    ////    var sb = new StringBuilder();
    ////    foreach (var x in encryptedByteArray)
    ////    {
    ////        item++;
    ////        sb.Append(x);

    ////        if (item < length)
    ////            sb.Append(",");
    ////    }

    ////    return sb.ToString();
    ////}





    ////public string MD5Hash(string text)
    ////{
    ////    MD5 md5 = new MD5CryptoServiceProvider();

    ////    //compute hash from the bytes of text
    ////    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

    ////    //get hash result after compute it
    ////    byte[] result = md5.Hash;

    ////    StringBuilder strBuilder = new StringBuilder();
    ////    for (int i = 0; i < result.Length; i++)
    ////    {
    ////        //change it into 2 hexadecimal digits
    ////        //for each byte
    ////        strBuilder.Append(result[i].ToString("x2"));
    ////    }

    ////    return strBuilder.ToString();
    ////}




    //////public static string ToHex(this byte[] bytes, bool upperCase)
    //////{
    //////    StringBuilder result = new StringBuilder(bytes.Length * 2);

    //////    for (int i = 0; i < bytes.Length; i++)
    //////        result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

    //////    return result.ToString();
    //////}

    ////static byte[] GetBytes(string str)
    ////{
    ////    byte[] bytes = new byte[str.Length * sizeof(char)];
    ////    System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
    ////    return bytes;
    ////}

    ////static string GetString(byte[] bytes)
    ////{
    ////    char[] chars = new char[bytes.Length / sizeof(char)];
    ////    System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
    ////    return new string(chars);
    ////}

    ////public string retornoNormal(string texto) {

    ////    byte[] bite = GetBytes(texto);
    ////    var retorno = GetString(bite);
    ////    return retorno;
    
    ////}


}
