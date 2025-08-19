// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

using SystemRandom = System.Random;
using UnityRandom = UnityEngine.Random;

// third
using DG.Tweening;
using R3;
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Generic.Assets
{
    public static partial class ResourceUtils
    {
        #region Controller
        public static IEnumerator LoadSpriteCoroutine(string resourcePath, Action<Sprite> onLoaded)
        {
            if (string.IsNullOrEmpty(resourcePath))
            {
                onLoaded?.Invoke(null);
                yield break;
            }

            ResourceRequest request = Resources.LoadAsync<Sprite>(resourcePath);

            yield return request;

            var sprite = request.asset as Sprite;
            onLoaded?.Invoke(sprite);
        }
        #endregion Controller
    }
}
