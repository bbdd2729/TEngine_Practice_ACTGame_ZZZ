/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer
{
    public abstract class SearchableItem
    {
        public static string GetPattern(string str)
        {
            string search = str;

            TextInfo textInfo = Culture.textInfo;
            StringBuilder builder = StaticStringBuilder.Start();

            bool lastWhite = false;

            for (int i = 0; i < search.Length; i++)
            {
                char c = search[i];
                if (c == ' ' || c == '\t' || c == '\n')
                {
                    if (!lastWhite && builder.Length > 0)
                    {
                        builder.Append(' ');
                        lastWhite = true;
                    }
                }
                else
                {
                    builder.Append(textInfo.ToUpper(c));
                    lastWhite = false;
                }
            }

            if (lastWhite) builder.Length -= 1;

            return builder.ToString();
        }

        public static string GetPattern(string str, out string assetType)
        {
            assetType = string.Empty;
            string search = str;

            TextInfo textInfo = Culture.textInfo;

            Match match = Regex.Match(search, @"(:)(\w*)");
            if (match.Success)
            {
                assetType = textInfo.ToUpper(match.Groups[2].Value);
                if (assetType == "PREFAB") assetType = "GAMEOBJECT";
                search = Regex.Replace(search, @"(:)(\w*)", "");
            }

            StringBuilder builder = StaticStringBuilder.Start();

            bool lastWhite = false;

            for (int i = 0; i < search.Length; i++)
            {
                char c = search[i];
                if (c == ' ' || c == '\t' || c == '\n')
                {
                    if (!lastWhite && builder.Length > 0)
                    {
                        builder.Append(' ');
                        lastWhite = true;
                    }
                }
                else
                {
                    builder.Append(textInfo.ToUpper(c));
                    lastWhite = false;
                }
            }

            if (lastWhite) builder.Length -= 1;

            return builder.ToString();
        }

        protected abstract int GetSearchCount();

        protected abstract string GetSearchString(int index);

        public static int GetMatchIndex(string pattern, params string[] values)
        {
            if (values == null || values.Length == 0) return -1;
            if (string.IsNullOrEmpty(pattern)) return -1;

            for (int i = 0; i < values.Length; i++)
            {
                if (MatchInternal(pattern, values[i])) return i;
            }

            return -1;
        }

        public virtual bool Match(string pattern)
        {
            if (string.IsNullOrEmpty(pattern)) return true;

            for (int i = 0; i < GetSearchCount(); i++)
            {
                if (Match(pattern, GetSearchString(i))) return true;
            }

            return false;
        }

        public static bool Match(string pattern, params string[] values)
        {
            return GetMatchIndex(pattern, values) != -1;
        }

        protected static bool MatchInternal(string pattern, string str)
        {
            if (string.IsNullOrEmpty(pattern) || string.IsNullOrEmpty(str)) return false;
            
            ReadOnlySpan<char> strSpan = str.AsSpan();
            ReadOnlySpan<char> patternSpan = pattern.AsSpan();
            int l1 = strSpan.Length;
            int l2 = patternSpan.Length;
            char firstChar = patternSpan[0];

            if (l2 == 1)
            {
                return str.Contains(firstChar, StringComparison.OrdinalIgnoreCase);
            }
        
            char secondChar = patternSpan[1];

            for (int i = 0; i <= l1 - l2; i++)
            {
                if (char.ToUpperInvariant(strSpan[i]) != firstChar) continue;
            
                /*
                 * 1 - lower no skip or upper with skip
                 * 2 - search upper with skip
                 */
                int searchRule = 1;
                int j = 1;
                char pc = secondChar;

                for (int k = i + 1; k < l1; k++)
                {
                    char sc = strSpan[k];

                    if (!char.IsLower(sc))
                    {
                        if (sc == pc)
                        {
                            searchRule = 1;
                            if (++j == l2) return true;
                            pc = patternSpan[j];
                            continue;
                        }
                    }
                    else if (searchRule == 1 && char.ToUpperInvariant(sc) == pc)
                    {
                        if (++j == l2) return true;
                        pc = patternSpan[j];
                        continue;
                    }
                
                    searchRule = 2;
                }
            }

            return false;
        }
    }
}