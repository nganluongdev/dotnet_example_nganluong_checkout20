using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Xml;


/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WebService : System.Web.Services.WebService {

    public WebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    private string merchant_site_code = "24338"; // mã merchant  
    private String secure_pass = "12345612";//Mật khẩu merchant 
    public String GetMD5Hash(String input)
    {

        System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();

        byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);

        bs = x.ComputeHash(bs);

        System.Text.StringBuilder s = new System.Text.StringBuilder();

        foreach (byte b in bs)
        {
            s.Append(b.ToString("x2").ToLower());
        }
        String md5String = s.ToString();
        return md5String;
    }

    [WebMethod]
    public Boolean UpdateOrder(String transaction_info, String order_code, String payment_id, String payment_type, String secure_code)
    {
        String path = Server.MapPath("Contend");
        String secure_code_ws = this.GetMD5Hash(transaction_info + " " + order_code + " " + payment_id + " " + payment_type + " " + this.secure_pass);
        String nd = transaction_info + " " + order_code + " " + payment_id + " " + payment_type + " " + this.secure_pass;
        System.IO.StreamWriter str = new System.IO.StreamWriter(path + "/nganluong.txt");
        str.Write(nd);
        str.Flush();
        str.Close();

        if (secure_code == secure_code_ws)
        {
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = "data source=(local);Database=Online.Topcare;uid=tc;pwd=quynhnet0153;";
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE Gio_hang_ngan_luong set payment_type="+payment_type+", payment_id='"+payment_id+"' where transaction_info="+transaction_info;
                cmd.Connection = cnn;
                int result = cmd.ExecuteNonQuery();


                NL_Checkout nlcheckout = new NL_Checkout();
                nlcheckout.merchant_site_code = this.merchant_site_code;
                nlcheckout.secure_pass = this.secure_pass;

                string resultcheckorder = nlcheckout.Nl_checkoder(1, order_code, payment_id);


                XmlDocument dom = new XmlDocument();
                dom.LoadXml(resultcheckorder);
                XmlNodeList root = dom.DocumentElement.ChildNodes;
                if (root.Item(1).InnerText == "00")
                {
                    // cập nhật đơn hàng với oder_code 
                    //Phương thức thanh toán khách đã lựa chọn : == root.Item(2).ChildNodes.Item(14).InnerText;
                }


                

                if (result != 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex) {
                return false;
            }
        }
        else
            return false;
    }
    [WebMethod]
    //RefundOrder($transaction_info, $order_code, $payment_id, $refund_payment_id, $refund_amount, $refund_type, $refund_description, $secure_code)
    public Boolean RefundOrder(String transaction_info, 
                                String order_code, 
                                String payment_id,
                                String refund_payment_id, 
                                String refund_amount,
                                String refund_type,
                                String refund_description,
                                String secure_code)
    {
        String path = Server.MapPath("Contend");

        String md5 = transaction_info+" "+ order_code+ " "+ payment_id + " "+ refund_payment_id+ " "+ refund_amount + " "+ refund_type + " " + refund_description + " "+ this.secure_pass;
        String secure_code_ws = this.GetMD5Hash(md5);

        String nd = transaction_info + " " + order_code + " " + payment_id + " " + refund_type + " " + this.secure_pass;
        System.IO.StreamWriter str = new System.IO.StreamWriter(path + "/nganluong_log_refun.txt");
        str.Write(nd);
        str.Flush();
        str.Close();
        
        
        if (secure_code == secure_code_ws)
        {
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = "data source=(local);Database=Online.Topcare;uid=tc;pwd=quynhnet0153;";
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE Gio_hang_ngan_luong set payment_type=3, payment_id='"+payment_id+"' where transaction_info="+transaction_info;
                cmd.Connection = cnn;
                int result = cmd.ExecuteNonQuery();

                if (result != 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        else
            return false;
    }
}