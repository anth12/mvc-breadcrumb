using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Mvc.Breadcrumb.Utilities
{
    internal static class StringExtensions
    {
        internal static bool IsNotBlank(this string input)
        {
            return string.IsNullOrWhiteSpace(input) == false;
        }

        internal static bool IsBlank(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// Convert string to Readable capitalised text t e.g. 'Some-stringHere' => 'Some String Here' || 'Some-string_here' => 'Some String Here'
        /// </summary>
        /// <param name="value">Text to Camel hump format</param>
        /// <returns>Camel hump formatted string</returns>
        internal static string ToDisplayText(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";

            var words = CaseConvertSplitter(value);
            return string.Join(" ", words.Select(Capitalise));
        }

        #region private helper methods

        private static string Capitalise(string word)
        {
            if (string.IsNullOrEmpty(word))
                return word;
            if (word.Length == 1)
                return word.ToUpper();
            return word.Substring(0, 1).ToUpper() + word.Substring(1, word.Length - 1).ToLower();
        }
        private static IEnumerable<string> CaseConvertSplitter(string source)
        {
            //ignore apostrophes
            source = source.Replace("'", "");

            //Matches any none alphabetical charactor before a alphabetical charactor and lowercase before an uppercase
            var splitRegex = new Regex("((?<=[a-z])([A-Z]))|((?<=\\W)([a-z]|[A-Z]))|((?<=_)([a-z]|[A-Z]))");
            var replaceRegex = new Regex("\\W|_");

            var separatedString = splitRegex.Replace(source, " $0");

            return replaceRegex.Replace(separatedString, " ")
                    .Split(' ')
                    .Select(x => x.Trim().ToLower())
                    .Where(x => !string.IsNullOrWhiteSpace(x));
        }

        #endregion
    }
}
