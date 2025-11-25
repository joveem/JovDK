// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;
using SystemRandom = System.Random;
using UnityRandom = UnityEngine.Random;

// third
// using DG.Tweening;
// using R3;
// using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Utils
{
    public static class StringUtils
    {
        /// <summary>
        /// Removes accents and diacritics from the input string.
        /// </summary>
        public static string RemoveAccents(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Normalize to FormD: decomposes characters into base + accent
            string normalized = input.Normalize(NormalizationForm.FormD);

            // Filter out non-spacing marks (accents, cedilla, etc.)
            StringBuilder result = new StringBuilder();
            foreach (char c in normalized)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(c);
                if (category != UnicodeCategory.NonSpacingMark)
                    result.Append(c);
            }

            // Return the cleaned string in FormC (composed form)
            return result.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Removes all characters from the input string except letters and digits.
        /// </summary>
        public static string RemoveNonAlphanumericCharacters(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return Regex.Replace(input, @"[^a-zA-Z0-9]", "");
        }

        /// <summary>
        /// Removes all characters from the input string except digits.
        /// </summary>
        public static string RemoveNonNumericCharacters(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return Regex.Replace(input, @"[^0-9]", "");
        }
    }
}
