<%@ Page Language="C#" MasterPageFile="~/Themes/Blank.master" AutoEventWireup="true" CodeFile="Manage.aspx.cs" Inherits="Manage" %>
<%@ Import Namespace="AgileFrame.Orm.PersistenceLayer.Model" %>
<%@ Import Namespace="AgileFrame.Core"%>
<%@ Import Namespace="AgileFrame.Common"%>
<%@ Import Namespace="AgileFrame.Core.WebSystem"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    $(function () {
        manager.initBodyRows();
        manager.initFindDL();
        //manager.initPageSizeEvt("txtPageNum");
        bindWinResize(40);
        manager.initFindBtn("sel");
    });
    //ѡ��ҳ�濪ʼ
    var manager = new ListManager({ listCID: "tblist" });
    $(function () {
        manager.initBodyRows(); Sel_Load();
    });

    function selThis_B(tds) {
        var tdObj = $(tds[0]);
        var aObj = tdObj.find("a");
        if (aObj.length > 0) {
            _selERR = "����ѡ�����ӽڵ�����ݣ�";
            _selOK = false;
        }
    }
    function selOK_B(id) {
        //alert(id);
        $("input[id$='hidSelID']").val(id);
        //alert($("input[id$='hidSelID']").val());
        __doPostBack("ctl00$cphBody$LinkButton1", "");
    }
    //ѡ��ҳ�����
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<div class="main">
   <div class="titnav">
    <dl><dt><b><%=title %></b></dt></dl>
   </div>
   <div class="warn"><asp:Literal ID="litWarn" runat="server"></asp:Literal></div>    
   <div class="topfind hide"><!--��Ҫ��ȥʱ���� style="display:none;"-->

        <%=getFinder() %>

         <dl class="btn"><dd>
                <span><asp:Button ID="btnFind" runat="server" Text="��ѯ" onclick="btnFind_Click" /></span>
               <span><input id="btnClear" type="button" value="���" onclick="_topFindClear(this);"/></span>
               </dd></dl>
         <div class="clear"></div>
    </div>
    <div class="tool">
        <ul>
<%--            <li><span><input power="newWEC_A_VOTEDETAIL" id="btn_New" type="button" value="�½�" onclick="manager.newRecord('Edit.aspx','','1',900,700);" /></span></li>
            <li><span><input power="editWEC_A_VOTEDETAIL" id="btn_Edit" type="button" value="�༭" onclick="manager.editRecord('Edit.aspx','','1',900,700);" /></span></li>
            <li><span class="btn"><input power="newWEC_A_VOTEDETAIL" id="btn_NewSub" type="button" value="�½��¼�" onclick="manager.newRecord('STORAGEEdit.aspx','PID');" /></span></li>
            <li><span><input id="btn_Dels" type="button" value="ɾ��" onclick="manager.delBySelIDS('Back.aspx?modulename=<%=ModuleName%>');" /></span></li>--%>
            <li><span><input type="button" value="��ʾ��ѯ" id="display_search"/></span></li>
        </ul>
        <ol>
            <li><b>ÿҳ��ʾ</b></li>
            <li><input id="txtPageSize" type="text" runat="server" style="width:25px;" /><b>��</b></li>
            <li><span><asp:Button ID="Button4" runat="server" Text="����" OnClick="btnSetPageSize_Click" /></span></li>
            <li class="colist hide"><a onclick="_hideColsSel(this);">������</a><ol id="ol1"></ol></li>
        </ol>
    </div>
    <div class="tblist">
         <table cellpadding="0" cellspacing="0" id="tbList">
            <thead>
                <tr keyname="ID">
                    <td class="first"><input type="checkbox" onclick="_selAll(this);" /></td>
                   <%
                        foreach (DataBaseField f1 in fieldList)
                      { 
                         %>
                        <td data="{colname:'<%=f1.LongAttributeName%>',show:1}"><%=f1.ZhName%></td>
                    <%} %>
            </tr>
            </thead>
            <tbody>
                    <%
                        if (listObj.Count > 0)
                        {
                            foreach (ITableImplement val1 in listObj)
                            {
                                %>
                            <tr><td class="first"><input type="checkbox" value="<%=val1.GetValue(KeyID)%>" /></td>
                                <%
                                foreach (DataBaseField f1 in fieldList)
                                    { %>
                                    <td><%=GetHtmlList(f1, val1)%></td>
                                        <%//=val1.GetValue(attr1);//=val1.GetHtmlList(attr1); %>
                                    <%}%>
                            </tr>
                                <%
                             }
                         } %>
            </tbody>
         </table>
    </div>
    <div class="path-url hide">
        <strong>��ǰ·����</strong><asp:Literal ID="litPathLink" runat="server"></asp:Literal>
    </div>
    <div class="pages">
        <WebCtrl:AspNetPager ID="aspPager" runat="server" onpagechanged="aspPager_PageChanged"></WebCtrl:AspNetPager>
    </div>
</div>
<!--��ѡ-->
<input id="hidSelID" runat="server" type="hidden" />
<!--��ѡ-->
<input id="hidOld" type="hidden" runat="server" />
<input id="hidInitIDS" type="hidden" runat="server" />

<input id="hidVal" type="hidden" runat="server" />
<input id="hidFind" type="hidden" runat="server" />
<input id="hidCondition" type="hidden" runat="server" />
</asp:Content>