using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication3
{
    public partial class adminauthormanagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        // add click event
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkIfAuthor())
            {
                Response.Write("<script>alert('Author with this ID already Exists')</script>");
            }
            else
            {
                addnewauthor();
            }
        }

        //Update Click event
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkIfAuthor())
            {
                updateauthor();
            }
            else
            {

                Response.Write("<script>alert('Author with this ID does not Exists')</script>");
            }
        }

        //Delete CClick event
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkIfAuthor())
            {
                deleteauthor();
            }
            else
            {

                Response.Write("<script>alert('Author with this ID does not Exists')</script>");
            }

        }

        //Go event
        protected void Button2_Click(object sender, EventArgs e)
        {
            getauthorbyid();
        }

        void getauthorbyid()
        {
            try
            {

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                // here we check member id should be unique for that we fireing the query to check if it already exsit or not.
                SqlCommand cmd = new SqlCommand("Select * from author_master_tbl Where author_id='" + TextBox3.Text.Trim() + "';", con);
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

        //user define function - addnewauthor

        void addnewauthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Insert Into author_master_tbl(author_id,author_name) values(@author_id,@author_name)", con);
                cmd.Parameters.AddWithValue("@author_id", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());

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

        //user define function
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
                SqlCommand cmd = new SqlCommand("Select * from author_master_tbl Where author_id='" + TextBox3.Text.Trim() + "';", con);
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


        //user drfine function - update author name.

        void updateauthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();


                }

                SqlCommand cmd = new SqlCommand("Update author_master_tbl SET author_name=@author_name Where author_id ='" + TextBox3.Text.Trim() + "'", con);
                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());
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

        //user define function to delete author
        void deleteauthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("Delete from author_master_tbl Where author_id ='" + TextBox3.Text.Trim() + "'", con);

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

        void cleareform()
        {
            TextBox2.Text = "";
            TextBox3.Text = "";
        }


    }
}