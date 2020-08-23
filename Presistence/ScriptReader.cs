using System.Diagnostics;
using System.IO;

namespace Persistence
{
    public class ScriptReader
    {
        //"GetInvoiceDetails.sql"
        public static string GetScript(string scriptName)
        {
            var path = GetScriptPath(scriptName);
            if (!System.IO.File.Exists(path))
            {
                return null;
            }
            using (var rdr = new StreamReader(path))
            {
                return rdr.ReadToEnd();
            }
        }
        private static string GetScriptPath(string scriptName)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}
            var dir = System.IO.Directory.GetCurrentDirectory();
            var filename = System.IO.Path.Combine(dir, "..\\Presistence\\SqlScripts", scriptName);
            return filename;
        }
    }
}
