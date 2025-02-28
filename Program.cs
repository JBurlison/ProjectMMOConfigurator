using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using ProjectMMOConfigurator.Forms;
using ProjectMMOConfigurator.Services;

namespace ProjectMMOConfigurator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);
            
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var mainForm = serviceProvider.GetRequiredService<MainForm>();
                Application.Run(mainForm);
            }
        }
        
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<JsonFileService>();
            services.AddSingleton<ModelFactory>();
            services.AddSingleton<FileSystemService>();
            services.AddSingleton<SkillSearchService>();
            services.AddSingleton<BatchEditService>();
            
            services.AddSingleton<MainForm>();
        }
    }
}