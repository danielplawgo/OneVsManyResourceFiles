using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace OneVsManyResourceFiles
{
    public class Benchmarks
    {
        [Benchmark]
        public void UsingResources()
        {
            var test = Resources.One;
        }

        [Benchmark]
        public void UsingResourceManager()
        {
            var resourceManager = Resources.ResourceManager;

            var test = resourceManager.GetString("One", CultureInfo.InvariantCulture);
        }

        [Benchmark]
        public void CreatingResourceManager()
        {
            var resourceManager =
                    new ResourceManager("OneVsManyResourceFiles.Resources", typeof(Resources).Assembly);
        }

        [Benchmark]
        public void CreatingResourceManagerAndGetOneString()
        {
            var resourceManager =
                    new ResourceManager("OneVsManyResourceFiles.Resources", typeof(Resources).Assembly);

            var test = resourceManager.GetString("One", CultureInfo.InvariantCulture);
        }

        [Benchmark]
        public void CreatingResourceManagerAndGetThousandString()
        {
            var resourceManager =
                new ResourceManager("OneVsManyResourceFiles.Resources", typeof(Resources).Assembly);
            for (int i = 0; i < 1000; i++)
            {
                var test = resourceManager.GetString("One", CultureInfo.InvariantCulture);
            }
        }

        [Benchmark]
        public void ReadManyLanguage()
        {
            var resourceManager =
                new ResourceManager("OneVsManyResourceFiles.Resources", typeof(Resources).Assembly);

            var test = resourceManager.GetString("One", CultureInfo.GetCultureInfo("pl-PL"));
            test = resourceManager.GetString("One", CultureInfo.GetCultureInfo("de-DE"));
            test = resourceManager.GetString("One", CultureInfo.GetCultureInfo("en-GB"));
        }
    }
}
