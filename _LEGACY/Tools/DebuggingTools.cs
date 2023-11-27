// system / unity
using System;

// third
// ...

// from company
// ...

// from project
// ...


namespace JovDK.Debug
{
    public static class DebuggingTools
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

        public static string ToColor(this string _text, string _colorCode)
        {

            if (!string.IsNullOrWhiteSpace(_colorCode))
                return "<color=" + _colorCode + ">" + _text + "</color>";
            else
                return _text;

        }

        public static string ToShortId(
            this string _text,
            bool ignoreParentheses = false)
        {

            bool isShortable =
                !string.IsNullOrWhiteSpace(_text) &&
                _text.Length > 4;

            string startParenteses = ignoreParentheses ? "" : "(";
            string endParenteses = ignoreParentheses ? "" : ")";

            if (isShortable)
                return startParenteses + "..." + _text.Substring(_text.Length - 4) + endParenteses;
            else
                return _text;

        }

        public static string ToNestedText(
            this string _text,
            bool isSingleItem = false)
        {
            string value = "";

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
                        value += lastLineText + textLine + "\n";
                    else if (isFirstInList)
                        value += firstLineText + textLine + "\n";
                    else
                        value += middleLineText + textLine + "\n";
                }
                // is single line
                else
                    value += "└─ " + textLine + "\n";
            }

            return value;
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
