using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace RockPaperScissors
{
    public class SQLConector
    {
        SqlConnection myConnection = new SqlConnection("user id=sa;" +
                                       "password='12345';server=localhost;" +
                                       "Trusted_Connection=yes;" +
                                       "database=RockPaperScissors; " +
                                       "connection timeout=30");
        public void openConection()
        {
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void closeConection()
        {
            try
            {
                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public DataTable selectWinners()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from Winners",
                                                         myConnection);
                myReader = myCommand.ExecuteReader();                
                dt.Load(myReader);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return dt;
        }

        public void insertWinner(string name, int points)
        {
            try
            {
                using (myConnection)
                {
                    myConnection.Open();

                    SqlCommand cmd = new SqlCommand("InsertWinner", myConnection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@WinnerName", name));
                    cmd.Parameters.Add(new SqlParameter("@WinnerScore", points));

                    cmd.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally {
                myConnection.Close();
            }
        }

        public void deletetWinners()
        {
            try
            {
                using (myConnection)
                {
                    myConnection.Open();

                  
                    SqlCommand cmd = new SqlCommand("DeleteWinners", myConnection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                myConnection.Close();
            }
        }
    }
}