using System.Text;

namespace TurbineJobMVC.Extensions
{
    public static class Extensions
    {
        public static string ConvertToWesternArbicNumerals(string input)
        {
            var result = new StringBuilder(input.Length);
            foreach (var c in input)
                if (char.IsNumber(c))
                    result.Append(char.GetNumericValue(c));
                else
                    result.Append(c);

            return result.ToString();
        }
    }
}