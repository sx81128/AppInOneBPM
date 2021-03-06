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
using AgileFrame.AppInOne.Common;

public partial class WEC_ACTIVITY_SNCODEEdit : BaseAdminPage
{
    WEC_ACTIVITY_SNCODE valObj =new WEC_ACTIVITY_SNCODE();
    int count = 0;
    string keyid = "";
    protected string title = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        title = valObj._ZhName + "�༭";
        Page.Title = title;
        if (!string.IsNullOrEmpty(Request["ID"]))
        {
            keyid = Request["ID"];
        }

        if (!string.IsNullOrEmpty(Request["KeyID"]))
        {
            keyid = Request["KeyID"];
        }
        if (!IsPostBack)
        {
                                                   
            txtSTATUS.Items.AddRange(FormHelper.GetListItem(WEC_ACTIVITY_SNCODE.Attribute.STATUS));                                                           
            txtZJ_TIME.Value = (DateTime.Now).ToString("yyyy-MM-dd");                                 
            txtSY_TIME.Value = (DateTime.Now).ToString("yyyy-MM-dd");

            this.txtID.Disabled = true; this.txtID.Value = "0";
            this.txtID.Attributes["class"] = "dis";
            try
            {
                if (keyid != "")
                {
                    valObj = BLLTable<WEC_ACTIVITY_SNCODE>.GetRowData(WEC_ACTIVITY_SNCODE.Attribute.ID, keyid);
                    if(valObj==null) return ;
                    
                    
                    txtID.Value = Convert.ToString(valObj.ID);//Convert.ToDecimal                
                    
                    txtSN_CODE.Value = Convert.ToString(valObj.SN_CODE);//Convert.ToString                
                    
                    txtAWARD_TYPE.Value = Convert.ToString(valObj.AWARD_TYPE);//Convert.ToString                
                    
                    txtSTATUS.Value=valObj.STATUS.ToString();                
                    
                    txtPHONE.Value = Convert.ToString(valObj.PHONE);//Convert.ToString                
                    
                    txtWX_CODE.Value = Convert.ToString(valObj.WX_CODE);//Convert.ToString                
                    
                    txtZJ_TIME.Value = (valObj.ZJ_TIME == DateTime.MinValue) ? "" : valObj.ZJ_TIME.ToString("yyyy-MM-dd");                
                    
                    txtSY_TIME.Value = (valObj.SY_TIME == DateTime.MinValue) ? "" : valObj.SY_TIME.ToString("yyyy-MM-dd");                
                    
                    txtA_ID.Value = Convert.ToString(valObj.A_ID);//Convert.ToDecimal
                }
            }
            catch (Exception ex)
            {
                litWarn.Text = ex.Message;
            }
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            WEC_ACTIVITY_SNCODE valObj = new WEC_ACTIVITY_SNCODE();
            
            
            if(txtID.Value !="" )
                valObj.ID = Convert.ToDecimal(txtID.Value);
            
            
            if(txtSN_CODE.Value !="" )
                valObj.SN_CODE = Convert.ToString(txtSN_CODE.Value);
            
            
            if(txtAWARD_TYPE.Value !="" )
                valObj.AWARD_NAME = Convert.ToString(txtAWARD_TYPE.Value);
            
            
            if(txtSTATUS.Value !="" )
                valObj.STATUS = Convert.ToInt32(txtSTATUS.Value);
            
            
            if(txtPHONE.Value !="" )
                valObj.PHONE = Convert.ToString(txtPHONE.Value);
            
            
            if(txtWX_CODE.Value !="" )
                valObj.WX_CODE = Convert.ToString(txtWX_CODE.Value);
            
            
            if(txtZJ_TIME.Value !="" )
                valObj.ZJ_TIME = Convert.ToDateTime(txtZJ_TIME.Value);
            
            
            if(txtSY_TIME.Value !="" )
                valObj.SY_TIME = Convert.ToDateTime(txtSY_TIME.Value);
            
            
            if(txtA_ID.Value !="" )
                valObj.A_ID = Convert.ToDecimal(txtA_ID.Value);

            if (keyid != "")
            {
                valObj.ID = Convert.ToDecimal(keyid);
                count = BLLTable<WEC_ACTIVITY_SNCODE>.Update(valObj, WEC_ACTIVITY_SNCODE.Attribute.ID);
            }
            else
            {
                count = BLLTable<WEC_ACTIVITY_SNCODE>.Insert(valObj, WEC_ACTIVITY_SNCODE.Attribute.ID);
                keyid = valObj.ID.ToString();

            }
            if (count > 0)
            {
                StringBuilder sbData = new StringBuilder("{valObj:''");
                List<AttributeItem> lstCol = valObj.af_AttributeItemList;
                for (int i = 0; i < lstCol.Count; i++)
                {
                    object val = valObj.GetValue(lstCol[i]);
                    if (val != null)
                    {
                        sbData.Append(",").Append(lstCol[i].FieldName).Append(":'").Append(val.ToString()).Append("'");
                    }
                }
                sbData.Append("}");
                if (ViewState["sbData"] == null)
                {
                    ViewState["sbData"] = sbData.ToString();
                }
                else {
                    ViewState["sbData"] += ","+sbData.ToString();
                }
                Button btn = (Button)sender;
                if (btn.ID.IndexOf("btnOK")!=-1)
                {
                    if (ViewState["sbData"] == null)
                    {
                        string dataStr = "[" + ViewState["sbData"] .ToString()+ "]";
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "goto", "window.returnValue=\"" + dataStr + "\";window.close();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "goto", "window.returnValue=\"re\";window.close();", true);
                    }
                }
                else
                {
                    
                    
                    txtID.Value ="";
                    
                    
                    txtSN_CODE.Value ="";
                    
                    
                    txtAWARD_TYPE.Value ="";
                    
                    
                    txtSTATUS.Value ="";
                    
                    
                    txtPHONE.Value ="";
                    
                    
                    txtWX_CODE.Value ="";
                    
                    
                    txtZJ_TIME.Value ="";
                    
                    
                    txtSY_TIME.Value ="";
                    
                    
                    txtA_ID.Value ="";
                }
            }
        }
        catch (Exception ex)
        {
            litWarn.Text = ex.Message;
        }
    }

}
