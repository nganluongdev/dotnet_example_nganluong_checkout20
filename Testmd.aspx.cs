using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;
using System.Security.Cryptography;
public partial class Testmd : System.Web.UI.Page
{
    private string merchant_site_code = "24338";
    private string merchant_pass = "12345612";
    private string receiver = "hoannet@gmail.com";
      
	  
  
  
    protected void btthanhtoan_Click(object sender, EventArgs e)
    {
        NL_Checkout nlcheckout = new NL_Checkout();
        nlcheckout.merchant_site_code = this.merchant_site_code;
        nlcheckout.secure_pass = this.merchant_pass;

        string rs = nlcheckout.buildCheckoutUrlNew("http://hoannet.vn/success.aspx", this.receiver, "", txtOrderID.Text, txtPriceOrder.Text, "vnd", 1, 0, 0, 0, 0, "", "Truong Xuan Hoan*|*hoantx@nganluong.vn*|*0986588099*|*18 Tam Trinh", "");
        Response.Write(rs);
        Response.Redirect(rs);

    }
    protected void btcheckoder_Click(object sender, EventArgs e)
    {
        NL_Checkout nlcheckout = new NL_Checkout();
        nlcheckout.merchant_site_code = this.merchant_site_code;
        nlcheckout.secure_pass = this.merchant_pass;
        string resultcheckorder = nlcheckout.Nl_checkoder(1, "1849", "1117603");
        

        XmlDocument dom = new XmlDocument();
        dom.LoadXml(resultcheckorder);
        XmlNodeList root = dom.DocumentElement.ChildNodes;
        if (root.Item(1).InnerText =="00")
        {
            string result = root.Item(0).InnerText + "|" + root.Item(1).InnerText + "|"
                        + root.Item(2).ChildNodes.Item(0).InnerText
                        + "|"
                        + root.Item(2).ChildNodes.Item(14).InnerText;

            texbox_result_checkoder.Text = result;
        }
        

        txtserverkt.Text = resultcheckorder;


               
    }

    public string Encrypt(string toEncrypt, string secretKey)//, bool useHashing
    {
        byte[] keyArray;
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

        //System.Configuration.AppSettingsReader settingsReader =
        //                                    new System.Configuration.AppSettingsReader();
        // Get the key from config file

        //string key = (string)settingsReader.GetValue("SecurityKey",
        //                                                 typeof(String));
        //System.Windows.Forms.MessageBox.Show(key);
        //If hashing use get hashcode regards to your key
        //if (useHashing)
        //{
        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(secretKey));
        //Always release the resources and flush data
        // of the Cryptographic service provide. Best Practice

        hashmd5.Clear();
        //}
        //else
        //keyArray = UTF8Encoding.UTF8.GetBytes(secretKey);

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //set the secret key for the tripleDES algorithm           
        tdes.Key = keyArray;

        //mode of operation. there are other 4 modes.
        //We choose ECB(Electronic code Book)
        tdes.Mode = CipherMode.ECB;
        //padding mode(if any extra byte added)

        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateEncryptor();
        //transform the specified region of bytes array to resultArray
        byte[] resultArray =
          cTransform.TransformFinalBlock(toEncryptArray, 0,
          toEncryptArray.Length);
        //Release resources held by TripleDes Encryptor
        tdes.Clear();
        //Return the encrypted data into unreadable string format
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    public string Decrypt(string cipherString, string secretKey)//, bool useHashing
    {
        byte[] keyArray;
        //get the byte code of the string

        byte[] toEncryptArray = Convert.FromBase64String(cipherString);

        //System.Configuration.AppSettingsReader settingsReader =
        //                                    new System.Configuration.AppSettingsReader();
        ////Get your key from config file to open the lock!
        //string key = (string)settingsReader.GetValue("SecurityKey",
        //                                             typeof(String));

        //if (useHashing)
        //{
        //    //if hashing was used get the hash code with regards to your key
        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(secretKey));
        //release any resource held by the MD5CryptoServiceProvider
        hashmd5.Clear();
        //}
        //else            
        //if hashing was not implemented get the byte code of the key
        //keyArray = UTF8Encoding.UTF8.GetBytes(secretKey);


        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //set the secret key for the tripleDES algorithm
        tdes.Key = keyArray;
        //mode of operation. there are other 4 modes. 
        //We choose ECB(Electronic code Book)

        tdes.Mode = CipherMode.ECB;
        //padding mode(if any extra byte added)
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(
                             toEncryptArray, 0, toEncryptArray.Length);
        //Release resources held by TripleDes Encryptor                
        tdes.Clear();
        //return the Clear decrypted TEXT
        return UTF8Encoding.UTF8.GetString(resultArray);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string ed;
        ed = this.Encrypt("1133123VIETTEL100001100338531hoannet123NganLuong3VIETPAY4Topup", "NganLuong3VIETPAY4Topup");
        Response.Write(ed);
    }
}