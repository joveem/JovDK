using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;

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

}

public static class GoodCollors
{

    static public string red = "#e00";
    static public string orange = "#f61";
    static public string yellow = "#aa0";
    static public string green = "#0a0";
    static public string blue = "#00f";
    static public string pink = "#f0f";

}
