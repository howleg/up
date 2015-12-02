using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_privleges_AddCourse : System.Web.UI.Page
{
    /**********************************************************************
   * REPLACE THIS STRING WITH CONNECTIONSTRING FROM YOUR LOCAL DATABASE  *
   **********************************************************************/
   // string johnDB = "Data Source=USER-VAIOSQLEXPRESS;Initial Catalog=courseHunter540NEW;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    private static String johnDB = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
            String courseNum = TextBox4.Text;
            String courseTi = TextBox5.Text;
            String creditsString = TextBox6.Text;
            int creditInt = Convert.ToInt32(creditsString);


        if (CheckBox1.Checked)
        {
            using (SqlConnection insertElecCon = new SqlConnection(johnDB))
            {

                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.CommandText = "insertElective";
                cmdInsert.CommandType = CommandType.StoredProcedure;


                cmdInsert.Parameters.AddWithValue("@coursNumber", courseNum);
                cmdInsert.Parameters.AddWithValue("@courseTitle", courseTi);
                cmdInsert.Parameters.AddWithValue("@credit", creditInt);

                cmdInsert.Connection = insertElecCon;
                insertElecCon.Open();
                //  try {

                cmdInsert.ExecuteNonQuery();

                //}
                //catch (SqlException) { }

                insertElecCon.Close();
            }//endUsing
        }//EndIf
        else
        {
            using (SqlConnection insertCon = new SqlConnection(johnDB))
            {

                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.CommandText = "insertCourse";
                cmdInsert.CommandType = CommandType.StoredProcedure;


                cmdInsert.Parameters.AddWithValue("@coursNumber", courseNum);
                cmdInsert.Parameters.AddWithValue("@courseTitle", courseTi);
                cmdInsert.Parameters.AddWithValue("@credit", creditInt);

                cmdInsert.Connection = insertCon;
                insertCon.Open();
                //  try {

                cmdInsert.ExecuteNonQuery();

                //}
                //catch (SqlException) { }

                insertCon.Close();
            }//EndUsing
        }//endElse
    }
}