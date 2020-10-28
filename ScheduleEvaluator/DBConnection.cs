using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace ScheduleEvaluator
{
    public class DBConnection
    {
        SqlConnection myConnection;      //Declare the SQL connection to Database

        #region SQL Stuff
        //------------------------------------------------------------------------------
        // concise code to execute queries
        // 
        // 
        //------------------------------------------------------------------------------

        // a C# 'DataTable' represents "one table of in-memory data" (see .NET docs)
        // 'ExecuteToDT' essentially means:
        //      - execute SQL command
        //      - store execution result in a DataTable object
        //      - return the now-populated DataTable object
        public DataTable ExecuteToDT(string query)
        {
            OpenSQLConnection();
            
            // construct an SQL command object
            SqlCommand cmd = new SqlCommand(query, myConnection);
            
            // empty DataTable; will hold the results returned by the command
            DataTable dt = new DataTable();
            
            // [COMMENT Oct. 27, 2020]:
            // this nested 'using' block should be rewritten just a tad bit, because
            // the variable <con> is not used at all, and command creation is redundant.
            // making use of the vars created with 'using' will eliminate
            // redundancy and improve operational efficiency and memory usage. thanks!
            using (var con = myConnection)
            {
                using (var command = new SqlCommand(query))
                {
                    myConnection.Open();
                    
                    // a C# 'SqlDataReader' (see .NET docs):
                    // "Provides a way of reading a forward-only'
                    // 'stream of rows from a SQL Server database."
                    //
                    // 'ExecuteReader()' (see .NET docs):
                    // sends CommandText to the Connection, builds a SqlDataReader,
                    // and returns this reader WITH execution results/values/data.
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // populate the temporary DataTable with the SqlDataReader's results
                        dt.Load(dr);
                    }
                }
            }
            
            // cleanup and return the newly populated DataTable
            myConnection.Close();
            return dt;
        }

        // 'ExecuteToString' essentially means:
        //      - execute SQL command
        //      - store execution result in a StringBuilder object (mutable string of chars) 
        //      - return the string data from the StringBuilder object
        public string ExecuteToString(string query)
        {
            OpenSQLConnection();
            
            // construct an SQL command object
            SqlCommand cmd = new SqlCommand(query, myConnection);
            myConnection.Open();
            var result = new StringBuilder();
            
            // 'ExecuteReader()' (see .NET docs):
            // sends CommandText to the Connection, builds a SqlDataReader,
            // and returns this reader WITH execution results/values/data.
            var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                result.Append("[]");
            }
            else
            {
                // 'reader.Read()' advances the SqlDataReader's current row
                // to the next row;
                while (reader.Read())
                {
                    // 'reader.GetValue(i)' returns the data from the
                    // "ith" column in the CURRENT ROW that <reader> points to
                    result.Append(reader.GetValue(0).ToString());
                }
            }
            
            // cleanup and return the result's string data
            myConnection.Close();
            return result.ToString();
        }
        //------------------------------------------------------------------------------
        // sql connection
        // 
        // 
        //------------------------------------------------------------------------------
        private void OpenSQLConnection()
        {

            myConnection = new SqlConnection("Data Source=65.175.68.34;Initial Catalog=vsaDev;Persist Security Info=True;User ID=sa;Password=kD$wg&OUrhfC6AMMq6q5Xhj");
        }
        #endregion

    }
}
