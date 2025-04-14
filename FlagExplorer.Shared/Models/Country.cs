using System.Collections.Generic;
using System.Linq;

namespace FlagExplorer.Shared.Models
{
    public class Country
    {
        public NameData Name { get; set; }
        public List<string> Capital { get; set; }
        public int Population { get; set; }
        public FlagsData Flags { get; set; }

        public string CommonName => Name?.Common;
        public string CapitalCity => Capital?.FirstOrDefault();
        public string FlagUrl => Flags?.Png;
    }

    public class NameData
    {
        public string Common { get; set; }

        // Implicit conversion from string to NameData
        public static implicit operator NameData(string value)
        {
            return new NameData { Common = value };
        }
    }


    public class FlagsData
    {
        public string Png { get; set; }

        public static implicit operator FlagsData(string v)
        {
            throw new NotImplementedException();
        }
    }
}
