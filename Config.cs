using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace scoop_manifest_generator
{
    public partial class Config
    {
        public JObject LoadConfig()
        {
            try
            {
                string json = File.ReadAllText("config.json");

                JObject jsonObject = JsonConvert.DeserializeObject<JObject>(json);

                return jsonObject;
            }
            catch (FileNotFoundException)
            {
                string error = "{ \"status\": \"error\" }";
                JObject jsonObject = JsonConvert.DeserializeObject<JObject>(error);

                Console.WriteLine("Error: file not found");

                return jsonObject;
            }
            catch (Exception ex)
            {
                string error = "{ \"status\": \"error\" }";
                JObject jsonObject = JsonConvert.DeserializeObject<JObject>(error);

                Console.WriteLine("Error:");
                Console.WriteLine(ex);

                return jsonObject;
            }
        }
    }
}
