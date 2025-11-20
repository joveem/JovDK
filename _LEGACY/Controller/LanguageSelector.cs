using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using JovDK.Debugging;

namespace JovDK.LEGACY.Localization
{

    public class LanguageSelector : MonoBehaviour
    {

        private bool isShowing = false;
        private bool hasInstantiatedButtons = false;


        [SerializeField]
        GameObject languageSelectorPanel;


        [SerializeField]
        Transform languageButtonsPivot;
        [SerializeField]
        Button languageButtonPrefab;



        private void LanguageButton(string _languageId)
        {

            LocalizationService.Instance.SetLanguage(_languageId);
            HidePanel();

        }



        #region View

        public void InstantiateLanguageButtons(LocalizationLanguage[] _languages)
        {

            foreach (LocalizationLanguage _language in _languages)
            {

                if (_language != null)
                {

                    if (!string.IsNullOrWhiteSpace(_language.LanguageId))
                    {

                        GameObject _intance = Instantiate(languageButtonPrefab.gameObject, languageButtonsPivot);

                        _intance.GetComponent<Button>().onClick.AddListener(() =>
                        {

                            LanguageButton(_language.LanguageId);

                        });

                        if (_intance.GetComponent<Image>() != null)
                        {

                            if (_language.CountryFlagSprite != null)
                            {

                                _intance.GetComponent<Image>().sprite = _language.CountryFlagSprite;

                            }
                            else
                            {

                                DebugExtension.DevLogWarning("language SPRITE IS NULL! ( languageId = " + _language.LanguageId + " )");

                            }


                        }
                        else
                        {

                            DebugExtension.DevLogWarning("languageButtonPrefab have NO IMAGE COMPONENT");

                        }

                    }
                    else
                    {

                        DebugExtension.DevLogError("Some language have an INVALIDE LANGUAGE ID!");

                    }

                }

            }

            hasInstantiatedButtons = true;

        }

        public void ShowPanel()
        {

            isShowing = true;

            if (languageSelectorPanel != null)
            {

                languageSelectorPanel.SetActive(true);

                if (!hasInstantiatedButtons)
                {

                    InstantiateLanguageButtons(LocalizationService.Instance.PossibleLanguagesList);

                }

            }
            else
            {

                DebugExtension.DevLogError("languageSelectorPanel IS NULL!");

            }

        }

        public void HidePanel()
        {

            isShowing = false;

            if (languageSelectorPanel != null)
            {

                languageSelectorPanel.SetActive(false);

            }
            else
            {

                DebugExtension.DevLogError("languageSelectorPanel IS NULL!");

            }

        }

        #endregion
    }
}
