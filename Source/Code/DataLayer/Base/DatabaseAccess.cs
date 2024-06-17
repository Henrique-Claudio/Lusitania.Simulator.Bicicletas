using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using Lusitania.Simuladores.DataLayer.Properties;
using Oracle.DataAccess.Types;
using System.Xml;

namespace Lusitania.Simuladores.DataLayer.Base
{
    public class DatabaseAccess
    {
        public static string myConnectionString
        {
            get { return Settings.Default.ConnectionString;  }
        }


        public List<OracleParameter> ExecuteQuery(List<OracleParameter> param, string commandText)
        {

            List<OracleParameter> myCollectionList = new List<OracleParameter>();

            OracleConnection myConnection = new OracleConnection(myConnectionString);
            OracleCommand myCommand = new OracleCommand(commandText, myConnection);
            myCommand.BindByName = true;

            myCommand.CommandType = CommandType.StoredProcedure;

            if (param != null)
            {
                foreach (OracleParameter p in param)
                {
                    myCommand.Parameters.Add(p);
                }
            }


            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {                
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }


            foreach (OracleParameter p in myCommand.Parameters)
            {
                if (p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.InputOutput)
                {
                    myCollectionList.Add(p);
                }
            }

            return myCollectionList;
        }

        public List<Object> ExecuteQueryIO(List<OracleParameter> param, string commandText)
        {

            List<Object> myCollectionList = new List<Object>();

            OracleConnection myConnection = new OracleConnection(myConnectionString);
            OracleCommand myCommand = new OracleCommand(commandText, myConnection);
            myCommand.BindByName = true;

            myCommand.CommandType = CommandType.StoredProcedure;

            if (param != null)
            {
                foreach (OracleParameter p in param)
                {
                    myCommand.Parameters.Add(p);
                }
            }


            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();

                foreach (OracleParameter p in myCommand.Parameters)
                {
                    if (p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.ReturnValue || p.Direction == ParameterDirection.InputOutput)
                    {
                        if (p.OracleDbType == OracleDbType.XmlType)
                        {
                            myCollectionList.Add(((Oracle.DataAccess.Types.OracleXmlType)p.Value).Value);
                        }
                        if (p.OracleDbType == OracleDbType.Clob)
                        {
                            myCollectionList.Add(((Oracle.DataAccess.Types.OracleClob)p.Value).Value);
                        }
                        else
                        {
                            myCollectionList.Add(p.Value);
                        }
                    }
                }
            }
            catch (OracleException ex)
            {                
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }

            return myCollectionList;
        }


        public XmlDocument ExecuteQueryIO_XML(OracleParameter param, string commandText)
        {

            XmlDocument _returnValue = new XmlDocument();

            OracleConnection myConnection = new OracleConnection(myConnectionString);
            OracleCommand myCommand = new OracleCommand(commandText, myConnection);
            myCommand.BindByName = true;
            myCommand.CommandType = CommandType.StoredProcedure;
            if (param != null)
            {
                myCommand.Parameters.Add(param);                
            }

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();

                _returnValue = ((Oracle.DataAccess.Types.OracleXmlType)param.Value).GetXmlDocument();
            }
            catch (OracleException ex)
            {
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }

            return _returnValue;
        }

        public List<OracleParameter> ExecuteQueryFunction(List<OracleParameter> param, string commandText)
        {
            List<OracleParameter> myCollectionList = new List<OracleParameter>();

            OracleConnection myConnection = new OracleConnection(myConnectionString);
            OracleCommand myCommand = new OracleCommand(commandText, myConnection);
            myCommand.BindByName = true;
            myCommand.CommandType = CommandType.StoredProcedure;

            if (param != null)
            {
                foreach (OracleParameter p in param)
                {
                    myCommand.Parameters.Add(p);
                }
            }


            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();

            }
            catch (OracleException ex)
            { 
            }                
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }

            foreach (OracleParameter p in myCommand.Parameters)
            {
                if (p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.ReturnValue)
                {
                    myCollectionList.Add(p);
                }
            }

            return myCollectionList;
        }

        public DataTable CursorToDataTable(List<OracleParameter> param, string commandText)
        {
            List<OracleParameter> myCollectionList = new List<OracleParameter>();

            OracleConnection myConnection = new OracleConnection(myConnectionString);
            OracleCommand myCommand = new OracleCommand(commandText, myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            if (param != null)
            {
                foreach (OracleParameter p in param)
                {
                    myCommand.Parameters.Add(p);
                }
            }

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (OracleException Ex)
            {
               
            }

            OracleRefCursor cursor = null;

            foreach (OracleParameter p in myCommand.Parameters)
            {
                if (p.Direction == ParameterDirection.Output && p.Value.GetType() == typeof(OracleRefCursor))
                {
                    cursor = (OracleRefCursor)(p.Value);
                }
            }


            OracleDataAdapter da = new OracleDataAdapter();

            DataTable dt = new DataTable();

            if (cursor != null)
            {
                if (!cursor.IsNull)
                {
                    try
                    {
                        da.Fill(dt, cursor);
                    }
                    catch (OracleException ex)
                    {
                       
                    }
                    finally
                    {
                        myConnection.Close();
                        myConnection.Dispose();
                    }
                }
            }

            if (myConnection.State != ConnectionState.Closed)
            {
                try
                {
                    myConnection.Close();
                    myConnection.Dispose();
                }
                catch (Exception)
                { }
            }

            return dt;

        }

        public DataTable CursorToDataTable(OracleCommand cmd, string ConnectionString)
        {

            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();

            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    cmd.Connection = connection;

                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();

                    cmd.ExecuteNonQuery();


                    OracleRefCursor cursor = null;

                    foreach (OracleParameter p in cmd.Parameters)
                    {
                        if (p.Direction == ParameterDirection.Output && p.Value.GetType() == typeof(OracleRefCursor))
                        {
                            cursor = (OracleRefCursor)(p.Value);
                        }
                    }

                    OracleDataAdapter da = new OracleDataAdapter();
                    if (!cursor.IsNull)
                    {
                        try
                        {
                            da.Fill(dt, cursor);
                        }
                        catch (OracleException ex)
                        {

                        }
                    }
                }
                catch (Exception)
                {

                    cmd.Connection.Close();
                }
            }

            return dt;

        }

    }
}
