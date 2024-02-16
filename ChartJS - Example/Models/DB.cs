using System.Data;
using System.Data.SqlClient;
using System.Numerics;

namespace ChartExample.Models
{
    public class DB
    {
        public SqlConnection con { get; set; }
        public DB()
        {
            string conStr = "Data Source=DESKTOP-1DD8VBL;Initial Catalog=SurveyProject;Integrated Security=True";
            con = new SqlConnection(conStr);
        }

        public Dictionary<string, int> getFavouriteCodeEditors()
        {
            Dictionary<string, int> labelsAndCounts = new Dictionary<string, int>();

            try
            {
                con.Open();

                string query = "select code_editor, count(code_editor) as count from student_info group by code_editor";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader sdr = cmd.ExecuteReader();

                // store labels and counts inside the dictionary
                while (sdr.Read())
                {
                    labelsAndCounts.Add(sdr["code_editor"].ToString(), (int)sdr["count"]);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { con.Close(); }

            return labelsAndCounts;
        }
    }
}
