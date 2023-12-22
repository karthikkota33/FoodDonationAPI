using Microsoft.Extensions.FileProviders;

namespace FoodDonationAPI.Helpers
{
    public static class EmailTemplates
    {
        static IWebHostEnvironment _hostingEnvironment;
        static IConfigurationRoot _configurationRoot;
        static string testEmailTemplate;

        public static void Initialize(IWebHostEnvironment hostingEnvironment, IConfigurationRoot configurationRoot)
        {
            _hostingEnvironment = hostingEnvironment;
            _configurationRoot = configurationRoot;
        }

        public static string GetTestEmail()
        {
            testEmailTemplate = ReadPhysicalFile("Helpers/Templates/TestTemplate.template");
            string emailMessage = testEmailTemplate.ToString();
              //.Replace("{user}", recepientName)
              //.Replace("{footerLink}", domain + "assets/images/risk-dynamics-logo.png")
              //.Replace("{link}", link);

            return emailMessage;
        }

        private static string ReadPhysicalFile(string path)
        {
            if (_hostingEnvironment == null)
                throw new InvalidOperationException($"{nameof(EmailTemplates)} is not initialized");

            IFileInfo fileInfo = _hostingEnvironment.ContentRootFileProvider.GetFileInfo(path);

            if (!fileInfo.Exists)
                throw new FileNotFoundException($"Template file located at \"{path}\" was not found");

            using (var fs = fileInfo.CreateReadStream())
            {
                using (var sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
