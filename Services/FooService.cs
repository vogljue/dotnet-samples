// Service class in C# (Tutorial).
using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Carservice.Utilities;

namespace Carservice.Services
{
    /// <summary>
    /// Represents the service interface..
    /// </summary>
    public interface IFooService  
    {
        void DoFileService();
        void DoRegexService();
    }    

    /// <summary>
    /// Represents the service configuration options.
    /// </summary>
    public class FooServiceOptions
    {
        public string Server { get; set; }
    }
    
    /// <summary>
    /// Represents the service class.
    /// </summary>
    public class FooService : IFooService  
    {
        private readonly ILogger<FooService> _logger;
        private readonly string _server;
 
        public FooService(ILoggerFactory loggerFactory, IOptions<FooServiceOptions> options)
        {
            _logger = loggerFactory.CreateLogger<FooService>();
            _server = options.Value.Server;
        }

        public void DoFileService()
        {
            _logger.LogInformation($"Doing some File Utils tests on server {_server}!");
            _logger.LogInformation("Current working directory: {0}", FileSystemUtils.GetCurrentDirectory());
            try {
                _logger.LogInformation(FileSystemUtils.LoadHostsFile());
                _logger.LogInformation(UvgtGeneratorUtils.BuildUvgtDataSet());
                
                /*
                string gts1 = "12345678-12";
                string gts2 = "12345678-AB";
                _logger.LogInformation("ASCII Compare: {0}", gts1.CompareTo(gts2));
                
                string gts1e = FileSystemUtils.ConvertAsciiToEbcdic(gts1);
                string gts2e = FileSystemUtils.ConvertAsciiToEbcdic(gts2);
                _logger.LogInformation("EBCDIC Compare: {0}", gts1e.CompareTo(gts2e));
                */
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "LoadHostsFile");
            }
        }

        public void DoRegexService()
        {
            _logger.LogInformation($"Doing some Regex tests on server {_server}!");

            // Regular expressions
            string pattern = "K4B.(?<lauf>W[1-4]{2}).(?<dsn>.*).T.*";
            string[] names = { "K4B.W11.PRUEFLST,T171001",
                               "K4B.W11.LASER0,T171001",
                               "K4B.W10.PRUEFLST,T171001",
                               "K4B.W21.PRUEFLST,T171001"
                             };
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            foreach (string name in names)
            {
                Match match = regex.Match(name);
                if (match.Success)
                {
                    GroupCollection groups = match.Groups;
                    _logger.LogInformation("Regex match {0}", match.Value);
                    _logger.LogInformation("Regex group lauf {0} dsn {1}", groups["lauf"].Value, groups["dsn"].Value);
                }
            }
        }
    }
}