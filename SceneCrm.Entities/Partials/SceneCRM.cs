using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace SceneCrm.Entities
{
    public partial class SceneCRM : DbContext
    {
        public static string GetConnectionString()
        {
            string baseConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            var entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.Provider = "System.Data.SqlClient";
            entityBuilder.ProviderConnectionString = baseConnectionString;
            entityBuilder.Metadata = @"res://*/SceneCRM.csdl|res://*/SceneCRM.ssdl|res://*/SceneCRM.msl";
            return entityBuilder.ToString();
        }
    }
}
