using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace SceneCrm.Importer {
    public class VolunteerDatabase {
        private string connectionString;
        public VolunteerDatabase(string fullPathToMdbFile, string databasePassword) {
            this.connectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Database Password={1};", fullPathToMdbFile, databasePassword);
        }

        public IEnumerable<VolunteerRecord> VolunteerRecords {
            get {
                using (var conn = new OleDbConnection(connectionString)) {
                    conn.Open();
                    using (var cmd = new OleDbCommand("SELECT * FROM Address", conn)) {
                        using (var reader = cmd.ExecuteReader()) {
                            while (reader.Read()) yield return (new VolunteerRecord(reader));
                        }
                    }
                }
            }
        }
    }

    public class VolunteerRecord {
        public int AccessId;
        public string LastName;
        public string FirstName;
        public string SecondPersonFirstName;
        public string SecondPersonLastName;
        public string Notes;
        public bool NoMailout;
        public bool Deadwood;
        public VolunteerRecord(IDataReader reader) {
            this.AccessId = reader.GetInt32(reader.GetOrdinal("Person ID"));
            this.LastName = reader["Last Name"].ToString();
            this.FirstName = reader["First Name"].ToString();
            this.SecondPersonFirstName = reader["Second Person 1st Name"].ToString();
            this.SecondPersonLastName = reader["Second Person Last Name"].ToString();
            this.Notes = reader["Notes"].ToString();
            this.NoMailout = (bool)reader["No Mailout"];
            this.Deadwood = (bool)reader["Send to deadwood"];
        }
    }
}
