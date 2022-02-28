using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JovDK.Debug;

public class LanguageManager : MonoBehaviour
{

    public static LanguageManager instance;

    public Dictionary<string, string> dictionary;

    private void Awake()
    {

        if (LanguageManager.instance == null)
        {

            instance = this;
        }
        else
        {

            DebugExtension.DevLogWarning("one or more Language Managers instaces has been detected!");
            Destroy(this);

        }

        SetupDictionary();

    }

    public void SetupDictionary()
    {

        dictionary = new Dictionary<string, string>();


        string _filePath = "Assets/_Game/Editor/Resources/Locations/pt-br.txt";

        string[] _fileTextLines;


        StreamReader reader = new StreamReader(_filePath);

        _fileTextLines = File.ReadAllLines(_filePath);

        foreach (string _line in _fileTextLines)
        {
            if (_line != null && _line.Length > 1 && _line[0] != '#' && _line.IndexOf('=') != -1)
            {


                string _textId = _line.Substring(0, _line.IndexOf('='));
                string _textValue = _line.Substring(_line.IndexOf('=') + 1, _line.Length - (_line.IndexOf('=') + 1));

                dictionary.Add(_textId, _textValue);

            }

        }

    }

    /*
    private bool IsValidLine(string _line)
    {

        bool _value = false;



        return _value;

    }
    */

    public static string GetTextById(string _textId)
    {

        string _textValue = ".....";

        if (instance != null)
        {

            if (LanguageManager.instance.dictionary != null)
            {

                if (!LanguageManager.instance.dictionary.TryGetValue(_textId, out _textValue))
                {

                    DebugExtension.DevLogWarning("The id \"" + _textId + "\" have NO CORRESPONDING text!");

                }
            }
            else
            {

                DebugExtension.DevLogWarning("Dictionary instance is NULL!");

            }

        }
        else
        {

            DebugExtension.DevLogWarning("LanguageManager instance is NULL!");

        }

        return _textValue;

    }

}
