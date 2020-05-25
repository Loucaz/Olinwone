using System;
using System.Data.SqlClient;
using System.Data;


namespace Utilitaires
{
    public class DataServices
    {
        #region "Variables locales"

        private SqlDataReader SqlDataReader;

        #endregion

        #region "Propriétés"

        /**
        * \brief Connexion demandée
*/
        public SqlConnection UtilitySqlConnection;

        /**
        * \brief Commande SQL associée à la connexion demandée
*/
        public SqlCommand UtilitySqlCommand;

        /**
        * \brief DataTable résultante de la sélection
*/
        public DataTable UtilityDataTable;

        /**
        * \brief Détermine l'existence des données pour la requête transmise
*/
        public bool UtilityDataExiste;

        /**
        * \brief  Premier enregistrement résultant de la sélection
*/
        public DataRow UtilityDataRow;

        /**
        * \brief Premier champ résultant de la sélection
*/
        public Object UtilityDataField;

        /**
        * \brief Id du dernier enregistrement créé
*/
        public Object UtilityDataId;

        public enum ExecutionMode { INSERT, UPDATE, DELETE };

        #endregion

        #region "Méthodes et fonctions publiques"

        public DataServices(string dsn, string req)
        {
            UtilitySqlConnection = new SqlConnection(dsn);
            UtilitySqlCommand = new SqlCommand(req, UtilitySqlConnection);
            UtilitySqlCommand.Connection.Open();
            UtilitySqlCommand.CommandTimeout = 600;
        }


        /**
        * \brief Décharge la connexion
*/
        public void Dispose()
        {
            try
            {
                UtilitySqlCommand.Dispose();
            }
            catch { }
            try
            {
                UtilitySqlConnection.Dispose();
            }
            catch { }
        }

        /**
        * \brief Ajoute un paramètre pour la requête
*/
        public void AddParameter(string pName, SqlDbType pType, int pSize, ParameterDirection pDirection, Object pValue, string pSourceColumn)
        {
            SqlParameter Parameter = new SqlParameter();
            Parameter.ParameterName = pName;
            Parameter.SqlDbType = pType;
            if (pSize != 0)
            {
                Parameter.Size = pSize;
            }
            Parameter.Direction = pDirection;
            Parameter.Value = pValue;
            Parameter.SourceColumn = pSourceColumn;
            UtilitySqlCommand.Parameters.Add(Parameter);
        }


        /**
        * \brief 
        */
        public void GetStructures()
        {
            try
            {
                UtilityDataTable = new DataTable();
                UtilitySqlCommand.CommandType = CommandType.Text;
                SqlDataReader = UtilitySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                UtilityDataExiste = true;

                UtilityDataTable.Load(SqlDataReader);
                UtilityDataRow = UtilityDataTable.Rows[0];
                UtilityDataField = UtilityDataRow[0];
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                UtilityDataExiste = false;
            }
        }

        /**
        * \brief Execute la requette SQL 
        */
        public void Execute(ExecutionMode mode)
        {
            UtilitySqlCommand.CommandType = CommandType.Text;
            if (mode == ExecutionMode.INSERT)
            {
                UtilitySqlCommand.CommandText += "; SELECT @@IDENTITY;";
                UtilityDataId = UtilitySqlCommand.ExecuteScalar();
            }
            else
            {
                UtilitySqlCommand.ExecuteNonQuery();
            }
            UtilitySqlCommand.Connection.Close();
        }
        #endregion
    }
}