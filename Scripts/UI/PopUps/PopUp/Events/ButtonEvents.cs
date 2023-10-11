// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using TMPro;

// from company
using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

// from project
// ...

namespace JovDK.UI.PopUp
{
    public partial class PopUp : BasePanel
    {
        void SetupButtons()
        {
            _positiveButton.SetOnClickIfNotNull(PositiveButton);
            _negativeButton.SetOnClickIfNotNull(NegativeButton);
            _closeButton.SetOnClickIfNotNull(CloseButton);
        }

        void PositiveButton()
        {
            _positiveCallback?.Invoke();
            _postPositiveCallback?.Invoke();
        }

        void NegativeButton()
        {
            _negativeCallback?.Invoke();
            _postNegativeCallback?.Invoke();
        }

        void CloseButton()
        {
            _closeCallback?.Invoke();
            _postCloseCallback?.Invoke();
        }
    }
}
