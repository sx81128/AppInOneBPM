﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using AgileFrame.Orm.PersistenceLayer.Model;
using AgileFrame.Orm.PersistenceLayer.BLL;
using AgileFrame.Orm.PersistenceLayer.BLL.Base;
using AgileFrame.AppInOne.Common;
using AgileFrame.AppInOne.HR;
using AgileFrame.AppInOne.OA;
using AgileFrame.AppInOne.SYS;
public partial class Admin_DEPT_DeptBack : AgileFrame.AppInOne.Common.BaseAdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //显示子节点
            if (Request["ShowSubNodes"] != null)
            {
                string nodetype = Request["ntype"];
                StringBuilder sb = new StringBuilder("[");
                string pid = Request["id"];//orgid/deptid/taskid
                if (nodetype == "org")
                {
                   
                    #region
                    HR_ORG obj = new HR_ORG();
                    HR_ORG val = new HR_ORG();
                    obj.P_ORG_ID = pid;

                    val.OrderBy(HR_ORG.Attribute.SORT_NO, Order.Asc);
                    List<HR_ORG> lst = BLLTable<HR_ORG>.Factory(conn).Select(val, obj);
                    if (lst != null && lst.Count > 0)
                    {
                        for (int i = 0; i < lst.Count; i++)
                        {
                            if (sb.Length > 2)
                            {
                                sb.Append(",");
                            }
                            decimal subcount = lst[i].SUB_NUM;// lst[i].SUB_NUM > 0 ? lst[i].SUB_NUM : 1;
                            sb.Append("{id:'").Append(lst[i].ORG_ID);
                            sb.Append("',pid:'").Append(lst[i].P_ORG_ID.ToString()).Append("',no:'").Append(lst[i].SORT_NO);
                            sb.Append("',sc:").Append(subcount.ToString());
                            sb.Append(",name:'").Append(lst[i].ORG_NAME).Append("',ntype:'org'}");
                        }
                    }
                    #endregion
                }
                if (nodetype == "org" || nodetype == "dept")
                {
                    #region
                    HR_DEPT obj = new HR_DEPT();
                    HR_DEPT val = new HR_DEPT();
                    if (nodetype == "org") {
                        obj.ORG_ID = pid;
                        obj.P_DEPT_ID = "0";
                    }
                    else {
                        obj.P_DEPT_ID = pid;
                    }
                    val.OrderBy(HR_DEPT.Attribute.SORT_NO, Order.Asc);

                    List<HR_DEPT> lst = BLLTable<HR_DEPT>.Factory(conn).Select(val, obj);
                    if (lst.Count > 0)
                    {
                        for (int i = 0; i < lst.Count; i++)
                        {
                            if (sb.Length > 2)
                            {
                                sb.Append(",");
                            }
                            int subcount = BLLTable<HR_DEPT>.Factory(conn).Select(HR_DEPT.Attribute.P_DEPT_ID, lst[i].DEPT_ID).Count;

                            sb.Append("{rid:'").Append(lst[i].P_DEPT_ID).Append("',id:'").Append(lst[i].DEPT_ID.ToString());
                            sb.Append("',pid:'").Append(lst[i].P_DEPT_ID.ToString()).Append("',no:'").Append(lst[i].SORT_NO);
                            sb.Append("',sc:").Append(subcount.ToString());
                            sb.Append(",name:'").Append(lst[i].DEPT_NAME).Append("',ntype:'dept'}");

                        }
                       
                    }
                    #endregion
                }
                sb.Append("]");
                Response.Write(sb.ToString());
            }
            //删除群组
            if (Request["deleteNode"] != null)
            {
                string keyid = Request["id"];
                int re = DeptHelper.DelDept(keyid);// BLLTable<HR_DEPT>.Factory(conn).Delete(HR_DEPT.Attribute.DEPT_ID, keyid);
                Response.Write("{re:" + re + ",msg:'删除成功'}");
            }
            //修改序号
            if (Request["UpdateSort"] != null)
            {
                string chgid1 = Request["chgid1"];
                string chgsort1 = Request["chgsort1"].ToString();
                string chgid2 = Request["chgid2"];
                string chgsort2 = Request["chgsort2"].ToString();
                try
                {
                   // int re=BllTable.UpdateCol(sys_ModuleInfo.Attribute.SortNum, chgsort1, sys_ModuleInfo.Attribute.ModuleID, chgid1, 1);
                    //re=BllTable.UpdateCol(sys_ModuleInfo.Attribute.SortNum, chgsort2, sys_ModuleInfo.Attribute.ModuleID, chgid2, 1);
                    int re = BLLTable<HR_DEPT>.Factory(conn).Update(HR_DEPT.Attribute.DEPT_ID, chgid1, HR_DEPT.Attribute.SORT_NO, chgsort1);
                    re = BLLTable<HR_DEPT>.Factory(conn).Update(HR_DEPT.Attribute.DEPT_ID, chgid2, HR_DEPT.Attribute.SORT_NO, chgsort2);

                    Response.Write(re.ToString());
                }
                catch {
                    Response.Write("0");
                }
               
            }

            UserBaseExd userExd = new UserBaseExd(userBase);

            //显示子节点
            if (Request["getSubNodes"] != null)
            {
                string mypaths = null;//
                if (SysSetting.TaskLog.SendTaskLimit)
                {
                    mypaths = "," + userExd.GetMyManageDeptIDS(true) + "," + userExd.GetMyDeptPathIDS() + ",";
                }
                if (Request["look"] != null)
                {
                    string user_data_ids = UserHelper.GetUserDataIDS(userBase, HR_DEPT.Attribute.DEPT_ID);
                    mypaths += user_data_ids + "," + userExd.GetMyDeptPathIDS() + ",";
                }
                if (Request["treepower"] != null)
                {
                    string user_data_ids = UserHelper.GetUserDataIDS(userBase, HR_DEPT.Attribute.DEPT_ID);
                    mypaths += user_data_ids + "," + userExd.GetMyDeptPathIDS() + ",";
                }
                if (Request["all"] != null)
                {
                    mypaths = null;
                }
                string nodetype = Request["ntype"];
                StringBuilder sb = new StringBuilder("[");
                string pid = Request["id"];//orgid/deptid/taskid
                if (nodetype == "root" || nodetype == "")
                {
                    #region
                    HR_ORG obj = new HR_ORG();
                    HR_ORG val = new HR_ORG();
                    obj.P_ORG_ID = pid;
                    val.OrderBy(HR_ORG.Attribute.SORT_NO, Order.Asc);
                    List<HR_ORG> lst = BLLTable<HR_ORG>.Factory(conn).Select(val, obj);
                    if (lst.Count != null)
                    {
                        for (int i = 0; i < lst.Count; i++)
                        {
                            if (sb.Length > 2)
                            {
                                sb.Append(",");
                            }
                            decimal subcount = lst[i].SUB_NUM > 0 ? lst[i].SUB_NUM : 1;
                            sb.Append("{id:'").Append(lst[i].ORG_ID);
                            sb.Append("',pid:'").Append(lst[i].P_ORG_ID.ToString()).Append("',no:").Append(lst[i].SORT_NO);
                            sb.Append(",sc:").Append(subcount.ToString());
                            sb.Append(",name:'").Append(lst[i].ORG_NAME).Append("',ntype:'org'}");
                        }
                    }
                    #endregion
                }
                if (nodetype == "org" || nodetype == "dept")
                {
                    #region
                    HR_DEPT objDept = new HR_DEPT();
                    HR_DEPT valDept = new HR_DEPT();
                    if (nodetype == "org")
                    {
                        objDept.ORG_ID = pid;
                        objDept.P_DEPT_ID = "0";
                    }
                    else
                    {
                        objDept.P_DEPT_ID = pid;
                    }
                    //if (Request["my"] != null)
                    //{

                    //    mydepts += mydepts == "" ? userBase.DeptID : "," + userBase.DeptID;
                    //    objDept.In(HR_DEPT.Attribute.DEPT_ID, mydepts);
                    //}
                    valDept.OrderBy(HR_DEPT.Attribute.SORT_NO, Order.Asc);
                    List<HR_DEPT> lstDept = BLLTable<HR_DEPT>.Factory(conn).Select(valDept, objDept);
                    if (lstDept.Count != null)
                    {
                        for (int i = 0; i < lstDept.Count; i++)
                        {
                            if (mypaths == null || mypaths.IndexOf("," + lstDept[i].DEPT_ID + ",") != -1)
                            {
                                if (sb.Length > 2)
                                {
                                    sb.Append(",");
                                }
                                decimal subcount = lstDept[i].SUB_NUM > 0 ? lstDept[i].SUB_NUM : 1;
                                sb.Append("{id:'").Append(lstDept[i].DEPT_ID);
                                sb.Append("',pid:'").Append(lstDept[i].P_DEPT_ID.ToString()).Append("',no:").Append(lstDept[i].SORT_NO);
                                sb.Append(",sc:").Append(subcount.ToString());
                                sb.Append(",name:'").Append(lstDept[i].DEPT_NAME).Append("',ntype:'dept'}");
                            }
                        }
                    }
                    #endregion
                }
                sb.Append("]");
                Response.Write(sb.ToString());
            }
            Response.End();
        }
    }
}
