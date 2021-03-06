using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AgileFrame.Core;
using AgileFrame.Orm.PersistenceLayer.Model;
using AgileFrame.Orm.PersistenceLayer.BLL;
using AgileFrame.Orm.PersistenceLayer.BLL.Base;

public partial class HR_CHECKWORKDEVICEShow2 : AgileFrame.AppInOne.Common.BasePage
{
    HR_CHECKWORKDEVICE valObj=new HR_CHECKWORKDEVICE();
    protected string title = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        title = "设备详细";
        Page.Title = title;
        if (!IsPostBack)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request["DeviceID"]))
                {

                    valObj = BLLTable<HR_CHECKWORKDEVICE>.Factory(conn).GetRowData(HR_CHECKWORKDEVICE.Attribute.DeviceID, Request["DeviceID"]);
                    if(valObj==null) return ;
                    
                    
                    txtDeviceID.Text = Convert.ToString(valObj.DeviceID);//Convert.ToString
                    
                    
                    txtORG_ID.Text = Convert.ToString(valObj.ORG_ID);//Convert.ToString
                    
                    
                    txtUSE_FLAG.Text=valObj.USE_FLAG.ToString();

                }
            }
            catch (Exception ex)
            {
                litWarn.Text = ex.Message;
            }

            if (Request["ajax"] != null)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "utf-8";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");//设置输出流为简体中文
                //Response.ContentType = "html/text";

                this.EnableViewState = false;
                System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
                System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
                System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
                divC.RenderControl(oHtmlTextWriter);

                Response.Write(oStringWriter.ToString());
                Response.End();
            }
        }
    }

}
