using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JovDK.Debug;

public class VersionController : MonoBehaviour
{

    public static VersionController instance;

    public int gameVersionCode = 10;

    public string _testCode = "0";
    public bool canLoadGame = false;


    private void Awake()
    {

        if (VersionController.instance == null)
        {

            instance = this;
        }
        else
        {

            DebugExtension.DevLogWarning("one or more Version Controllers instaces has been detected!");
            Destroy(this);

        }

    }

    public void SearchForUpdates()
    {
        DebugExtension.DevLog("Searching for updates...");

        PanelsManager.instance.ShowLoadingPanel();

        ServerManager.instance.GetVersion(gameVersionCode, (_code, _response) =>
        {

            //DebugExtension.DevLog("updates response = " + _response);

            switch (_code)
            {

                case 0:
                    {
                        ShowCanotFindVersion();
                        break;
                    }
                case 200:
                    {
                        switch (_response.status)
                        {

                            case Models.versionStatus.Updated:
                                {
                                    // current version is updated!
                                    DebugExtension.DevLog("game updated!".ToColor(GoodCollors.green));
                                    canLoadGame = true;
                                    break;
                                }
                            case Models.versionStatus.Outdated:
                                {
                                    // current version is outdated!
                                    ShowUpdateWarning();
                                    break;
                                }
                            case Models.versionStatus.Blocked:
                                {
                                    // current version is blocked!


                                    canLoadGame = false;
                                    ShowBlockedWarning();
                                    break;
                                }

                        }
                        break;

                    }
                default:
                    {
                        break;
                    }
            }

            /*

                if (_response != null)
                {

                    switch (_response)
                    {

                        case "updated":
                            {
                                DebugExtension.DevLog("GAME UPDATED!");
                                canLoadGame = true;
                                break;

                            }
                        case "outdated":
                            {
                                PanelsManager.instance.ShowConfirmationPopup(null, LanguageManager.GetTextById("Update.Available.Description")).SetConfirmationActions(() => { DebugExtension.DevLog("OPENNING STORE..."); }, () => { canLoadGame = true; }, null).SetButtonsTexts(LanguageManager.GetTextById("Update.Available.Confirmation"), LanguageManager.GetTextById("Update.Available.Cancel"), null);
                                break;
                            }
                        case "blocked":
                            {
                                canLoadGame = false;
                                PanelsManager.instance.ShowInformationPopup(null, LanguageManager.GetTextById("Update.Block.Description")).RemoveAllPostActions().SetConfirmationActions(() => { DebugExtension.DevLog("OPENNING STORE..."); }, null, null).SetButtonsTexts(LanguageManager.GetTextById("Update.Block.Confirmation"), null, null);
                                break;
                            }

                    }

                }
            */


        });

    }

    private void ShowCanotFindVersion()
    {

        PanelsManager.instance.ShowInformationPopup(null, LanguageManager.GetTextById("Update.CanotFind.Description")).SetConfirmationActions(() => { SearchForUpdates(); }, null, null).SetButtonsTexts(LanguageManager.GetTextById("Update.CanotFind.Confirmation"), null, null);

    }

    private void ShowUpdateWarning()
    {

        PanelsManager.instance.ShowConfirmationPopup(null, LanguageManager.GetTextById("Update.Available.Description")).RemovePostConfirmationAction().SetConfirmationActions(() => { DebugExtension.DevLog("OPENNING STORE..."); }, () => { canLoadGame = true; }, null).SetButtonsTexts(LanguageManager.GetTextById("Update.Available.Confirmation"), LanguageManager.GetTextById("Update.Available.Cancel"), null);

    }

    private void ShowBlockedWarning()
    {

        PanelsManager.instance.ShowInformationPopup(null, LanguageManager.GetTextById("Update.Block.Description")).RemoveAllPostActions().SetConfirmationActions(() => { DebugExtension.DevLog("OPENNING STORE..."); }, null, null).SetButtonsTexts(LanguageManager.GetTextById("Update.Block.Confirmation"), null, null);

    }

}
