namespace LauncherAPI
{
    public class VersionService
    {
        private static List<ClientInformation> mkpClient = new List<ClientInformation> { 
            new ClientInformation { cnpj = "60.938.777/0001-23", versionNumber = "1.0.0.5"},
            new ClientInformation { cnpj = "60.938.777/0001-24", versionNumber = "2.0.0.1"}
        };

        public static LastestResponse Latest(VersionSettings versionSettings, string cnpj, string version) 
        {
            LastestResponse response = new LastestResponse();

            ClientInformation clientInfo = mkpClient.First(x => x.cnpj == cnpj);
            
            response.update = Version.Parse(version) < Version.Parse(clientInfo.versionNumber);

            if(!response.update)
                return response;

            response.version = version;
            response.urlDownload = $"{versionSettings.UrlDownload}{clientInfo.versionNumber}{versionSettings.Extension}";

            return response;
        }
    }
}
