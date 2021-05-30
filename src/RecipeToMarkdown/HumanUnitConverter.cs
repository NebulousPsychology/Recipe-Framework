using System;
using System.Collections.Generic;

namespace RecipeToMarkdown
{
    enum ConvertableVolumeUnit
    {
        Cup_us, Tbs_us, tsp_us, Tbs_uk, tsp_uk, mL
    }
    /// <summary>
    /// A tool for converting decimal numbers of a given cooking unit into a mixed number of a 'reasonable' unit
    /// </summary>
    /// <example>
    /// 100 Tbs = 6 1/4 cups
    /// </example>
    public class HumanUnitConverter
    {
        static Dictionary<double, string> FractionLookup = new Dictionary<double, string>(){
                {0.25, "¼"},
                {0.5, "½"},
                {0.75, "¾"},
                {1.0/3.0, "⅓"},
                {2.0/3.0, "⅔"},
                {1.0/5.0, "⅕"},
                {2.0/5.0, "⅖"},
                {3.0/5.0, "⅗"},
                {4.0/5.0, "⅘"},
            };

        static string ToMixedNumber(double value)
        {
            int whole = (int)Math.Floor(value);
            double decPart = value - whole;
            // fixme: floating point math
            bool mixable = FractionLookup.ContainsKey(decPart);
            string fraction = mixable ? FractionLookup[decPart] : null;
            return mixable ? $"{whole} {fraction}" : value.ToString();
        }
        static Tuple<string, string> Convert(double qty, string unit)
        {
            return new Tuple<string, string>(ToMixedNumber(qty), unit);
        }
    }
}