using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GMKT.GMobile.Biz;

namespace GMKT.GMobile.Web
{
	public partial class kakaotest : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            KakaoTalkServiceBiz biz = new KakaoTalkServiceBiz();
            CommonResultT result = new CommonResultT();

            result = biz.SendCertCode("821085071636");

            if (result.result_code.Equals("1000"))
            {
                Response.Write("<script>");
                Response.Write("alert('인증번호가 발송되었습니다.');");
                Response.Write("location.href=''");
                Response.Write("</script>");
            }



		}

		protected void btnAddTemp_Click(object sender, EventArgs e)
		{
			KakaoTalkServiceBiz biz = new KakaoTalkServiceBiz();
			CommonResultT result = new CommonResultT();

			result = biz.AddUserTemp(this.txtCustNo.Text.Trim(), this.txtPhoneNumber.Text.Trim(), this.txtTempUserKey.Text.Trim(), this.txtChgId.Text.Trim(), "K");
			
			this.txtRetCode.Text = result.result_code;
			this.txtMessage.Text = result.result_message;
			this.txtUser_key.Text = result.user_key;
			this.txtResponseText.Text = result.result_text;
		}

		protected void btnAdd_Click(object sender, EventArgs e)
		{
			KakaoTalkServiceBiz biz = new KakaoTalkServiceBiz();
			CommonResultT result = new CommonResultT();

			result = biz.AddUser(this.txtCustNo.Text.Trim(), this.txtPhoneNumber.Text.Trim(), this.txtCertCode.Text.Trim(), this.txtChgId.Text.Trim(), "M");

			this.txtRetCode.Text = result.result_code;
			this.txtMessage.Text = result.result_message;
			this.txtUser_key.Text = result.user_key;
			this.txtResponseText.Text = result.result_text;
		}

		protected void btnCertSend_Click(object sender, EventArgs e)
		{
			KakaoTalkServiceBiz biz = new KakaoTalkServiceBiz();
			CommonResultT result = new CommonResultT();

			result = biz.SendCertCode(this.txtPhoneNumber.Text.Trim());

			this.txtRetCode.Text = result.result_code;
			this.txtMessage.Text = result.result_message;
			this.txtUser_key.Text = result.user_key;
			this.txtResponseText.Text = result.result_text;
		}
	}
}