using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;


namespace JovDK.LEGACY.Localization
{
    public class LocalizationService
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        // TODO: REVIEW THIS
        // TODO: replace this instance with an static config
        // TODO: config to be used to all LocalizationServices
        // TODO: objects
        public static LocalizationService Instance;

        string _selectedLanguage = "en-us";
        public string SelectedLanguage => _selectedLanguage;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] LanguageSelector _languageSelector;


        [Space(5), Header("[ Configs ]"), Space(10)]

        // bool configs;
        [SerializeField]
        LocalizationLanguage[] _possibleLanguagesList = new LocalizationLanguage[]
        {
            new LocalizationLanguage()
            {
                LanguageId = "en-us",
                CountryFlagSprite = null
            },
            new LocalizationLanguage()
            {
                LanguageId = "pt-br",
                CountryFlagSprite = null
            },
        };

        public LocalizationLanguage[] PossibleLanguagesList => _possibleLanguagesList;

        Dictionary<string, string> _currentLanguageTermsById;




        public LocalizationService()
        {
            if (LocalizationService.Instance == null)
                Instance = this;
            else
            {
                DebugExtension.DevLogWarning("$$> ".ToColor(GoodColors.Red), "One or more Language Managers instaces has been detected!");
                // Destroy(this);
            }
        }

        #region MonoBehaviour
        private void Awake()
        {
            SetupDictionary();
        }

        private void Start()
        {
            if (!PlayerPrefs.HasKey("language"))
            {
                _languageSelector.DoIfNotNull(
                    () => _languageSelector.ShowPanel(),
                    () =>
                    {
                        string debugText =
                            "languageSelector IS NULL!"
                            .ToColor(GoodColors.Orange);

                        DebugExtension.DevLogWarning(debugText);
                    });
            }
            else
                SetLanguage(PlayerPrefs.GetString("language"));
        }
        #endregion MonoBehaviour

        public void SetLanguage(string _language)
        {
            PlayerPrefs.SetString("language", _language);
            _selectedLanguage = _language;
            SetupDictionary();
            ApplyCurrentLanguage();
        }

        public void SetupDictionary()
        {
            _currentLanguageTermsById = new Dictionary<string, string>();

            DebugExtension.DevLog("_selectedLanguage = ", _selectedLanguage);
            TextAsset _textAsset = Resources.Load<TextAsset>("localization-terms-content-by-language-id/" + _selectedLanguage + "/localization-terms");

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

                    _currentLanguageTermsById.Add(_textId, _textValue);
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

            Instance.DoIfNotNull(() =>
                LocalizationService.Instance._currentLanguageTermsById.DoIfNotNull(() =>
                {

                    if (!LocalizationService.Instance._currentLanguageTermsById
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
