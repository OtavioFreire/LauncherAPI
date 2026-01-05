namespace LauncherAPI
{
    public class VersionService
    {
        private static List<ClientInformation> mkpClient = new List<ClientInformation> { 
            new ClientInformation { Id = 5, VersionNumber = "1.0.0.5"},
            new ClientInformation { Id = 234, VersionNumber = "2.0.0.1"}
        };

        public static LastestResponse Latest(VersionSettings versionSettings, int idClient, string version) 
        {
            LastestResponse response = new LastestResponse();

            ClientInformation clientInfo = mkpClient.First(x => x.Id == idClient);
            
            response.update = Version.Parse(version) < Version.Parse(clientInfo.VersionNumber);

            if(!response.update)
                return response;

            response.version = version;
            response.urlDownload = $"{versionSettings.UrlDownload}{clientInfo.VersionNumber}{versionSettings.Extension}";

            return response;
        }
    }
}
