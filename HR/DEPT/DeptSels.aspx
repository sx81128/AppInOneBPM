﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="HRM_DeptSels" CodeFile="DeptSels.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" />
    <style type="text/css">
    .form{ text-align:left;margin:0;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="form">
    <div style="height:460px; overflow:auto;">
        <asp:TreeView ID="trDpt" runat="server" ShowCheckBoxes="All">
        </asp:TreeView>
    </div>
     </div>
    <div class="tool">
    <p><span><asp:Button ID="btnOK" runat="server" Text="确定选择" 
            onclick="btnOK_Click" /></span>
          <span><input id="btnCancle" type="button" value="取消" onclick="window.close();" /></span>  
            </p>
    </div>
   
    </form>
</body>
</html>
