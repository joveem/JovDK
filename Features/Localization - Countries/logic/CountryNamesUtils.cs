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


namespace JovDK.Localization.Countries
{
    public static partial class CountryNamesUtils
    {
        // configs
        static readonly Dictionary<string, string> _nativeNameOverridesByUpperIsoCode = new(StringComparer.OrdinalIgnoreCase)
        {
            { "US", "United States" },
            { "GB", "United Kingdom" },
            { "KR", "대한민국" },
            { "KP", "조선민주주의인민공화국" },
            { "CI", "Côte d’Ivoire" },
            { "CD", "République démocratique du Congo" },
            { "TZ", "Tanzania" },
            { "IR", "ایران" },
            { "VN", "Việt Nam" },
            { "SY", "سوريا" },
            { "RU", "Россия" },
            { "LA", "ປະເທດລາວ" },
            { "BO", "Estado Plurinacional de Bolivia" },
            { "VE", "República Bolivariana de Venezuela" },
            { "XK", "Kosovë" }
        };

        static readonly Dictionary<string, string> _nativeNameOverridesFallbacksByUpperIsoCode = new(StringComparer.OrdinalIgnoreCase)
        {
            // ---- Caribbean / NANP & Atlantic territories ----
            ["AC"] = "Ascension Island",                    // UK overseas; English is the practical local form
            ["AD"] = "Andorra",                             // Same in Catalan/ES/FR; short and non-controversial
            ["AG"] = "Antigua and Barbuda",                 // English official
            ["AI"] = "Anguilla",                            // English official
            ["AS"] = "American Samoa",                      // English/Samoan; English short is widely used in UI
            ["BB"] = "Barbados",                            // English official
            ["BM"] = "Bermuda",                             // English official
            ["BS"] = "The Bahamas",                         // Official short English form
            ["DM"] = "Dominica",                            // English official
            ["GD"] = "Grenada",                             // English official
            ["GU"] = "Guam",                                // English/Chamorro; English short is standard in UI
            ["KN"] = "Saint Kitts and Nevis",               // English official
            ["KY"] = "Cayman Islands",                      // English official
            ["LC"] = "Saint Lucia",                         // English official
            ["MP"] = "Northern Mariana Islands",            // English/Chamorro; English short is standard
            ["MS"] = "Montserrat",                          // English official
            ["SX"] = "Sint Maarten",                        // Dutch official; “Sint Maarten” is native in NL
            ["TC"] = "Turks and Caicos Islands",            // English official
            ["VC"] = "Saint Vincent and the Grenadines",    // English official
            ["VG"] = "British Virgin Islands",              // English official
            ["VI"] = "U.S. Virgin Islands",                 // English official

            // ---- Africa / Indian Ocean ----
            ["AO"] = "Angola",                              // Portuguese endonym
            ["BF"] = "Burkina Faso",                        // French-based endonym (unchanged)
            ["BI"] = "Burundi",                             // Kirundi/FR; “Burundi” is stable
            ["BJ"] = "Bénin",                               // French endonym commonly used domestically
            ["CF"] = "République centrafricaine",           // French endonym (short form widely understood)
            ["CG"] = "République du Congo",                 // French endonym for Congo (Brazzaville)
            ["CV"] = "Cabo Verde",                          // Official endonym (requested internationally)
            ["DJ"] = "Djibouti",                            // FR/AR; “Djibouti” is stable endonym
            ["EH"] = "الصحراء الغربية / Western Sahara",   // Sensitive status; Arabic endonym + neutral English
            ["GA"] = "Gabon",                               // FR endonym
            ["GH"] = "Ghana",                               // English endonym
            ["GM"] = "The Gambia",                          // Official English short with article
            ["GN"] = "Guinée",                              // FR endonym
            ["GQ"] = "Guinea Ecuatorial",                   // ES endonym
            ["GW"] = "Guiné-Bissau",                        // PT endonym
            ["GY"] = "Guyana",                              // EN endonym
            ["IO"] = "British Indian Ocean Territory",      // English designation (no widely used local endonym)
            ["KM"] = "Komori / Comores",                    // Multiple official (Comorian/FR); neutral dual form
            ["LR"] = "Liberia",                             // EN endonym
            ["LS"] = "Lesotho",                             // Sesotho/EN; “Lesotho” is stable
            ["MG"] = "Madagasikara / Madagascar",           // MG native vs FR/EN; dual to avoid preference
            ["MR"] = "موريتانيا / Mauritanie",             // AR/FR both official; dual neutral form
            ["MU"] = "Mauritius / Maurice",                 // EN/FR both common; dual to avoid preference
            ["MW"] = "Malawi",                              // EN endonym
            ["MZ"] = "Moçambique",                          // PT endonym
            ["NA"] = "Namibia",                             // EN endonym
            ["NE"] = "Niger",                               // FR endonym (without accent in FR usage)
            ["SD"] = "السودان / Sudan",                     // AR/EN both used; dual neutral form
            ["SL"] = "Sierra Leone",                        // EN endonym
            ["SS"] = "South Sudan",                         // EN endonym
            ["ST"] = "São Tomé e Príncipe",                 // PT endonym
            ["SZ"] = "Eswatini",                            // Official endonym
            ["TD"] = "Tchad",                               // FR endonym (Chad in EN; FR is local official)
            ["TG"] = "Togo",                                // FR endonym (same spelling)
            ["UG"] = "Uganda",                              // EN endonym
            ["ZM"] = "Zambia",                              // EN endonym

            // ---- Europe / Overseas (FR/UK/Nordic) ----
            ["AX"] = "Åland",                               // Swedish endonym
            ["BL"] = "Saint-Barthélemy",                    // FR endonym
            ["BQ"] = "Caribisch Nederland",                 // NL endonym
            ["CW"] = "Curaçao",                             // NL endonym
            ["FK"] = "Falkland Islands / Islas Malvinas",   // Disputed naming; dual EN/ES neutral form
            ["GG"] = "Guernsey",                            // EN/Norse heritage; Guernésiais exists but rare in UI
            ["GF"] = "Guyane",                              // FR endonym
            ["GI"] = "Gibraltar",                           // EN endonym
            ["GP"] = "Guadeloupe",                          // FR endonym
            ["IM"] = "Isle of Man",                         // EN endonym (Manx exists but rare in UI)
            ["JE"] = "Jersey",                              // EN endonym
            ["MF"] = "Saint-Martin",                        // FR endonym (northern, FR part)
            ["NC"] = "Nouvelle-Calédonie",                  // FR endonym
            ["PM"] = "Saint-Pierre-et-Miquelon",            // FR endonym
            ["SJ"] = "Svalbard og Jan Mayen",               // NO endonym
            ["VA"] = "Città del Vaticano",                  // IT endonym
            ["WF"] = "Wallis-et-Futuna",                    // FR endonym
            ["YT"] = "Mayotte",                             // FR endonym

            // ---- Asia / Pacific ----
            ["AW"] = "Aruba",                               // NL/Papiamento; Aruba is stable
            ["BT"] = "འབྲུག་ཡུལ་ / Bhutan",                       // Dzongkha endonym + EN for UI fallback
            ["CC"] = "Cocos (Keeling) Islands",             // EN designation
            ["CK"] = "Cook Islands",                        // EN endonym
            ["CX"] = "Christmas Island",                    // EN designation
            ["CY"] = "Κύπρος / Kıbrıs",                     // Greek/Turkish; dual to avoid preference
            ["Fj"] = "Fiji",                                // EN endonym (note: key must be uppercase "FJ")
            ["FM"] = "Federated States of Micronesia",      // EN endonym (no short uncontested local name)
            ["KI"] = "Kiribati",                            // Gilbertese also “Kiribati”; same spelling
            ["MH"] = "Marshall Islands",                    // EN endonym
            ["MV"] = "ދިވެހި / Maldives",                      // Dhivehi + EN fallback (UI readability)
            ["NF"] = "Norfolk Island",                      // EN designation
            ["NR"] = "Nauru",                               // EN endonym (Nauruan similar)
            ["NU"] = "Niue",                                // EN endonym
            ["PF"] = "Polynésie française",                 // FR endonym
            ["PG"] = "Papua Niugini / Papua New Guinea",    // Tok Pisin + EN dual neutral
            ["PW"] = "Belau / Palau",                       // Palauan + EN dual neutral
            ["SB"] = "Solomon Islands",                     // EN endonym
            ["SC"] = "Sesel / Seychelles",                  // Seychellois Creole + EN dual neutral
            ["SH"] = "Saint Helena, Ascension and Tristan da Cunha", // EN designation for the territory
            ["TA"] = "Tristan da Cunha",                    // Local/EN endonym
            ["TK"] = "Tokelau",                             // EN endonym (Tokelauan same)
            ["TL"] = "Timor-Leste",                         // PT/Tetum endonym
            ["TO"] = "Tonga",                               // Tongan/EN same
            ["TV"] = "Tuvalu",                              // Tuvaluan/EN same
            ["VU"] = "Vanuatu",                             // Bislama/EN/FR; Vanuatu is neutral
            ["WS"] = "Sāmoa",                               // Samoan endonym with macron

            // ---- Middle East / Recognition-sensitive ----
            ["PS"] = "دولة فلسطين / State of Palestine",    // Recognition-sensitive; Arabic + neutral EN

            // ---- Europe / microstates ----
            ["SM"] = "San Marino",                 // Italian endonym; same spelling; non-controversial

            // ---- Caribbean / French overseas ----
            ["MQ"] = "Martinique",                 // French overseas department; French endonym equals English

            // ---- South America ----
            ["SR"] = "Suriname",                   // Dutch endonym; same spelling; non-controversial
        };


