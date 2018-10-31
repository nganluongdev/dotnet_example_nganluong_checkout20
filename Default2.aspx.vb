

Partial Class Default2
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim return_url As String = "http://"
        Dim receiver As String = "http://"
        Dim transaction_info1 As String = "http://"
        Dim order_code As String = "http://"
        Dim price As String = "http://"
        Dim transaction_info As String = "http://"
        Dim currency_code As String = "http://"
        Dim quantity As String = "http://"
        Dim tax As String = "http://"
        Dim discount As String = "http://"
        Dim fee_cal As String = "http://"
        Dim fee_shipping As String = "http://"
        Dim order_description As String = "http://"
        Dim buyer_info As String = "http://"
        Dim affiliate_code As String = "http://"


        'Dim nl As  
        'Dim urlnganluong As String = nl.buildCheckoutUrl(return_url, receiver, transaction_info, order_code, price, currency_code, quantity, tax, discount, fee_cal, fee_shipping, order_description, buyer_info, affiliate_code)
        'Response.Redirect(urlnganluong)
    End Sub
End Class
