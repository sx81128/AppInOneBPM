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
using AgileFrame.AppInOne.SYS;
using AgileFrame.Core.WebSystem;
using System.IO;
using System.Web.Script.Serialization;

public partial class WEC_REQUESTEdit : BaseAdminPage
{
    #region ģ�弯
    string Editor = @"<!--Switch-->
                    <!--Case InputString--><!--����-->
                    <input id='{�ֶοؼ���}' type='text' runat='server' ck='{type:{�ֶ�js����ö��},min:0,max:{�ֶα�ǩ����},need:{�ֶ��Ƿ����}}'/>
                    <!--Case SmallText-->
                    <textarea id='{�ֶοؼ���}' class='smalltextarea' cols='1' rows='1' style='height:20px;width:300px;overflow:hidden;' 
                            runat='server' ck='{need:{�ֶ��Ƿ����},len:{�ֶα�ǩ����},type:{�ֶ�js����ö��}}' />
                    <!--Case Text-->
                    <script type='text/javascript'>
                        $(function () {
                            var kindEditor = new creatKindEditor('#<%={�ֶοؼ���}.ClientID%>');
                        });
                    </script>
                    <input id='{�ֶοؼ���}' type='text'  runat='server' style='width:668px;height:230px;' ck='{need:{�ֶ��Ƿ����},len:{�ֶα�ǩ����},type:{�ֶ�js����ö��}}'/>
                    <!--Case HTML-->
                    <FCKV2:FCKeditor ID='{�ֶοؼ���}' runat='server' Height='250' Width='400' ToolbarSet='Basic' />
                    <!--Case SelectDrop--><!--Case SelectDrop-->
                    <select id='{�ֶοؼ���}' runat='server' ck='{need:{�ֶ��Ƿ����},len:{�ֶα�ǩ����},type:{�ֶ�js����ö��}}'>
                    </select>
                    <!--Case Select_RelationUser-->
                    <input id='{�ֶοؼ���}' type='hidden'  runat='server'/>
                    <input id='{�ֶοؼ���}_NAME' class='selshowinput' readonly='readonly' style='width:70%;float:left;' runat='server' type='text' />
                    <input class='sel' type='button' onclick='sel(this,""{����ĸ��д��������}"");' value='' style='float:left;' />                    
                    <!--Case SelectMultiple-->
                    <!--Case CheckBoxList-->
                    <input id='{�ֶοؼ���}' type='checkbox' class='ckb' runat='server' />
                    <!--Case RadioBoxList--> 
                    <input id='{�ֶοؼ���}' type='radio' class='ckb' runat='server' />
                    <!--Case InputDateTime-->
                    <input id='{�ֶοؼ���}' type='text' readonly='readonly' onclick='setday(this)' runat='server' ck='{need:{�ֶ��Ƿ����},len:{�ֶα�ǩ����},type:3}' />
                    <!--Case InputDate-->
                    <input id='{�ֶοؼ���}' type='text' readonly='readonly' onclick='setday(this)' runat='server' ck='{need:{�ֶ��Ƿ����},len:{�ֶα�ǩ����},type:3}'/>
                    <!--Case FileUpUpDoc-->
                    <uc1:UpFile ID='UpFile{����ĸ��д�ֶ���}' runat='server' />
                    <!--Case InputPassWord--> 
                    <input id='{�ֶοؼ���}' type='password' runat='server' ck='{need:{�ֶ��Ƿ����},len:{�ֶα�ǩ����},type:{�ֶ�js����ö��}}' />
                    <!--Case FileUpImage-->              
                    <script type='text/javascript'>
                        $(function () {
                            var smImage = new creatSmImage('#btn{�ֶοؼ���}', '#<%={�ֶοؼ���}.ClientID%>', '#<%=hid{�ֶοؼ���}.ClientID%>');
                        });
                    </script>
                    <div class='controls'>
					    <img id='{�ֶοؼ���}' runat='server' style='max-height:100px;vertical-align: middle;'>
                        <span class='insertimage'><a id='btn{�ֶοؼ���}'>ѡ��ͼ�ķ���</a></span>  �����С(��720��400)
                        <input type='hidden' id='hid{�ֶοؼ���}' runat='server' />
                    </div>
                <!--EndSwitch-->";

    #endregion ģ�弯
    WEC_REQUEST valObj =new WEC_REQUEST();
    int count = 0;
    public string keyid = "", kind="";
    protected string title = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        title = valObj._ZhName + "�༭";
        Page.Title = title;
        if (!string.IsNullOrEmpty(Request["TID"]))
        {
            keyid = Request["TID"];
        }

