// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
// ...

// from company
using JovDK.Debugging;
using JovDK.Debugging.Input;
using JovDK.Control.Touch;
using JovDK.SafeActions;

// from project
// ...


namespace JovDK.Testing.Showcase
{
    public partial class Asmb_TouchControllerAnalogsShowcaseScene : MonoBehaviour
    {
        void SetInitialValues()
        {
            _leftAnalogDebugView.DoIfNotNull(() => _leftAnalogDebugView.SetAxysFactor(1f));
            _rightDragAreaDebugView.DoIfNotNull(() => _rightDragAreaDebugView.SetAxysFactor(1f));
        }
    }
}
