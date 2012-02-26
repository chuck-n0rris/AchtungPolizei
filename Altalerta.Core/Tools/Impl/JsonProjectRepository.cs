using System;
using System.Collections.Generic;
using System.IO;
using Altalerta.Core.Essential;
using Newtonsoft.Json;

namespace Altalerta.Core.Tools.Impl
{
    public class JsonProjectRepository : IProjectRepository
    {
        public IEnumerable<Project> GetAll()
        {
            return JsonConvert.DeserializeObject<List<Project>>(
                File.ReadAllText(GetSettingsPath()));
        }

        private string GetSettingsPath()
        {
            var applicationData = Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData);

            applicationData = Path.Combine(
                applicationData, "Altalerta");

            if (!Directory.Exists(applicationData))
            {
                Directory.CreateDirectory(applicationData);
            }

            return Path.Combine(applicationData, "settings.json");
        }
    }
}