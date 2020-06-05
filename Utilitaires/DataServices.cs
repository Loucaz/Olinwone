using System;
using System.Data.SqlClient;
using System.Data;

namespace Utilitaires
{
    //////////////////////////////////////////////////
    /// @class DataServices
    /// @par Classe pour travailler avec des requêtes SQL ainsi que ses paramètres destiner un interroger une base SQL serveur.
    /// Exemple d’utilisation:
    /// @n Constructeur: configure le SqlConnection et la SqlCommand
    /// @n AddParameter (facultatif): ajouter des paramètres a la requête
    /// @n GetStructures (facultatif): permet de faire des verifications
    /// @n Execute (facultatif): execute la requette SQL
    /// @n Dispose: Ferme le SqlConnection et la SqlCommand
    /// @note Il est conseiller de se renseigner sur  System.Data.SqlClient avec notament SqlConnection et SqlCommand
    //////////////////////////////////////////////////
    public class DataServices
    {
        #region "Variables locales"
        private SqlDataReader SqlDataReader;
        #endregion

        #region "Propriétés"
        //////////////////////////////////////////////////
        /// @brief Connexion demandée
        //////////////////////////////////////////////////
        public SqlConnection UtilitySqlConnection;

        //////////////////////////////////////////////////
        /// @brief Commande SQL associée à la connexion demandée
        //////////////////////////////////////////////////
        public SqlCommand UtilitySqlCommand;

        //////////////////////////////////////////////////
        /// @brief DataTable résultante de la requête
        //////////////////////////////////////////////////
        public DataTable UtilityDataTable;

        //////////////////////////////////////////////////
        /// @brief Détermine l'existence des données pour la requête transmise
        //////////////////////////////////////////////////
        public bool UtilityDataExiste;

        //////////////////////////////////////////////////
        /// @brief Premier enregistrement résultant de la requête
        //////////////////////////////////////////////////
        public DataRow UtilityDataRow;

        //////////////////////////////////////////////////
        /// @brief Premier champ résultant de la requête
        //////////////////////////////////////////////////
        public Object UtilityDataField;

        //////////////////////////////////////////////////
        /// @brief Id du dernier enregistrement créé
        //////////////////////////////////////////////////
        public Object UtilityDataId;


        //////////////////////////////////////////////////
        /// @brief Mode de la requête: INSERT / UPDATE / DELETE
        //////////////////////////////////////////////////
        public enum ExecutionMode { INSERT, UPDATE, DELETE };

        #endregion

        #region "Méthodes et fonctions publiques"

        //////////////////////////////////////////////////
        /// @brief Constructeur pour configurer le SqlConnection et la SqlCommand
        /// 
        /// Ouvre la connection @n 
        /// Prepare la commande Sql 
        /// @param dsn Config de connection ex: "Server=USER\SQLEXPRESS;User Id=user;Password=1234;"
        /// @param req La requête
        //////////////////////////////////////////////////
        public DataServices(string dsn, string req)
        {
            UtilitySqlConnection = new SqlConnection(dsn);
            UtilitySqlCommand = new SqlCommand(req, UtilitySqlConnection);
            UtilitySqlCommand.Connection.Open();
            UtilitySqlCommand.CommandTimeout = 600;
        }

        //////////////////////////////////////////////////
        /// @brief Décharge la connexion
        /// 
        //////////////////////////////////////////////////
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

        //////////////////////////////////////////////////
        /// @brief Ajoute un paramètre pour la requête
        /// @param pName Nom du paramètre comme user
        /// @param pType SqlDbType comme int ou encore VarChar 
        /// @param pSize Taille du SqlDbType comme 255 pour un Varchar
        /// @param pDirection Spécifie le type d’un paramètre dans une requête par rapport à DataSet: Input / InputOutput	/ Output / ReturnValue
        /// @param pValue Valeur 
        /// @param pSourceColumn Champ
        //////////////////////////////////////////////////
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


        //////////////////////////////////////////////////
        /// @brief Recupere la structure de la requete
        /// 
        /// Stock le premier enregistrement et le premier champ de la requête @n
        /// Vérifie si des données existent avec UtilityDataExiste
        //////////////////////////////////////////////////
        public void GetStructures()
        {
            try
            {
                UtilityDataTable = new DataTable();
                UtilitySqlCommand.CommandType = CommandType.Text;
                SqlDataReader = UtilitySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                UtilityDataTable.Load(SqlDataReader);

                UtilityDataRow = UtilityDataTable.Rows[0];
                UtilityDataField = UtilityDataRow[0];
                UtilityDataExiste = true;
            }
            catch (Exception)
            {
                UtilityDataExiste = false;
            }
        }

        //////////////////////////////////////////////////
        /// @brief Execute la requette SQL
        /// 
        /// Stock le dernier resultat dans UtilityDataId @n
        /// Ferme la connection 
        //////////////////////////////////////////////////
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