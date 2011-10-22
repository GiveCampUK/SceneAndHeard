using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace SceneCrm.Importer {
    public class ChildrenProductionsSpreadsheet {
        public class Row {
            int i;
            private IDataReader reader;
            public Row(IDataReader reader) {
                this.reader = reader;
            }
            public string MembershipNumber { get { return (reader.IsDBNull(i = 0) ? (string)null : reader.GetString(i)); } }
            public string MmemcarNnews2011 { get { return (reader.IsDBNull(i = 1) ? (string)null : reader.GetString(i)); } }
            public string Surname { get { return (reader.IsDBNull(i = 2) ? (string)null : reader.GetString(i)); } }
            public string Forename { get { return (reader.IsDBNull(i = 3) ? (string)null : reader.GetString(i)); } }
            public string PlaymakingOneTerm { get { return (reader.IsDBNull(i = 4) ? (string)null : reader.GetString(i)); } }
            public string PlaymakingOneYear { get { return (reader.IsDBNull(i = 5) ? (string)null : reader[i].ToString()); } }
            public string ReplayTerm { get { return (reader.IsDBNull(i = 6) ? (string)null : reader.GetString(i)); } }
            public string ReplayYear { get { return (reader.IsDBNull(i = 7) ? (string)null : reader.GetString(i)); } }
            public string StageOneTerm { get { return (reader.IsDBNull(i = 8) ? (string)null : reader.GetString(i)); } }
            public string StageOneYear { get { return (reader.IsDBNull(i = 9) ? (string)null : reader.GetString(i)); } }
            public string OneonOneTerm { get { return (reader.IsDBNull(i = 10) ? (string)null : reader.GetString(i)); } }
            public string OneonOneYear { get { return (reader.IsDBNull(i = 11) ? (string)null : reader.GetString(i)); } }
            public string Playback { get { return (reader.IsDBNull(i = 12) ? (string)null : reader.GetString(i)); } }
            public string PlaymakingOneProduction { get { return (reader.IsDBNull(i = 13) ? (string)null : reader.GetString(i)); } }
            public string Z_PlaymakingOneYear { get { return (reader.IsDBNull(i = 14) ? (string)null : reader.GetString(i)); } }
            public string PlaymakingOnePlay { get { return (reader.IsDBNull(i = 15) ? (string)null : reader.GetString(i)); } }
            public string PlaymakingOneDramaturg { get { return (reader.IsDBNull(i = 16) ? (string)null : reader.GetString(i)); } }
            public string PlaymakingOneDirector { get { return (reader.IsDBNull(i = 17) ? (string)null : reader.GetString(i)); } }
            public string PlaymakingOneActor1 { get { return (reader.IsDBNull(i = 18) ? (string)null : reader.GetString(i)); } }
            public string PlaymakingOneActor2 { get { return (reader.IsDBNull(i = 19) ? (string)null : reader.GetString(i)); } }
            public string PlaymakingOneActor3 { get { return (reader.IsDBNull(i = 20) ? (string)null : reader.GetString(i)); } }
            public string ReplayProduction { get { return (reader.IsDBNull(i = 21) ? (string)null : reader.GetString(i)); } }
            public string Z_ReplayYear { get { return (reader.IsDBNull(i = 22) ? (string)null : reader.GetString(i)); } }
            public string ReplayPlay { get { return (reader.IsDBNull(i = 23) ? (string)null : reader.GetString(i)); } }
            public string ReplayDramaturg { get { return (reader.IsDBNull(i = 24) ? (string)null : reader.GetString(i)); } }
            public string ReplayDirector { get { return (reader.IsDBNull(i = 25) ? (string)null : reader.GetString(i)); } }
            public string ReplayActor1 { get { return (reader.IsDBNull(i = 26) ? (string)null : reader.GetString(i)); } }
            public string ReplayActor2 { get { return (reader.IsDBNull(i = 27) ? (string)null : reader.GetString(i)); } }
            public string ReplayActor3 { get { return (reader.IsDBNull(i = 28) ? (string)null : reader.GetString(i)); } }
            public string OneonOneProduction { get { return (reader.IsDBNull(i = 29) ? (string)null : reader.GetString(i)); } }
            public string Z_OneonOneYear { get { return (reader.IsDBNull(i = 30) ? (string)null : reader.GetString(i)); } }
            public string OneonOnePlay { get { return (reader.IsDBNull(i = 31) ? (string)null : reader.GetString(i)); } }
            public string OneonOneWriter { get { return (reader.IsDBNull(i = 32) ? (string)null : reader.GetString(i)); } }
            public string OneonOneDirector { get { return (reader.IsDBNull(i = 33) ? (string)null : reader.GetString(i)); } }
            public string OneonOneActor { get { return (reader.IsDBNull(i = 34) ? (string)null : reader.GetString(i)); } }
            public string PlaybackProduction { get { return (reader.IsDBNull(i = 35) ? (string)null : reader.GetString(i)); } }
            public string PlaybackYear { get { return (reader.IsDBNull(i = 36) ? (string)null : reader.GetString(i)); } }
            public string PlaybackPlay { get { return (reader.IsDBNull(i = 37) ? (string)null : reader.GetString(i)); } }
            public string PlaybackDramaturg { get { return (reader.IsDBNull(i = 38) ? (string)null : reader.GetString(i)); } }
            public string PlaybackDirector { get { return (reader.IsDBNull(i = 39) ? (string)null : reader.GetString(i)); } }
            public string Playback2ndWriter { get { return (reader.IsDBNull(i = 40) ? (string)null : reader.GetString(i)); } }
            public string PlaybackActor { get { return (reader.IsDBNull(i = 41) ? (string)null : reader.GetString(i)); } }
            public string StageOneActor { get { return (reader.IsDBNull(i = 42) ? (string)null : reader.GetString(i)); } }
            public string QuestionnaireResponses { get { return (reader.IsDBNull(i = 43) ? (string)null : reader.GetString(i)); } }
            public bool HasData {
                get {
                    if (this.Forename == "Forename" || this.Surname == "Surname") return (false);
                    return (!(reader.IsDBNull(0) || reader.IsDBNull(2)));
                }
            }

            public bool AttendedPm1 {
                get {
                    int year;
                    if (Int32.TryParse(this.PlaymakingOneYear, out year)) return (!string.IsNullOrEmpty(this.PlaymakingOneTerm));
                    return (false);
                }
            }
        }
        

        private string fullPathToExcelFile;
        public ChildrenProductionsSpreadsheet(string fullPathToExcelFile) {
            this.fullPathToExcelFile = fullPathToExcelFile;
        }
        public IEnumerable<Row> Rows {
            get {
                var connectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES;\"", fullPathToExcelFile);
                using (var conn = new OleDbConnection(connectionString)) {
                    using (var cmd = new OleDbCommand("SELECT * FROM [Children & Productions$]", conn)) {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader()) {
                            while (reader.Read()) {
                                var row = new Row(reader);
                                if (row.HasData) yield return (row);
                            }
                        }
                    }
                }
            }
        }
    }
}