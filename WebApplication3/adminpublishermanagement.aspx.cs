using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication3
{
    public partial class adminpublishermanagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }



        //Add button
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkIfAuthor())
            {
                Response.Write("<script>alert('Author with this ID already Exists')</script>");
            }
            else
            {
                addpublisher();
            }
        }

        //update button
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkIfAuthor())
            {
                updatepublisher();
            }
            else
            {

                Response.Write("<script>alert('Author with this ID does not Exists')</script>");
            }
        }

        //delete button
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkIfAuthor())
            {
                deletepublisher();
            }
            else
            {

                Response.Write("<script>alert('Author with this ID does not Exists')</script>");
            }
        }

        //Go button
        protected void Button2_Click(object sender, EventArgs e)
        {
            getpublisherid();
        }

        void addpublisher()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Insert Into publisher_master_tbl(publisher_id,publisher_name) values(@publisher_id,@publisher_name)", con);
                cmd.Parameters.AddWithValue("@publisher_id", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@publisher_name", TextBox2.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Sign Up Successful. Go to User Login to Login');</script>");
                cleareform();
                GridView1.DataBind();

            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void deletepublisher()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("Delete from publisher_master_tbl Where publisher_id ='" + TextBox3.Text.Trim() + "'", con);

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Author Deleted successfully')</script>");
                cleareform();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void updatepublisher()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();


                }

                SqlCommand cmd = new SqlCommand("Update publisher_master_tbl SET publisher_name=@publisher_name Where publisher_id ='" + TextBox3.Text.Trim() + "'", con);
                cmd.Parameters.AddWithValue("@publisher_name", TextBox2.Text.Trim());
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Author Update successfully')</script>");
                cleareform();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }


        void cleareform()
        {
            TextBox2.Text = "";
            TextBox3.Text = "";
        }


        bool checkIfAuthor()
        {
            try
            {

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                // here we check member id should be unique for that we fireing the query to check if it already exsit or not.
                SqlCommand cmd = new SqlCommand("Select * from publisher_master_tbl Where publisher_id='" + TextBox3.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd); // it used in a disconnected way.
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    return true;

                }
                else
                {
                    return false;
                }



            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        void getpublisherid()
        {
            try
            {

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                // here we check member id should be unique for that we fireing the query to check if it already exsit or not.
                SqlCommand cmd = new SqlCommand("Select * from publisher_master_tbl Where publisher_id='" + TextBox3.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd); // it used in a disconnected way.
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0][1].ToString();

                }
                else
                {
                    Response.Write("<script>alert('Invalid author id');</script>");
                }



            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }
    }
}