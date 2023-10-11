using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;


namespace JovDK.LEGACY.Localization
{
    public class LocalizationService
    {

        // TODO: REVIEW THIS
        // TODO: replace this instance with an static confing
        // TODO: confing to be used to all LocalizationServices
        // TODO: objects
        public static LocalizationService instance;

        public LocalizationService()
        {

            if (LocalizationService.instance == null)
            {

                instance = this;
            }
            else
            {

                DebugExtension.DevLogWarning("one or more Language Managers instaces has been detected!");
                // Destroy(this);

            }

        }


        [Space(5), Header("[ Dependencies ]"), Space(10)]

        bool dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        bool state;


        [Space(5), Header("[ Parts ]"), Space(10)]

        bool parts;


        [Space(5), Header("[ Configs ]"), Space(10)]

        bool configs;



        public string selectedLanguage = "en-us";

        public Dictionary<string, string> dictionary;

        public Language[] languagesList;

        [SerializeField]
        private LanguageSelector languageSelector;

        private void Awake()
        {

            SetupDictionary();

        }

        private void Start()
        {

            if (!PlayerPrefs.HasKey("language"))
            {

                languageSelector.DoIfNotNull(
                    () => languageSelector.ShowPanel(),
                    () =>
                    {

                        string debugText =
                            "languageSelector IS NULL!"
                            .ToColor(GoodCollors.orange);

                        DebugExtension.DevLogWarning(debugText);

                    });


            }
            else
            {

                SetLanguage(PlayerPrefs.GetString("language"));

            }

        }

        public void SetLanguage(string _language)
        {

            PlayerPrefs.SetString("language", _language);
            selectedLanguage = _language;
            SetupDictionary();
            ApplyCurrentLanguage();

        }

        public void SetupDictionary()
        {

            dictionary = new Dictionary<string, string>();

            DebugExtension.DevLog("selectedLanguage = " + selectedLanguage);
            TextAsset _textAsset = Resources.Load<TextAsset>("Locations/" + selectedLanguage);

            string[] _fileTextLines = _textAsset.text.Split(
                new string[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            );

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

        private void ApplyCurrentLanguage()
        {

            MultiLanguageText[] multiLanguageTextList =
                Resources.FindObjectsOfTypeAll(typeof(MultiLanguageText)) as MultiLanguageText[];

            foreach (MultiLanguageText _text in multiLanguageTextList)
            {

                if (_text != null)
                    _text.ApplyText();

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

            instance.DoIfNotNull(() =>
                LocalizationService.instance.dictionary.DoIfNotNull(() =>
                {

                    if (!LocalizationService.instance.dictionary
                        .TryGetValue(_textId, out _textValue))
                    {

                        string debugText =
                            "The id \"" + _textId + "\"" +
                            " have NO CORRESPONDING text!";

                        DebugExtension.DevLogWarning(debugText);

                    }

                }));


            // uncomment to debug translations on Development versions
            /*
    #if UNITY_EDITOR || DEVELOPMENT_BUILD
            _textValue = "<color=#f0f>?</color>" + _textValue;
    #endif
            */

            return _textValue;

        }

    }
}
