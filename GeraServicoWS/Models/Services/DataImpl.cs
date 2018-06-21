using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace GeraServicoWS.Models.Services
{
    public class DataImpl : Data
    {
        static string conn = ConfigurationManager.ConnectionStrings["ConnStrDb"].ConnectionString;

        SqlConnection connection;

        SqlCommand command;
        SqlCommand command1;
        
        public override Dictionary<string,string> GetTableApi(string owner)
        {
            
            List<TableOwner> listTable = new List<TableOwner>();
            List<Property> listPropertys = null;
            Dictionary<string, string> keys = new Dictionary<string, string>();
            int count = 0;

            using (connection = new SqlConnection(conn))
            {
                
                string cmd = "SELECT " +
               "referencingTable.Name '[Tables]' " +
               "FROM sys.foreign_key_columns fk " +
                "JOIN sys.tables referencingTable ON fk.parent_object_id = referencingTable.object_id " +
                "JOIN sys.schemas sch ON referencingTable.schema_id = sch.schema_id " +
                "JOIN sys.objects foreignKey ON foreignKey.object_id = fk.constraint_object_id " +
                "JOIN sys.tables referencedTable ON fk.referenced_object_id = referencedTable.object_id " +
               "WHERE referencedTable.name = 'Owner'";
                command = new SqlCommand(cmd, connection);
                command.CommandType = CommandType.Text;
                try
                {
                    connection.Open();
                    var read = command.ExecuteReader();
                    while (read.Read())
                    {
                        listTable.Add( new TableOwner { NameTable = read["[Tables]"].ToString() });
                    }
                    
                }
                catch (SqlException e)
                {
                    throw new ArgumentException("A pesquisa não retornou resultados. Erro:" + e.Message);
                }
            }

            using(connection = new SqlConnection(conn))
            {
                foreach(var t in listTable)
                {
                    if (t.NameTable.Contains(owner) && owner != "")
                    {
                        listPropertys = new List<Property>();
                        string cmd = "select *from " + t.NameTable;
                        command = new SqlCommand(cmd, connection);
                        command.CommandType = CommandType.Text;
                        try
                        {
                            connection.Open();
                            var read = command.ExecuteReader();
                            var schema = read.GetSchemaTable();
                            foreach (DataRow r in schema.Rows)
                            {
                                foreach (DataColumn c in schema.Columns)
                                {
                                    var prop = c.ColumnName + "=" + r[c].ToString();
                                    if (prop.Contains("ColumnName"))
                                    {

                                        listPropertys.Add(new Property { PropertyName = r[c].ToString() });
                                        break;
                                    }

                                }
                            }
                            while (read.Read())
                            {
                                
                                for (int i = 0; i < listPropertys.Count; i++)
                                {
                                    string value = read[listPropertys[i].PropertyName].ToString();
                                    if (keys.ContainsKey(listPropertys[i].PropertyName))
                                    {
                                        
                                        keys.Add(listPropertys[i].PropertyName + count.ToString(), value);
                                        count++;
                                    }
                                    else
                                    {
                                        keys.Add(listPropertys[i].PropertyName, value);
                                    }
                                }
                               
                                
                            }
                            connection.Close();

                            

                        }catch(SqlException e)
                        {
                            throw new ArgumentException("A pesquisa não retornou resultados. Erro:" + e.Message);
                        }
                    }
                  
                }

                
            }

            return keys;
        }

        public override Owner GetOwner(string login)
        {
            using (connection = new SqlConnection(conn))
            {
                string cmd = "select *from Owner where Login = @Login";
                Owner owner = new Owner();
                command = new SqlCommand(cmd, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@Login", SqlDbType.NVarChar);
                command.Parameters["@Login"].Value = login;
                try
                {
                    connection.Open();
                    var read = command.ExecuteReader();
                    while (read.Read())
                    {
                        owner.Id = Convert.ToInt32(read["Id"]);
                        owner.Login = read["Login"].ToString();
                        owner.Senha = read["Senha"].ToString();
                        owner.Remenber = Convert.ToBoolean(read["Remenber"]);
                    }
                    return owner;

                }
                catch (SqlException ex)
                {
                    throw new ArgumentException("A pesquisa não retornou dados. Erro:" + ex.Message);
                }
            }
        }

        public override List<Owner> GetOwners()
        {
            using (connection = new SqlConnection(conn))
            {
                string cmd = "select *from Owner";
                List<Owner> list = new List<Owner>();
                command = new SqlCommand(cmd, connection);
                command.CommandType = CommandType.Text;
                try
                {
                    connection.Open();
                    var read = command.ExecuteReader();
                    while (read.Read())
                    {
                        list.Add(new Owner { Id = Convert.ToInt32(read["Id"]), Email = read["Email"].ToString(), Login = read["Login"].ToString(), Senha = read["Senha"].ToString(), Remenber = Convert.ToBoolean(read["Remenber"]) });
                    }
                    return list;

                }
                catch (SqlException ex)
                {
                    throw new ArgumentException("A pesquisa não retornou dados. Erro:" + ex.Message);
                }
            }
        }

        public override void SaveNewApi(Owner owner, params string[] values)
        {
            using (connection = new SqlConnection(conn))
            {
                command = new SqlCommand("CreateTableInsertData", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@route", SqlDbType.NVarChar);
                command.Parameters.Add("@property1", SqlDbType.NVarChar);
                command.Parameters.Add("@value1", SqlDbType.NVarChar);
                command.Parameters.Add("@property2", SqlDbType.NVarChar);
                command.Parameters.Add("@value2", SqlDbType.NVarChar);
                command.Parameters.Add("@property3", SqlDbType.NVarChar);
                command.Parameters.Add("@value3", SqlDbType.NVarChar);
                command.Parameters.Add("@property4", SqlDbType.NVarChar);
                command.Parameters.Add("@value4", SqlDbType.NVarChar);
                command.Parameters.Add("@property5", SqlDbType.NVarChar);
                command.Parameters.Add("@value5", SqlDbType.NVarChar);
                command.Parameters.Add("@property6", SqlDbType.NVarChar);
                command.Parameters.Add("@value6", SqlDbType.NVarChar);
                command.Parameters.Add("@property7", SqlDbType.NVarChar);
                command.Parameters.Add("@value7", SqlDbType.NVarChar);
                command.Parameters.Add("@property8", SqlDbType.NVarChar);
                command.Parameters.Add("@value8", SqlDbType.NVarChar);
                command.Parameters.Add("@property9", SqlDbType.NVarChar);
                command.Parameters.Add("@value9", SqlDbType.NVarChar);
                command.Parameters.Add("@property10", SqlDbType.NVarChar);
                command.Parameters.Add("@value10", SqlDbType.NVarChar);
                command.Parameters.Add("@login", SqlDbType.NVarChar);
                command.Parameters.Add("@idOwner", SqlDbType.NVarChar);
                command.Parameters.Add("@extension", SqlDbType.NVarChar);
                int j = 1;
                for (int i = 0; i < command.Parameters.Count; i++)
                {
                    if (values[j] == null || values[j] == "")
                    {
                        command.Parameters[i].Value = "Coluna_Vazia" + j.ToString();
                    }
                    else
                    {

                        command.Parameters[i].Value = values[j];
                    }
                    if (command.Parameters[i].ParameterName == "@idOwner")
                    {
                        command.Parameters[i].Value = owner.Id.ToString();
                    }
                    if (command.Parameters[i].ParameterName == "@login")
                    {
                        command.Parameters[i].Value = owner.Login;
                    }
                    if (command.Parameters[i].ParameterName == "@extension")
                    {
                        var rand = new Random(DateTime.Now.Millisecond);
                        command.Parameters[i].Value = rand.Next().ToString();
                    }

                    j++;
                }
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public override void SaveOwner(Owner owner)
        {

            using (connection = new SqlConnection(conn))
            {
                string cmd = "insert Owner values(@Email, @Login, @Senha, @Remenber)";
                string cmd1 = "select *from Owner";
                command = new SqlCommand(cmd, connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.Add("@Email", SqlDbType.Text);
                command.Parameters.Add("@Login", SqlDbType.Text);
                command.Parameters.Add("@Senha", SqlDbType.Text);
                command.Parameters.Add("@Remenber", SqlDbType.Int);
                command.Parameters["@Email"].Value = owner.Email;
                command.Parameters["@Login"].Value = owner.Login;
                command.Parameters["@Senha"].Value = owner.Senha;
                command.Parameters["@Remenber"].Value = Convert.ToInt32(owner.Remenber);
                command1 = new SqlCommand(cmd1, connection);

                try
                {
                    connection.Open();
                    var read = command1.ExecuteReader();
                    while (read.Read())
                    {
                        if (read["Login"].ToString() == owner.Login)
                        {
                            throw new ArgumentException("Esse Login já exixte.");
                        }
                        if (read["Email"].ToString() == owner.Email)
                        {
                            throw new ArgumentException("Esse Email já existe.");
                        }
                    }
                    connection.Close();

                    connection.Open();

                    int rowAffected = command.ExecuteNonQuery();

                }
                catch (SqlException ex)
                {
                    throw new ArgumentException("Não foi possível salvar dados. Erro:" + ex.Message);
                }
            }
        }
    }
}