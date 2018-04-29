﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 企业信息管理 {
    public partial class supply : System.Web.UI.Page
    {

        string connectionStr = ConfigurationManager.ConnectionStrings["access"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("login.aspx");
                return;
            }
            username.Text = Session["nickname"] as string;
            comment.Text = Session["comment"] as string;
            showPurchaseList();
        }
        protected void purchase_list_RowDeleting(object sender, EventArgs e)
        {

        }

        protected void showPurchaseList()
        {
           if (!IsPostBack)
            {
                using (OleDbConnection conn = new OleDbConnection(connectionStr)){
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter("select * from purchase ", conn)){
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "purchase");
                        purchase_list.DataSource = ds;
                        purchase_list.DataBind();
                    }
                }
            }
        }


        /// <summary>
        /// 根据pur_id删除一行采购数据
        /// </summary>
        /// <param name="supId"></param>
        protected void Delete(int purId)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionStr))
            {
                using (OleDbCommand cmd = new OleDbCommand("delete from purchase where pur_id=" + purId, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// “删除”按钮事件处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void purchase_list_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            foreach (DictionaryEntry entry in e.Keys)
            {
                Delete((int)entry.Value);
                purchase_list.Rows[e.RowIndex].Visible = false;
            }
        }
    }
}