using System.Reflection;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;
using Serilog;


namespace UtilityMethodsNS
{

    public class HRequestResolver
    {
        private static readonly string BASE_PATH = "basepath";

        public static string DoResolve(string lambdaName, string processLoadType, IDictionary<string, string> context)
        {
            var loadType = Enum.Parse<LoadType>(processLoadType);
            if (loadType == LoadType.DB)
            {
                return DbResolve(context, loadType);
            }
            else if (loadType == LoadType.FILE)
            {
                return FileResolve(lambdaName, context, loadType);
            }
            else
            {
                throw new HandymanException($"Lambda definition for {lambdaName} is unknown");
            }
        }

        private static string DbResolve(IDictionary<string, string> context, LoadType loadType)
        {
            return context[LoadTypeExtensions.GetDescription(loadType)];
        }

        private static string FileResolve(string lambdaName, IDictionary<string, string> context, LoadType loadType)
        {//TODO check loadtype
            var filePath = context.TryGetValue(LoadTypeExtensions.GetDescription(loadType), out var value) ? value : "";
            var basePath = context.TryGetValue(BASE_PATH, out var value2) ? value2 : "";
            if (String.IsNullOrEmpty(filePath))
            {
                throw new HandymanException($"File path configuration for process {lambdaName} is missing, check spw_process_config or spw_instance_config");
            }
            return ReadFile(filePath, basePath);
        }

        public static string ReadFile(string filePath, string basePath)
        {
            try
            {
                return DoPathResolve(filePath, basePath);
            }
            catch (IOException e)
            {
                throw new HandymanException("Read process definition is failed", e);
            }
        }

        private static string DoPathResolve(string fileRelativePath, string basePath)
        {
            var fullPath = (!String.IsNullOrEmpty(basePath) ? $"{basePath}/" : "") + fileRelativePath;
            LogInformation($"Trying to find the process file {fullPath}");
            var finalPathUrl = Assembly.GetExecutingAssembly().GetManifestResourceStream(fullPath);
            var finalPathInHost = new FileInfo(fullPath);
            if (finalPathUrl != null)
            {
                using (var streamReader = new StreamReader(finalPathUrl, Encoding.UTF8))
                {
                    return streamReader.ReadToEnd();
                }
            }
            else if (finalPathInHost.Exists)
            {
                return File.ReadAllText(fullPath);
            }
            else
            {
                throw new HandymanException($"Process definition for {fullPath} is absent");
            }
        }


        //LoadType loadType = LoadType.DB;
        //string variable = loadType.GetDescription(); // "processContent"

        public class HandymanException : Exception
        {
            public HandymanException(string message) : base(message)
            {
            }

            public HandymanException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }

        private static void LogInformation(string message)
        {
            Log.Information(message);
        }

        public enum LoadType
        {
            [Description("processContent")]
            DB,

            [Description("filepath")]
            FILE,

            [Description("")]
            REST
        }

        public class LoadTypeExtensions
        {
            public static string GetDescription(LoadType value)
            {
                var type = typeof(LoadType);
                var name = Enum.GetName(type, value);
                if (name == null) return null;
                var field = type.GetField(name);
                var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                return attr?.Description;
            }
        }
    }


    public static class PropertyHandler
    {
        private static readonly string CONFIG_PROPERTIES = "config.properties";
        private static readonly NameValueCollection PROPS;

        static PropertyHandler()
        {
            try
            {
                // var assembly = Assembly.GetExecutingAssembly();
                // List<string> resourceNames = new List<string>(assembly.GetManifestResourceNames());
                //string PROJECT_NAME =Assembly.GetEntryAssembly()?.GetName().Name;

                string PROJECT_NAME =Assembly.GetExecutingAssembly()?.GetName().Name;

                var prop = new NameValueCollection();
                using (var input = typeof(PropertyHandler).Assembly.GetManifestResourceStream($"{PROJECT_NAME}.resources.config.properties"))
                {
                    using (var streamReader = new StreamReader(input, Encoding.UTF8))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            line = line.Trim();
                            if (line.StartsWith("#") || line.StartsWith("!"))
                            {
                                continue;
                            }

                            var separatorIndex = line.IndexOf('=');
                            if (separatorIndex < 0)
                            {
                                continue;
                            }

                            var key = line.Substring(0, separatorIndex).Trim();
                            var value = line.Substring(separatorIndex + 1).Trim();
                            if (!string.IsNullOrEmpty(key))
                            {
                                prop.Add(key, value);
                            }
                        }
                    }
                }
                PROPS = prop;
            }
            catch (Exception e)
            {
                Log.Error(e, "Sorry, unable to load {0}", CONFIG_PROPERTIES);
                throw;
            }
        }

        public static string Get(string key)
        {
            return PROPS.Get(key);
        }
    }


}
