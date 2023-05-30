using System.Collections.Generic;
using System.Data;
using Serilog;

using AccessConfigNS;
using ConfigNS;
using ExceptionNS;
using RepoConfigNS;

namespace ResourceAccessNS
{

    public class ResourceAccess
    {
        internal static IDbConnection GetSqlConnection(string resourceName)
        {
            SpwResourceConfig resource = ConfigAccess.GetResourceConfig(resourceName);
            if (resource == null)
            {
                Log.Warning("{0} not found in Resource connections", resourceName);
                throw new HandymanException("Resource connection is null");
            }
            Log.Warning("{0} found in Resource connections", resource.Config_Name);
            return resource.GetConnection();
        }
    }

}