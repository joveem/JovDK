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

            LocalizationService.instance.SetLanguage(_languageId);
            HidePanel();

        }



        #region View

        public void InstantiateLanguageButtons(Language[] _languages)
        {

            foreach (Language _language in _languages)
            {

                if (_language != null)
                {

                    if (!string.IsNullOrWhiteSpace(_language.languageId))
                    {

                        GameObject _intance = Instantiate(languageButtonPrefab.gameObject, languageButtonsPivot);

                        _intance.GetComponent<Button>().onClick.AddListener(() =>
                        {

                            LanguageButton(_language.languageId);

                        });

                        if (_intance.GetComponent<Image>() != null)
                        {

                            if (_language.sprite != null)
                            {

                                _intance.GetComponent<Image>().sprite = _language.sprite;

                            }
                            else
                            {

                                DebugExtension.DevLogWarning("language SPRITE IS NULL! ( languageId = " + _language.languageId + " )");

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

                    InstantiateLanguageButtons(LocalizationService.instance.languagesList);

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
