<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Testmd.aspx.cs" Inherits="Testmd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Mã đơn hàng:<asp:TextBox 
            ID="txtOrderID" runat="server"></asp:TextBox><br />
        Số tiền: <asp:TextBox ID="txtPriceOrder" runat="server"></asp:TextBox><br />
    
        <asp:Button ID="id_thanhtoan" runat="server" onclick="btthanhtoan_Click" 
            Text="Thanh toán" />
            <br />
    
    </div>
    <asp:Button ID="di_checlorder" runat="server" onclick="btcheckoder_Click" 
        Text="check order" />
    <asp:TextBox ID="texbox_result_checkoder" runat="server" Height="166px" 
        TextMode="MultiLine" Width="333px"></asp:TextBox>

         <asp:TextBox ID="txtserverkt" runat="server" Height="166px" 
        TextMode="MultiLine" Width="333px"></asp:TextBox>


    </form>
</body>
</html>
