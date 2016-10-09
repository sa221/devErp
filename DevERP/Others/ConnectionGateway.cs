using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace DevERP.Others
{
    public class ConnectionGateway
    {

        public string ConnectionString = WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        
        public SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }
        public SqlDataReader Reader { get; set; }
        public string Query { get; set; }

        public ConnectionGateway()
        {
            Connection = new SqlConnection(ConnectionString);
            Command = new SqlCommand();
            Command.Connection = Connection;

            //ConnectionZ = new OleDbConnection(ConnectionStringZ);
            //CommandZ = new OleDbCommand();
            //CommandZ.Connection = ConnectionZ;
        }

        
        //public string ConnectionStringZ = WebConfigurationManager.ConnectionStrings["Zktechoconnectionstring"].ConnectionString;
        //public OleDbConnection ConnectionZ { get; set; }
        //public OleDbCommand CommandZ { get; set; }
        //public OleDbDataReader ReaderZ { get; set; }
        //public string QueryZ { get; set; }
        protected void PrepareCommand(CommandType commandType)
        {
            Command.Parameters.Clear();
            Command.CommandType = commandType;
            Command.CommandText = Query;
        }

        protected void CloseAllConnection()
        {
            if (Reader!=null)
            {
                Reader.Close();
            }
            if (Connection!=null)
            {
                Connection.Close();
            }
        }
    }
}