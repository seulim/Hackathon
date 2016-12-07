<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kakaotest.aspx.cs" Inherits="GMKT.GMobile.Web.kakaotest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<br/>
			cust_no : <asp:TextBox ID="TextBox1" runat="server" Text="114654149"></asp:TextBox>
			<br/>
			cust_no : <asp:TextBox ID="txtCustNo" runat="server" Text="114654149"></asp:TextBox>
			<br/>
			phone_number : <asp:TextBox ID="txtPhoneNumber" runat="server" Text="010-9799-2845"></asp:TextBox>
			<br/>
			temp_user_key : <asp:TextBox ID="txtTempUserKey" runat="server"></asp:TextBox>
			<br/>
			cert_code : <asp:TextBox ID="txtCertCode" runat="server"></asp:TextBox>
			chg_id : <asp:TextBox ID="txtChgId" runat="server" Text="test4op"></asp:TextBox>
			<br/>
			=======================================================================================================================
			<br/><br/>

    	<asp:Button ID="btnAddTemp" runat="server" onclick="btnAddTemp_Click" Text="임시 User key로 친구 등록 요청하기" />
			&nbsp;
			<asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="인증코드로 친구 등록 요청하기" />
			&nbsp;
			<asp:Button ID="btnCertSend" runat="server" onclick="btnCertSend_Click" Text="인증번호 발송요청" />
			<br />
			<br/>

			=======================================================================================================================
			result_code : <asp:TextBox ID="txtRetCode" runat="server"></asp:TextBox><br/>
			result_message : <asp:TextBox ID="txtMessage" runat="server"></asp:TextBox><br/>
			user_key : <asp:TextBox ID="txtUser_key" runat="server"></asp:TextBox><br/>
			result_text : <asp:TextBox ID="txtResponseText" runat="server"></asp:TextBox><br/>
    </div>
    </form>
</body>
</html>
