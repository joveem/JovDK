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
    public partial class AudioService : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // [SerializeField] bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        // [SerializeField] bool _state;
        Dictionary<string, AudioConfig> _currentAudiosById = new Dictionary<string, AudioConfig>();


        // [Space(5), Header("[ Parts ]"), Space(10)]

        // [SerializeField] bool _parts;
        // [SerializeField] Button _mainButton;
        // [SerializeField] TextMeshProUGUI _mainText;
        // [SerializeField] Image _mainImage;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] List<AudioConfig> _audiosList = new List<AudioConfig>();

        /// <summary>
        /// If true, changes made in _audiosList values
        /// on inspector will be effective even in the
        /// Editor play mode
        /// </summary>
#if UNITY_EDITOR
        bool _DEBUG_isOnDebugDynamicMode = false;
        // bool _DEBUG_isOnDebugDynamicMode = true;
#else
        bool _DEBUG_isOnDebugDynamicMode = false;
#endif


        #region MonoBehaviour
        void Awake()
        {
            SetInitialState();
        }
        #endregion MonoBehaviour

        #region Controller
        void SetInitialState()
        {
            _audiosList.DoIfNotNull(() =>
            {
                foreach (AudioConfig audioConfig in _audiosList)
                {
                    audioConfig.DoIfNotNull(() =>
                    {
                        bool isAlreadyRegistered = _currentAudiosById.ContainsKey(audioConfig.Id);

                        if (!isAlreadyRegistered)
                        {
                            int variationsAmount = audioConfig.AudioClipsVariationsList.Length;
                            audioConfig.AudioSourceIntances = new AudioSource[variationsAmount];

                            for (int i = 0; i < variationsAmount; i++)
                            {
                                AudioClip audioClipVariation = audioConfig.AudioClipsVariationsList[i];

                                audioClipVariation.DoIfNotNull(() =>
                                {
                                    AudioSource audioSourceIntance = gameObject.AddComponent<AudioSource>();
                                    audioSourceIntance.clip = audioClipVariation;
                                    audioSourceIntance.volume = audioConfig.VolumeFactor;
                                    audioSourceIntance.pitch = audioConfig.PitchFactor;
                                    audioSourceIntance.spatialBlend = audioConfig.Is2D ? 0f : 1f;

                                    audioConfig.AudioSourceIntances[i] = audioSourceIntance;
                                },
                                () =>
                                {
                                    DebugExtension.DevLogError(
                                        "audioClipVariation is null!" + "\n" +
                                        "audioConfig.Id = " + audioConfig.Id.SerializeObjectToJSON() + "\n" +
                                        "i = " + i + "\n" +
                                        "variationsAmount = " + variationsAmount + "\n" +
                                        "");
                                });
                            }

                            _currentAudiosById[audioConfig.Id] = audioConfig;
                        }
                        else
                        {
                            DebugExtension.DevLogError(
                                "$$ > ".ToColor(GoodColors.Red) +
                                "Duplicated Id in _audiosList!".ToColor(GoodColors.Orange) + "\n" +
                                "audioConfig.Id = " + audioConfig.Id.SerializeObjectToJSON() + "\n" +
                                "");
                        }
                    });
                }
            });
        }

        bool IsAudioRegistered(string audioId, out AudioConfig foundAudioConfig)
        {
            bool value = false;

            foundAudioConfig = null;

            if (!_DEBUG_isOnDebugDynamicMode)
            {
                value = _currentAudiosById.ContainsKey(audioId);

                if (value)
                    foundAudioConfig = _currentAudiosById[audioId];
            }
            else
            {
                foreach (AudioConfig audioConfig in _audiosList)
                {
                    if (audioConfig is not null && audioConfig.Id == audioId)
                    {
                        value = true;
                        foundAudioConfig = audioConfig;
                        break;
                    }
                }
            }

            return value;
        }

        public AudioTaskResult _INTERNAL_PlaySfx(
            string sfxId,
            float pitchMultiplier = 1f,
            int? ignoreRandomIndex = null,
            int? forceRandomIndex = null)
        {
            AudioTaskResult result = new AudioTaskResult();
            result.Success = false;

            bool isAlreadyRegistered = IsAudioRegistered(sfxId, out AudioConfig audioConfig);

            if (isAlreadyRegistered)
            {
                int randomIndex = 0;
                int variationsAmount = audioConfig.AudioClipsVariationsList.Length;

                if (variationsAmount > 0)
                {
                    if (forceRandomIndex == null)
                    {
                        if (ignoreRandomIndex == null)
                        {

                            randomIndex = UnityRandom.Range(0, variationsAmount);
                        }
                        else
                        {
                            do
                            {
                                randomIndex = UnityRandom.Range(0, variationsAmount);
                            }
                            while (randomIndex == (int)ignoreRandomIndex);
                        }
                    }
                    else
                        randomIndex = (int)forceRandomIndex;
                }

                AudioSource audioSourceToPlay = audioConfig.AudioSourceIntances[randomIndex];
                audioSourceToPlay.DoIfNotNull(() =>
                {
                    // TODO: REVIEW THIS!
                    audioSourceToPlay.pitch = audioConfig.PitchFactor * pitchMultiplier;
                    audioSourceToPlay.volume = audioConfig.VolumeFactor;
                    audioSourceToPlay.Play();

                    result.Success = true;
                    result.AudioMaxDuration = audioSourceToPlay.clip.length;
                    result.RandomVariationIndex = randomIndex;
                });
            }
            else
            {
                DebugExtension.DevLogError(
                    "Audio was not found!" + "\n" +
                    "sfxId = " + sfxId.SerializeObjectToJSON() + "\n" +
                    "");
            }

            return result;
        }

        public AudioTaskResult _INTERNAL_StopSfx(string sfxId, float pitchMultiplier = 1f)
        {
            AudioTaskResult result = new AudioTaskResult();
            result.Success = false;

            bool isAlreadyRegistered = IsAudioRegistered(sfxId, out AudioConfig audioConfig);

            if (isAlreadyRegistered)
            {
                foreach (AudioSource audioSourceToPlay in audioConfig.AudioSourceIntances)
                {
                    audioSourceToPlay.DoIfNotNull(() =>
                    {
                        // TODO: REVIEW THIS!
                        audioSourceToPlay.pitch = audioConfig.PitchFactor * pitchMultiplier;
                        audioSourceToPlay.volume = audioConfig.VolumeFactor;
                        audioSourceToPlay.Stop();

                        result.Success = true;
                    });
                }
            }
            else
            {
                DebugExtension.DevLogError(
                    "Audio was not found!" + "\n" +
                    "sfxId = " + sfxId.SerializeObjectToJSON() + "\n" +
                    "");
            }

            return result;
        }

        public AudioTaskResult _INTERNAL_PlayOneShotSfx(
            string sfxId,
            float pitchMultiplier = 1f,
            int? ignoreRandomIndex = null,
            int? forceRandomIndex = null)
        {
            AudioTaskResult result = new AudioTaskResult();
            result.Success = false;

            bool isAlreadyRegistered = IsAudioRegistered(sfxId, out AudioConfig audioConfig);

            if (isAlreadyRegistered)
            {
                int randomIndex = 0;
                int variationsAmount = audioConfig.AudioClipsVariationsList.Length;

                if (variationsAmount > 0)
                {
                    if (forceRandomIndex == null)
                    {
                        if (ignoreRandomIndex == null)
                        {

                            randomIndex = UnityRandom.Range(0, variationsAmount);
                        }
                        else
                        {
                            do
                            {
                                randomIndex = UnityRandom.Range(0, variationsAmount);
                            }
                            while (randomIndex == (int)ignoreRandomIndex);
                        }
                    }
                    else
                        randomIndex = (int)forceRandomIndex;
                }

                AudioSource audioSourceToPlay = audioConfig.AudioSourceIntances[randomIndex];
                audioSourceToPlay.DoIfNotNull(() =>
                {
                    // TODO: REVIEW THIS!
                    audioSourceToPlay.pitch = audioConfig.PitchFactor * pitchMultiplier;
                    audioSourceToPlay.volume = audioConfig.VolumeFactor;
                    audioSourceToPlay.PlayOneShot(audioSourceToPlay.clip);

                    result.Success = true;
                    result.AudioMaxDuration = audioSourceToPlay.clip.length;
                    result.RandomVariationIndex = randomIndex;
                });
            }
            else
            {
                DebugExtension.DevLogError(
                    "Audio was not found!" + "\n" +
                    "sfxId = " + sfxId.SerializeObjectToJSON() + "\n" +
                    "");
            }

            return result;
        }
        #endregion Controller
    }

    public static class AudioServiceExtension
    {
        public static AudioTaskResult PlaySfx(
            this AudioService baseAudioService,
            string sfxId,
            float pitchMultiplier = 1f,
            int? ignoreRandomIndex = null,
            int? forceRandomIndex = null)
        {
            AudioTaskResult result = new AudioTaskResult();
            result.Success = false;

            baseAudioService.DoIfNotNull(() => result = baseAudioService._INTERNAL_PlaySfx(sfxId, pitchMultiplier));

            return result;
        }

        public static AudioTaskResult StopSfx(this AudioService baseAudioService, string sfxId, float pitchMultiplier = 1f)
        {
            AudioTaskResult result = new AudioTaskResult();
            result.Success = false;

            baseAudioService.DoIfNotNull(() => result = baseAudioService._INTERNAL_StopSfx(sfxId, pitchMultiplier));

            return result;
        }

        public static AudioTaskResult PlayOneShotSfx(
            this AudioService baseAudioService,
            string sfxId,
            float pitchMultiplier = 1f,
            int? ignoreRandomIndex = null,
            int? forceRandomIndex = null)
        {
            // DebugExtension.DevLog("sfxId = ", sfxId.SerializeObjectToJSON());

            AudioTaskResult result = new AudioTaskResult();
            result.Success = false;

            baseAudioService.DoIfNotNull(() => result = baseAudioService._INTERNAL_PlayOneShotSfx(sfxId, pitchMultiplier));

            return result;
        }
    }
}
