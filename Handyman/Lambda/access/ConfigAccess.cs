using ConfigNS;

namespace AccessConfigNS
{

    public class ConfigAccess
    {
        private static readonly HandymanRepo REPO = new HandymanRepoImpl();


        public static Dictionary<string, string> GetAllConfig(string pipelineName)
        {
            return REPO.GetAllConfig(pipelineName);
        }

        public static Dictionary<string, string> GetCommonConfig()
        {
            return REPO.GetCommonConfig();
        }
        public static SpwResourceConfig GetResourceConfig(string name)
        {
            return REPO.GetResourceConfig(name);
        }

        public static HashSet<string> GetPackageAction()
        {
            return REPO.GetPackageAction();
        }

        public static string GetLambdaName(string pipelineName)
        {
            if (pipelineName != null)
            {
                if (pipelineName.Contains("#"))
                {
                    return pipelineName.Substring(0, pipelineName.LastIndexOf("#"));
                }
            }
            return pipelineName;
        }
    }

}