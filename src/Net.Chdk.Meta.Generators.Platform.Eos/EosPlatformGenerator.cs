using System;
using System.Collections.Generic;
using System.Linq;

namespace Net.Chdk.Meta.Generators.Platform.Eos
{
    sealed class EosPlatformGenerator : InnerPlatformGenerator
    {
        public override string GetPlatform(string[] models)
        {
            if (models[0].Contains("Rebel"))
            {
                if (models.Length > 1)
                    return base.GetPlatform(new[] { $"EOS {models[1]}" });
                return null;
            }
            return base.GetPlatform(models);
        }

        protected override IEnumerable<string> PreGenerate(string source)
        {
            var split = source.Split(' ');
            if (!Keyword.Equals(split[0]))
                return null;

            if (split[1].Equals("M"))
                return new[] { "EOSM" };

            if (split[1].StartsWith("M"))
                return null;

            var index = Array.IndexOf(split, "Mark");
            if (index > 0)
            {
                var m = RomanToInteger(split[index + 1]);
                return split
                    .Take(index)
                    .Concat(new[]{ m.ToString() })
                    .Skip(1);
            }

            if (split[1].Equals("5D"))
                return new[] { "5DC" };

            return split.Skip(1);
        }

        protected override IEnumerable<string> Process(IEnumerable<string> split)
        {
            return split;
        }

        protected override string PostProcess(IEnumerable<string> split)
        {
            return string.Join(string.Empty, split);
        }

        protected override string Keyword => "EOS";

        private static int RomanToInteger(string roman)
        {
            switch (roman)
            {
                case "I":
                    return 1;
                case "II":
                    return 2;
                case "III":
                    return 3;
                case "IV":
                    return 4;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
