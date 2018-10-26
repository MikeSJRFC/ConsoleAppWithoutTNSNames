using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace ConsoleAppWithoutTNSNames
{
    class Program
    {
        static void Main(string[] args)
        {

            //Demo: Basic ODP.NET Core application to connect, query, and return
            // results from and OracleDataReader to a console

            //Create a connection to Oracle - EZConnect instead of using TNSNames.ora
            string conString = "User ID=JRCCDBA;Password=billion;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=jrv2-OracleSTBY.jrcc.local)(PORT=1521))(CONNECT_DATA = (SERVICE_NAME=jrccprd2.jrcc.local)));";

            //How to connect to an Oracle DB without SQL*Net configuration file
            // also known as tnsnames.ora.
            //"Data Source=<ip or hostname>:1521/service name>;";

            //How to connect to an Oracle DB with a DB alias.
            //Uncomment below and comment above.
            //"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=jrv2-OracleSTBY.jrcc.local)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=jrccprd2.jrcc.local)));";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                    con.Open();
                    cmd.BindByName = true;

                    //Use the command to display employee names from
                    //the EMPLOYEES table
                    cmd.CommandText = "select COMPANY from RXPHON where ID_NO = :id";

                    // Assign id to the department number 50
                    OracleParameter id = new OracleParameter("id", 50773);
                    cmd.Parameters.Add(id);

                    //Execute the command and use DataReader to display the 
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("Company Name: " + reader.GetString(0));
                    }

                    Console.WriteLine();
                    Console.WriteLine("Press 'Enter' to continue");

                    reader.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                    Console.ReadLine();
                }
                con.Close();
            }
        }
    }
}
