// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using SystemRandom = System.Random;
using UnityRandom = UnityEngine.Random;

// third
using DG.Tweening;
using R3;
using TMPro;
using PhoneNumbers;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Forms.PhoneNumber
{
    public static partial class PhoneNumberFormsUtils
    {
        #region Controller
        public static string GetDdiTextByIsoCode(string countryIsoCode)
        {
            string value = "+...";

            if (!string.IsNullOrWhiteSpace(countryIsoCode))
            {
                var util = PhoneNumberUtil.GetInstance();
                int code = util.GetCountryCodeForRegion(countryIsoCode.Trim().ToUpperInvariant());

                value = code > 0 ? "+" + code.ToString() : "+...";
            }

            return value;
        }
        #endregion Controller
    }
}
