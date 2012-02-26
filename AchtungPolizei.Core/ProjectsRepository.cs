using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace AchtungPolizei.Core
{
    public class ProjectsRepository
    {
        public IList<Project> GetProjects()
        {
            var settings = GetSettingsPath();
            try
            {
                return JsonConvert.DeserializeObject<List<Project>>(
                    File.ReadAllText(settings),
                    new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.Objects
                        });
            }
            catch (Exception)
            {
                return new List<Project>();
            }
        }

        public void SaveProjects(IEnumerable<Project> projects)
        {
            File.WriteAllText(
                GetSettingsPath(),
                JsonConvert.SerializeObject(
                    projects.ToList(),
                    Formatting.Indented,
                    new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.Objects
                        }));
        }

        private string GetSettingsPath()
        {
            var applicationData = Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData);

            applicationData = Path.Combine(
                applicationData, "AchtungPolizei");

            if (!Directory.Exists(applicationData))
            {
                Directory.CreateDirectory(applicationData);
            }

            return Path.Combine(applicationData, "settings.config");
        }
    }
}