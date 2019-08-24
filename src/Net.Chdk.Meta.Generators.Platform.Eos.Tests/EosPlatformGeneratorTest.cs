using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;

namespace Net.Chdk.Meta.Generators.Platform.Eos.Tests
{
    [TestClass]
    public class EosPlatformGeneratorTest
    {
        private IPlatformGenerator platformGenerator;

        public EosPlatformGeneratorTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddPlatformGenerator()
                .AddEosPlatformGenerator()
                .BuildServiceProvider();
            platformGenerator = serviceProvider.GetService<IPlatformGenerator>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullModels()
        {
            var result = platformGenerator.GetPlatform(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEmptyModels()
        {
            var result = platformGenerator.GetPlatform(new string[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNullModel()
        {
            var result = platformGenerator.GetPlatform(new string[] { null });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEmptyModel()
        {
            var result = platformGenerator.GetPlatform(new[] { string.Empty });
        }
    }
}
