using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;

namespace AdoDotNet._16jan
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(Cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Products", con);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("Id");
                    table.Columns.Add("Name");
                    table.Columns.Add("Price");
                    table.Columns.Add("Discounted price");


                    while (reader.Read())
                    {

                        DataRow dataRow = table.NewRow();

                        int originalPrice = Convert.ToInt32(reader["price"]);
                        double dicountedPrice = originalPrice * 90/100;


                        dataRow["Id"] = reader["id"];
                        dataRow["Name"] = reader["pname"];
                        dataRow["Price"] = reader["price"];
                        dataRow["Discounted Price"] = dicountedPrice;

                        table.Rows.Add(dataRow);


                    }

                    GridView1.DataSource = table;
                    GridView1.DataBind();
                }
            }
            
        }
    }
}