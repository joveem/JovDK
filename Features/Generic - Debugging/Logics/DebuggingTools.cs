// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using JovDK.SerializingTools.Json;
using UnityEngine;
using UnityEngine.UI;

// third
// ...

// from company
// ...

// from project
// ...


namespace JovDK.Debugging
{
    public static partial class DebuggingTools
    {
        public static string TextIfIsNull(this object _object, string _textIfNull, string _textIfNotNull = "")
        {
            return _object == null ? _textIfNull : _textIfNotNull;
        }

        public static string TextIfIsNullOrEmpty(this string _text, string _textIfNull, string _textIfNotNull = "")
        {
            return string.IsNullOrEmpty(_text) ? _textIfNull : _textIfNotNull;
        }

        public static string TextIfIsNullOrWhiteSpace(this string _text, string _textIfNull, string _textIfNotNull = "")
        {
            return string.IsNullOrWhiteSpace(_text) ? _textIfNull : _textIfNotNull;
        }

        public static string ToColor(this string baseText, string colorHexCode, bool nullOrWhiteSpaceIsExpected = false)
        {
            if (!string.IsNullOrWhiteSpace(colorHexCode))
            {
                StringBuilder baseStringBuilder = new StringBuilder();

                baseStringBuilder.Append("<color=");
                baseStringBuilder.Append(colorHexCode);
                baseStringBuilder.Append(">");
                baseStringBuilder.Append(baseText);
                baseStringBuilder.Append("</color>");

                return baseStringBuilder.ToString();
            }
            else
            {
                if (!nullOrWhiteSpaceIsExpected)
                {
                    DebugExtension.DevLogWarning(
                        "$> ".ToColor(GoodColors.Red), "\n",
                        "colorHexCode IsNullOrWhiteSpace!",
                        "colorHexCode = ", colorHexCode.SerializeObjectToJSON(), "\n",
                        "");
                }

                return baseText;
            }
        }

        public static void AppendWithColor(this StringBuilder baseStringBuilder, string baseText, string colorHexCode)
        {
            if (!string.IsNullOrWhiteSpace(colorHexCode))
            {
                baseStringBuilder.Append("<color=");
                baseStringBuilder.Append(colorHexCode);
                baseStringBuilder.Append(">");
                baseStringBuilder.Append(baseText);
                baseStringBuilder.Append("</color>");
            }
            else
                baseStringBuilder.Append(baseText);
        }

        public static void AppendMultiples(this StringBuilder baseStringBuilder, params string[] textList)
        {
            foreach (var baseText in textList)
            {
                if (baseText is not null)
                    baseStringBuilder.Append(baseText);
                else
                    baseStringBuilder.Append("<NULL>");
            }
        }

        public static void AppendType<T>(this StringBuilder baseStringBuilder, T baseObjectValue)
        {
            baseStringBuilder.Append(baseObjectValue.ToTypeText());
        }

        public static string ToTypeText<T>(this T baseObject)
        {
            return typeof(T).Name;
        }

        public static string ToShortId(
            this string baseText,
            bool ignoreParentheses = false)
        {
            StringBuilder baseStringBuilder = new StringBuilder();

            bool isShortable =
                !string.IsNullOrWhiteSpace(baseText) &&
                baseText.Length > 4;

            if (isShortable)
            {
                string startParenteses = ignoreParentheses ? "" : "(";
                string endParenteses = ignoreParentheses ? "" : ")";

                baseStringBuilder.Append(startParenteses);
                baseStringBuilder.Append("...");
                baseStringBuilder.Append(baseText.Substring(baseText.Length - 4));
                baseStringBuilder.Append(endParenteses);
            }
            else
                baseStringBuilder.Append(baseText);

            return baseStringBuilder.ToString();
        }

        public static string ToNestedText(
            this string _text,
            bool isSingleItem = false)
        {
            StringBuilder baseStringBuilder = new StringBuilder();

            string[] breakLinesList = new string[] { "\r\n", "\r", "\n" };
            string[] textLines = _text.Split(breakLinesList, StringSplitOptions.RemoveEmptyEntries);

            string firstLineText = isSingleItem ? "└─ " : "├─ ";
            string middleLineText = isSingleItem ? "   " : "├─ ";
            string lastLineText = isSingleItem ? "   " : "└─ ";

            for (int i = 0; i < textLines.Length; i++)
            {
                string textLine = textLines[i];

                bool isSingleLine = (textLines.Length == 1);
                bool isFirstInList = (i == 0);
                bool isLastInList = (i == textLines.Length - 1);

                if (!isSingleLine)
                {
                    if (isLastInList)
                    {
                        baseStringBuilder.Append(lastLineText);
                        baseStringBuilder.Append(textLine);
                        baseStringBuilder.Append("\n");
                    }
                    else if (isFirstInList)
                    {
                        baseStringBuilder.Append(firstLineText);
                        baseStringBuilder.Append(textLine);
                        baseStringBuilder.Append("\n");
                    }
                    else
                    {
                        baseStringBuilder.Append(middleLineText);
                        baseStringBuilder.Append(textLine);
                        baseStringBuilder.Append("\n");
                    }
                }
                // is single line
                else
                {
                    baseStringBuilder.Append("└─ ");
                    baseStringBuilder.Append(textLine);
                    baseStringBuilder.Append("\n");
                }
            }

            return baseStringBuilder.ToString();
        }
    }

    [Obsolete("This class need to be replaced with \"GoodColors\" class")]
    public static class GoodCollors
    {
        static public string red = "#e00";
        static public string orange = "#f61";
        static public string yellow = "#aa0";
        static public string green = "#0a0";
        static public string blue = "#00f";
        static public string pink = "#f0f";
    }

    public static class GoodColors
    {
        static public string Red = "#e00";
        static public string Orange = "#f61";
        static public string Yellow = "#aa0";
        static public string Green = "#0a0";
        static public string Blue = "#00f";
        static public string Pink = "#f0f";
    }
}
