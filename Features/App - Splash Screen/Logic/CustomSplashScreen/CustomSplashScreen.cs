// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnitySplashScreen = UnityEngine.Rendering.SplashScreen;

// third
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.App.SplashScreen
{
    public partial class CustomSplashScreen : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        // [Space(5), Header("[ State ]"), Space(10)]

        // bool _state;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] Image _logoImage;
        [SerializeField] Image _backgroundImage;
        [SerializeField] CanvasGroup _contentCanvasGroup;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] float _splashDuration = 2f;
        [SerializeField] float _hideFadeDuration = 1f;
        [SerializeField] float _enableClickGapAfterFadeStart = 0.2f;
        [SerializeField] bool _DEBUG_simulateUnitySplashOnEditor = true;
        [SerializeField] bool _externalPlayback;
        public bool ExternalPlayback => _externalPlayback;
        public bool PlaybackComplete { get; private set; }
        public event Action PlaybackFinished;
        bool _playingExternal, _focused = true, _paused;
        Animator[] _controlledAnimators;
        AudioSource[] _controlledAudio;

        public bool PrepareExternalPlayback()
        {
            if (!_externalPlayback || _playingExternal || PlaybackComplete || _logoImage == null || _contentCanvasGroup == null) return false;
            _controlledAnimators = _logoImage.GetComponentsInChildren<Animator>(true);
            _controlledAudio = GetComponentsInChildren<AudioSource>(true);
            foreach (var source in _controlledAudio)
            {
                source.playOnAwake = false; source.Stop();
                if (source.clip != null) source.clip.LoadAudioData();
            }
            // Prepare the owned animator without displaying or advancing its first movement.
            _logoImage.gameObject.SetActive(true);
            var mask = _logoImage.GetComponent<CanvasGroup>();
            if (mask == null) mask = _logoImage.gameObject.AddComponent<CanvasGroup>();
            mask.alpha = 0;
            foreach (var animator in _controlledAnimators)
            { animator.enabled = false; animator.Rebind(); animator.Update(0); }
            return true;
        }
        public bool PlayExternal()
        {
            if (!_externalPlayback || !isActiveAndEnabled || _playingExternal || PlaybackComplete || _controlledAnimators == null) return false;
            _playingExternal = true; StartCoroutine(ExternalPresentation()); return true;
        }
        public void ShowExternalFallback()
        {
            if (!_externalPlayback) return;
            foreach (var source in GetComponentsInChildren<AudioSource>(true)) source.Stop();
            foreach (var animator in GetComponentsInChildren<Animator>(true)) animator.enabled = false;
            ShowContent();
            if (_logoImage == null) return;
            _logoImage.gameObject.SetActive(true);
            var mask = _logoImage.GetComponent<CanvasGroup>();
            if (mask != null) mask.alpha = 1;
        }
        IEnumerator ExternalPresentation()
        {
            _logoImage.GetComponent<CanvasGroup>().alpha = 1;
            foreach (var source in _controlledAudio) source.Play();
            float elapsed = 0;
            bool foreground = false;
            float duration = Mathf.Max(0, _splashDuration) + Mathf.Max(0, _hideFadeDuration);
            while (elapsed < duration)
            {
                bool active = _focused && !_paused;
                if (!active || !foreground)
                {
                    foreach (var source in _controlledAudio) { if (active) source.UnPause(); else source.Pause(); }
                    foreground = active; yield return null; continue;
                }
                float delta = Time.unscaledDeltaTime;
                elapsed += delta;
                foreach (var animator in _controlledAnimators) animator.Update(delta);
                _contentCanvasGroup.alpha = elapsed <= _splashDuration ? 1 :
                    1 - Mathf.Clamp01((elapsed - _splashDuration) / Mathf.Max(.001f, _hideFadeDuration));
                yield return null;
            }
            _contentCanvasGroup.alpha = 0;
            _contentCanvasGroup.blocksRaycasts = false;
            PlaybackComplete = true;
            PlaybackFinished?.Invoke();
            gameObject.SetActive(false);
        }
        void OnApplicationFocus(bool focused) { _focused = focused; }
        void OnApplicationPause(bool paused) { _paused = paused; }


        void Awake()
        {
            if (_externalPlayback) { ShowContent(); HideLogo(); return; }
            bool hasToShowSplashScreen = true;

#if UNITY_EDITOR
            hasToShowSplashScreen = _DEBUG_simulateUnitySplashOnEditor;
#endif

            if (hasToShowSplashScreen)
            {
                ShowContent();
                HideLogo();
            }
            else
                Destroy(gameObject);
        }

        IEnumerator Start()
        {
            if (_externalPlayback) yield break;
#if UNITY_EDITOR
            if (_DEBUG_simulateUnitySplashOnEditor)
            {
                UnitySplashScreen.Begin();
                UnitySplashScreen.Draw();
            }
#endif

            while (!UnitySplashScreen.isFinished)
                yield return null;

            ShowLogo();

            yield return new WaitForSeconds(_splashDuration);
            PlayHideContent();

            yield return new WaitForSeconds(_enableClickGapAfterFadeStart);
            _backgroundImage.raycastTarget = false;
        }

        // void Update()
        // {

        // }

        // void FixedUpdate()
        // {

        // }
    }
}
