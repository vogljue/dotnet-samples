// Service class in C# (Tutorial).
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Carservice.Utilities;

namespace Carservice.Services
{
    /// <summary>
    /// Represents the service interface..
    /// </summary>
    public interface IFooService : IDisposable
    {
        void DoFileService();
        void DoRegexService();

        void DoTupleService();
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

        public void Dispose()
        {
            _logger.LogInformation("Dispose on FooService.");
        }

        public void DoFileService()
        {
            _logger.LogInformation($"Doing some File Utils tests on server {_server}!");
            _logger.LogInformation("Current working directory: {0}", FileSystemUtils.GetCurrentDirectory());
            try {
                _logger.LogInformation(FileSystemUtils.LoadHostsFile());
                _logger.LogInformation(UvgtGeneratorUtils.BuildUvgtDataSet());
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

        public void DoTupleService()
        {
            // Named Tuples
            List<int> zahlen = new List<int>() {2,4,6,8,10};
            var tupleResult = Bereich(zahlen);
            int minimum = tupleResult.min;

            (int min, int maximum) = Bereich(zahlen);
            Console.WriteLine(maximum);
            _logger.LogInformation("Bereich Zahlen max: {0}", maximum);
        
            // Pattern Matching
            object[] muster = { null, "Lukas" };
            foreach (var item in muster)
            {
                IsMuster(item);
            }

            DateTime now = DateTime.Now;
            Console.WriteLine($"{now} {PatternMatchingExtended(now)}");

            // Indices und Bereiche
            string[] vornamen = { "Jürgen", "Christoph", "Lukas", "Anton", "Dirk", "Gregor", "Luis", "Stefan" };
        
            Console.WriteLine($"Erster Vorname: {vornamen[0]}");
            Console.WriteLine($"Letzter Vorname: {vornamen[^1]}");
            Console.WriteLine($"Vorletzter Vorname: {vornamen[^2]}");
            
            Range r = 2..4;
            var namen = vornamen[r];
            foreach (var item in namen)
            {
                Console.WriteLine($"Range Vorname: {item}");
            }

        }
        private (int min, int max) Bereich(List<int> zahlen)
        {
            int min = zahlen.Min();
            int max = zahlen.Max();
            return (min, max);
        }

        private void IsMuster(object item)
        {
            switch (item)
            {
                case null:
                    Console.WriteLine("Nullwert");
                    break;
                case string s when s == "Lukas":
                    Console.WriteLine("String mit Lukas");
                    break;
            }
        }

        private string PatternMatchingExtended(DateTime now) =>
            now.DayOfWeek switch
            {
                DayOfWeek.Sunday => "Wochenende",
                DayOfWeek.Saturday => "Wochenende",
                _ => "Arbeitstag"
            };
    }
}