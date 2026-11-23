using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardMetals.CommonLibrary
{
    public static class LogInfo
    {

        private static string fileName;

        static LogInfo()
        {
            // fileName = @"C:\Users\Priti\Desktop\MVC\StandartMetalsERP\StandartMetalsERP\StandardMetals.WebApp\LogError\" + "Log_" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
            // fileName = @"D:\F\Apps\LMS\StandartMetalsERP\StandardMetals.WebApp\LogError\" + "Log_" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
            fileName = @"D:\Project\StandartMetalsERP\StandardMetals.WebApp\LogError\" + "Log_" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
            // D:\F\Apps\LMS\StandartMetalsERP\StandardMetals.WebApp

            //D:\F\Apps\MVC-Priti\Prod\StandartMetalsERP\StandartMetalsERP\StandardMetals.WebApp\LogError
        }

        public static void LogDetailsToFile(string logText)
        {

            //   string fileName = @"D:\Test\Log_14082024.txt";
            FileInfo fi = new FileInfo(fileName);

            try
            {
                // Check if file already exists. If yes, delete it.
                if (fi.Exists)
                {

                    using (StreamWriter sw = File.AppendText(fileName))
                    {
                        sw.WriteLine("Log Date = " + DateTime.Now.ToString());
                        sw.WriteLine("Step= " + logText);
                        sw.WriteLine("---------------------------------------xxxxxxxx------------------------------------");

                    }


                }
                else
                {
                    // Create a new file
                    using (StreamWriter sw = fi.CreateText())
                    {
                        sw.WriteLine("Log Date = " + DateTime.Now.ToString());
                        sw.WriteLine("Step= " + logText);
                        sw.WriteLine("---------------------------------------xxxxxxxx------------------------------------");

                    }
                }

            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }



        }


        public static void LogDetailsToFile(string logText, Exception ex)
        {

            try
            {

                if (File.Exists(fileName))
                {
                }
                else
                {
                    File.CreateText(fileName);
                }

                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine("----------------------------------Exception-----------------------------------------");
                    sw.WriteLine("Log Date = " + DateTime.Now.ToString());
                    sw.WriteLine("Error = " + ex.Message);
                    sw.WriteLine("Stack Trace = " + ex.StackTrace);
                    sw.WriteLine("---------------------------------------xxxxxxxx------------------------------------");

                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }

        public static string LoginDetailToDB(string logText, Exception ex)
        {
            string strCon = @"Data Source=DESKTOP-BQK6HVP\SQLEXPRESS;Initial Catalog=aaliml4n_MVC_ERPNEW;Integrated Security=True";
            string resultMessage = "Success"; // You can customize this message based on your requirements

            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    using (SqlCommand cmd = new SqlCommand("SpLogtoDB", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LogText", logText);
                        cmd.Parameters.AddWithValue("@ExceptionMessage", ex != null ? ex.Message : string.Empty);
                        cmd.Parameters.AddWithValue("@date", DateTime.Now);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception Ex)
            {
                // Handle exceptions here
                resultMessage = "Error: " + Ex.Message;
            }

            return resultMessage;
        }

    }
}