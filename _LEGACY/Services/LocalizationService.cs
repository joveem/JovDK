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
    public class LocalizationService : MonoBehaviour
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
        // string _selectedLanguage = "pt-br";
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

        [SerializeField] bool _overrideDefaultSelectedLanguage = false;
        [SerializeField] string _defaultSelectedLanguage = "en-us";




        public LocalizationService()
        {
            // if (LocalizationService.Instance == null)
            //     Instance = this;
            // else
            // {
            //     DebugExtension.DevLogWarning("$$> ".ToColor(GoodColors.Red), "One or more Language Managers instaces has been detected!");
            //     // Destroy(this);
            // }
        }

        #region MonoBehaviour
        private void Awake()
        {
            if (LocalizationService.Instance == null)
                Instance = this;
            else
            {
                DebugExtension.DevLogWarning("$$> ".ToColor(GoodColors.Red), "One or more Language Managers instaces has been detected!");
                // Destroy(this);
            }

            if (_overrideDefaultSelectedLanguage)
                _selectedLanguage = _defaultSelectedLanguage;

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

        public static string GetTextById(string termId, string defaultValue = ".....")
        {
            string termValue = defaultValue;

            bool success = false;

            Instance.DoIfNotNull(() =>
                Instance._currentLanguageTermsById.DoIfNotNull(() =>
                {
                    success = Instance._currentLanguageTermsById.TryGetValue(termId, out string foundTermValue);

                    if (success)
                        termValue = foundTermValue;
                    else
                    {
                        string debugTermId = termId ?? ">NULL<";
                        DebugExtension.DevLogWarning(
                            "$$> ".ToColor(GoodColors.Red),
                            "Term not found! ", "\n",
                            "termId = ", debugTermId, "\n",
                            "_currentLanguageTermsById.Count = ", Instance._currentLanguageTermsById.Count.ToString(), "\n",
                            "");
                    }
                }));

            // uncomment to debug translations on Development versions
            /*
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (success)
            {
                termValue = "<color=#00f>T</color>" + termValue + "<color=#00f>T</color>";
            }
            else
            {
                termValue = "<color=#f0f>E</color>" + termValue + "<color=#f0f>E</color>";
            }
#endif
            */

            return termValue;
        }

    }
}