        #region Controller
        public static string GetCountryNameByIsoCode(string countryIsoCode, bool debugIfNull = true)
        {
            if (string.IsNullOrWhiteSpace(countryIsoCode) || countryIsoCode.Length < 2)
            {
                bool hasToDebug = debugIfNull || countryIsoCode != null;

                if (hasToDebug)
                {
                    DebugExtension.DevLogWarning(
                        "$$> ".ToColor(GoodColors.Red),
                        "Invalid country ISO code!", "\n",
                        "countryIsoCode = ", countryIsoCode.SerializeObjectToJSON(), "\n",
                        "");
                }

                return "...";
            }

            string finalCountryIsoCode = countryIsoCode.Trim().ToUpperInvariant();

            if (_nativeNameOverridesByUpperIsoCode.TryGetValue(finalCountryIsoCode, out var pretty))
                return pretty;

            string value = "...";

            try
            {
                var region = new RegionInfo(finalCountryIsoCode);
                value = region.NativeName;

                if (String.IsNullOrWhiteSpace(value))
                {
                    // TODO: REVIEW THIS!!!
                    // DebugExtension.DevLogWarning(
                    //     "$$> ".ToColor(GoodColors.Red),
                    //     "value is empty!", "\n",
                    //     "countryIsoCode = ", countryIsoCode.SerializeObjectToJSON(), "\n",
                    //     "region.NativeName = ", region.NativeName.SerializeObjectToJSON(), "\n",
                    //     "region.EnglishName = ", region.EnglishName.SerializeObjectToJSON(), "\n",
                    //     "");
                    value = region.EnglishName;
                }

                if (String.IsNullOrWhiteSpace(value))
                {
                    DebugExtension.DevLogWarning(
                        "$$> ".ToColor(GoodColors.Red),
                        "value is empty!", "\n",
                        "countryIsoCode = ", countryIsoCode.SerializeObjectToJSON(), "\n",
                        "region.NativeName = ", region.NativeName.SerializeObjectToJSON(), "\n",
                        "region.EnglishName = ", region.EnglishName.SerializeObjectToJSON(), "\n",
                        "");
                }
            }
            catch (Exception ex0)
            {
                try
                {
                    bool hasFallback = _nativeNameOverridesFallbacksByUpperIsoCode.TryGetValue(finalCountryIsoCode, out var fallback);

                    if (hasFallback)
                        value = fallback;
                    else
                    {
                        DebugExtension.DevLogError(
                            "$> ".ToColor(GoodColors.Red),
                            "Failed to get country name for ISO code!", "\n",
                            "countryIsoCode = ", countryIsoCode.SerializeObjectToJSON(), "\n",
                            "ex0: ", ex0.Message, "\n",
                            "ex0 Stack Trace: ", ex0.StackTrace, "\n",
                            "");
                    }
                }
                catch (Exception ex1)
                {
                    DebugExtension.DevLogError(
                        "$> ".ToColor(GoodColors.Red),
                        "Failed to get country name for ISO code!", "\n",
                        "countryIsoCode = ", countryIsoCode.SerializeObjectToJSON(), "\n",
                        "ex1: ", ex1.Message, "\n",
                        "ex1 Stack Trace: ", ex1.StackTrace, "\n",
                        "");
                }
            }

            return value;
        }
        #endregion Controller
    }
}