        if (!string.IsNullOrEmpty(Request["KeyID"]))
        {
            keyid = Request["KeyID"];
        }
        if (Request["kind"] != null)
        {
            kind = Request["kind"];
        }
        if (!IsPostBack)
        {
                                                   
            txtMATCH_TYPE.Items.AddRange(FormHelper.GetListItem(WEC_REQUEST.Attribute.MATCH_TYPE));                          
            txtKIND.Items.AddRange(FormHelper.GetListItem(WEC_REQUEST.Attribute.KIND));             
            txtSTATUS.Items.AddRange(FormHelper.GetListItem(WEC_REQUEST.Attribute.STATUS));                                 
            txtADDTIME.Value = (DateTime.Now).ToString("yyyy-MM-dd");

            this.txtTID.Disabled = true; this.txtTID.Value = "0";
            this.txtTID.Attributes["class"] = "dis";
            try
            {
                if (keyid != "")
                {
                    valObj = BLLTable<WEC_REQUEST>.GetRowData(WEC_REQUEST.Attribute.TID, keyid);

                    if (valObj == null)
                    {
                        return;
                    }

                    txtTID.Value = Convert.ToString(valObj.TID);//Convert.ToDecimal                

                    txtAID.Value = Convert.ToString(valObj.AID);//Convert.ToDecimal                

                    txtKEYWORD.Value = Convert.ToString(valObj.KEYWORD);//Convert.ToString                

                    txtMATCH_TYPE.Value = Convert.ToString(valObj.MATCH_TYPE);

                    txtMEMO.Value = Convert.ToString(valObj.MEMO);//Convert.ToString                

                    txtKIND.Value = valObj.KIND.ToString();

                    if (txtKIND.Value.Equals("5"))
                    {
                        txtPICURL.Src = valObj.MEMO;
                    }

                    txtSTATUS.Value = valObj.STATUS.ToString();

                    txtADDTIME.Value = (valObj.ADDTIME == DateTime.MinValue) ? "" : valObj.ADDTIME.ToString("yyyy-MM-dd");
                }
                else
                {
                    txtKIND.Value = kind;
                    txtAID.Value = Convert.ToString(userBase2.Curraid);

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
            WEC_REQUEST valObj = new WEC_REQUEST();

            if (txtTID.Value != "")
                valObj.TID = Convert.ToDecimal(txtTID.Value);

            //���ӵ����
            if (valObj.TID == 0)
            {
                WEC_REQUEST cond = new WEC_REQUEST();
                cond.KEYWORD = Convert.ToString(txtKEYWORD.Value);
                cond.AID = Convert.ToInt32(userBase2.Curraid);
                if (BLLTable<WEC_REQUEST>.Count(cond) > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "goto", "alert('���������ظ��ؼ���')", true);
                    return;
                }
            }

            if(txtAID.Value !="" )
                valObj.AID = Convert.ToDecimal(txtAID.Value);
            
            
            if(txtKEYWORD.Value !="" )
                valObj.KEYWORD = Convert.ToString(txtKEYWORD.Value);
            
            
            if(txtMATCH_TYPE.Value !="" )
                valObj.MATCH_TYPE = Convert.ToInt32(txtMATCH_TYPE.Value);
            
            
            if (txtKIND.Value.Equals("5"))
            {
                valObj.MEMO = hidPICURL.Value; ;

            }
            else
            {
                valObj.MEMO = Convert.ToString(txtMEMO.Value);
            }
            
            
            if(txtKIND.Value !="" )
                valObj.KIND = Convert.ToInt32(txtKIND.Value);
            
            
            if(txtSTATUS.Value !="" )
                valObj.STATUS = Convert.ToInt32(txtSTATUS.Value);
            
            
            if(txtADDTIME.Value !="" )
                valObj.ADDTIME = Convert.ToDateTime(txtADDTIME.Value);

            if (valObj.TID >0)
            {
                //valObj.TID = Convert.ToDecimal(keyid);
                count = BLLTable<WEC_REQUEST>.Update(valObj, WEC_REQUEST.Attribute.TID);
            }
            else
            {
                count = BLLTable<WEC_REQUEST>.Insert(valObj, WEC_REQUEST.Attribute.TID);
                keyid = valObj.TID.ToString();

            }

            //ͼƬ�ظ�
            if (kind.Equals("5"))
            {
                string path=valObj.MEMO;
                path = path.Substring(path.LastIndexOf('/')+1);
                string filepath=Request.PhysicalApplicationPath+path;
                filepath = filepath.Replace('\\', '/');
                string content = HttpUtil.uploadFile(filepath, HttpUtil.getAccessToken("wx69c300b3e390be5b", "3c06a3f6eb8a562b278583dff8b9da1c"), "image");
                JavaScriptSerializer a = new JavaScriptSerializer();
                Dictionary<string, object> o = (Dictionary<string, object>)a.DeserializeObject(content);
                string media_id = (string)o["media_id"];
                WEC_REQUEST_MEDIA media = new WEC_REQUEST_MEDIA();
                media.MEIDAID = media_id;
                media.TID = valObj.TID;
                media.ADDTIME = DateTime.Now;
                media.TYPE = 0;
                media.FILENAME = filepath;
                BLLTable<WEC_REQUEST_MEDIA>.Insert(media,WEC_REQUEST_MEDIA.Attribute.ID);
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
                        string dataStr = "[" + ViewState["sbData"].ToString() + "]";
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "goto", "if (window.opener){window.opener.returnValue = '" + dataStr + "';}else{window.returnValue = '" + dataStr + "';}window.close();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "goto", "if (window.opener){window.opener.returnValue = 're';}else{window.returnValue = 're';}window.close();", true);
                    }
                }
                else
                {
                    
                    
                    txtTID.Value ="";
                    
                    
                    txtAID.Value ="";
                    
                    
                    txtKEYWORD.Value ="";
                    
                    
                    txtMATCH_TYPE.Value ="";
                    
                    
                    txtMEMO.Value ="";
                    
                    
                    txtKIND.Value ="";
                    
                    
                    txtSTATUS.Value ="";
                    
                    
                    txtADDTIME.Value ="";
                }
            }
        }
        catch (Exception ex)
        {
            litWarn.Text = ex.Message;
        }
    }

}
