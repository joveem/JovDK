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
        public string CategoryId = AudioCategoriesKeys.GamePlaySfx;
        public AudioClip[] AudioClipsVariationsList;
        public float VolumeFactor = 1f;
        public float PitchFactor = 1f;
        public bool Is2D = true;
        [HideInInspector] public AudioSource[] AudioSourceIntances;
    }

    public class AudioTaskOptions
    {
        public float PitchMultiplier = 1f;
        public bool Loop = false;
        public float? OverrideVolumeMultiplier = null;
        public int? IgnoreRandomIndex = null;
        public int? ForceRandomIndex = null;
        public double? InitialPlaybackPositionSeconds = null;
    }

    public class AudioTaskResult
    {
        public bool Success = false;
        public float AudioMaxDuration = -1f;
        public int RandomVariationIndex = -1;
        AudioSource _audioSourceResult = null;
        public float DefaultAudioVolumeFactor = 1f;
        public AudioSource AudioSourceResult { get { return _audioSourceResult; } }

        public void SetAudioSourceResult(AudioSource audioSource)
        {
            _audioSourceResult = audioSource;
        }
    }

    public static class AudioPlaybackPositionTools
    {
        public static int ResolveTimeSamples(
            double positionSeconds,
            int clipFrequency,
            int clipSamples)
        {
            if (clipFrequency <= 0 || clipSamples <= 0)
                return 0;

            double normalizedSeconds = System.Math.Max(0d, positionSeconds);
            long requestedSamples =
                (long)System.Math.Floor(normalizedSeconds * clipFrequency);
            return (int)(requestedSamples % clipSamples);
        }

        public static bool TryApplySeconds(
            AudioSource audioSource,
            double positionSeconds)
        {
            if (audioSource == null || audioSource.clip == null)
                return false;

            audioSource.timeSamples = ResolveTimeSamples(
                positionSeconds,
                audioSource.clip.frequency,
                audioSource.clip.samples);
            return true;
        }
    }

    public static class AudioCategoriesKeys
    {
        public const string MenuUiSfx = "menu-ui-sfx-01";
        public const string MenuVideoAudio = "menu-video-audio-01";
        public const string GamePlaySfx = "gameplay-sfx-01";
        public const string GamePlayMusic = "gameplay-music-01";
        public const string GamePlayVoiceNarrationSfx = "gameplay-voice-narration-sfx-01";
    }
}
