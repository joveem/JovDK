// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using SystemRandom = System.Random;
using UnityRandom = UnityEngine.Random;

// third
using DG.Tweening;
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Audio.Service
{
    [Serializable]
    public class AudioConfig
    {
        public string Id = "UNDEFINED";
        public AudioClip[] AudioClipsVariationsList;
        public float VolumeFactor = 1f;
        public float PitchFactor = 1f;
        public bool Is2D = true;
        [HideInInspector] public AudioSource[] AudioSourceIntances;
    }

    public class AudioTaskResult
    {
        public bool Success = false;
        public int RandomVariationIndex = -1;
    }
}