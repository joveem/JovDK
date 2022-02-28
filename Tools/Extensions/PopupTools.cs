using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PopupTools
{

    public static PopUp SetButtonsText(this PopUp _popup, string _confirmText = null, string _cancelText = null, string _closeText = null)
    {

        _popup.SetButtonsTexts(_confirmText, _cancelText, _closeText);

        return _popup;

    }

    public static PopUp SetConfirmationActions(this PopUp _popup, Action _confirmationAction = null, Action _cancelAction = null, Action _closeAction = null)
    {

        if (_confirmationAction != null)
        {

            _popup.SetConfirmationAction(_confirmationAction);

        }

        if (_cancelAction != null)
        {

            _popup.SetCancelAction(_cancelAction);

        }

        if (_closeAction != null)
        {

            _popup.SetCloseAction(_closeAction);

        }

        return _popup;

    }

    public static PopUp RemovePostConfirmationAction(this PopUp _popup)
    {

        _popup.SetPostConfirmationAction(new Action(()=>{}));

        return _popup;

    }
    public static PopUp RemovePostCancelAction(this PopUp _popup)
    {

        _popup.SetPostCancelAction(new Action(()=>{}));

        return _popup;

    }
    public static PopUp RemovePostCloseAction(this PopUp _popup)
    {

        _popup.SetPostCloseAction(new Action(()=>{}));

        return _popup;

    }

    public static PopUp RemoveAllPostActions(this PopUp _popup)
    {

        _popup.RemovePostConfirmationAction();
        _popup.RemovePostCancelAction();
        _popup.RemovePostCloseAction();

        return _popup;

    }



}
