using DbUp;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace GabrielesProject.AdformExam.DBEditor
{
    internal class Program
    {
        static bool ExecuteScripts(string dbConnectionString, string scriptFilter)
        {
            var upgrader =
                DeployChanges.To
                    .PostgresqlDatabase(dbConnectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), name => name.Contains(scriptFilter))
                    .WithVariablesEnabled()
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                return false;
            }
       ;
            return true;
        }

        static int Main()
        {
            Console.Clear();

            //get app settings from API app
            string dirs = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\GabrielesProject.AdformExam.WebAPI"));
            var builder = new ConfigurationBuilder()
                    .SetBasePath(dirs)
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddJsonFile("appsettings.Development.json", optional: false);

            IConfiguration config = builder.Build();
            string dbConnectionString = config.GetSection("ConnectionStrings").GetValue<string>("PostgreConnection") ?? throw new ArgumentNullException();

            EnsureDatabase.For.PostgresqlDatabase(dbConnectionString);

            Console.WriteLine();
            Console.WriteLine("DB tables edit");
            if (!ExecuteScripts(dbConnectionString, "ScriptsSQL.CreateTables."))
                return -1;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }

    }
}
