using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    private string merchant_site_code = "36680";
    private string merchant_pass = "matkhauketnoi";
    private string receiver = "demo@nganluong.vn";
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void bt_naptien_Click(object sender, EventArgs e)
    {
        NL_Checkout nlcheckout = new NL_Checkout();
        nlcheckout.merchant_site_code = this.merchant_site_code;
        nlcheckout.secure_pass = this.merchant_pass;
        
        var rnd = new Random(DateTime.Now.Millisecond);
        int oderID = rnd.Next(0, 3000);
        DateTime dtNow = DateTime.Now;
        string strOrderID = dtNow.Year.ToString() + dtNow.Month.ToString() + dtNow.Day.ToString() + dtNow.Hour.ToString() + dtNow.Minute.ToString() + dtNow.Second.ToString();

        string rs = nlcheckout.buildCheckoutUrlNew("http://localhost/success.aspx", this.receiver, "", strOrderID, txtPriceOrder.Text, "vnd", 1, 0, 0, 0, 0, "", "", "");
        Response.Write(rs);
        Response.Redirect(rs);

    }
}