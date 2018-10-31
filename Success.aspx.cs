using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Success : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            String return_url = Request["return_url"];
            String receiver = Request["receiver"];
            String transaction_info = Request["transaction_info"];
            String order_code = Request["order_code"];
            String price = Request["price"];
            String payment_id = Request["payment_id"];
            String payment_type = Request["payment_type"];
            String error_text = Request["error_text"];
            String secure_code = Request["secure_code"];
            NL_Checkout checkOut = new NL_Checkout();
            bool rs = checkOut.verifyPaymentUrl(transaction_info, order_code, price, payment_id, payment_type, error_text, secure_code);
            if (rs)
            {
                Response.Write("Thanh toán thành công !");
            }
            else
            {
                Response.Write("Thanh toán không thành công");
            }
        }
        catch(Exception ex) {
            Response.Write(ex.ToString());
        }
    }
}