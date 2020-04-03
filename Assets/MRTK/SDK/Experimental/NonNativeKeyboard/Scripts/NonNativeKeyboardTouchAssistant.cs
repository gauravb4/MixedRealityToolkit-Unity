﻿using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using UnityEngine.UI;


namespace Microsoft.MixedReality.Toolkit.Experimental.UI
{
    public class NonNativeKeyboardTouchAssistant : MonoBehaviour
    {
        [SerializeField] 
        private AudioClip _clickSound = null;

        private AudioSource _clickSoundPlayer;

        private void Start()
        {
            var capabilityChecker = CoreServices.InputSystem as IMixedRealityCapabilityCheck;

            if(capabilityChecker != null && capabilityChecker.CheckCapability(MixedRealityCapability.ArticulatedHand))
            {
                EnableTouch();
            }
        }

        private void EnableTouch()
        {
            _clickSoundPlayer = gameObject.AddComponent<AudioSource>();
            _clickSoundPlayer.playOnAwake = false;
            _clickSoundPlayer.spatialize = true;
            _clickSoundPlayer.clip = _clickSound;
            var buttons = GetComponentsInChildren<Button>();
            foreach (var button in buttons)
            {
                var ni = button.gameObject.EnsureComponent<NearInteractionTouchableUnityUI>();
                ni.EventsToReceive = TouchableEventType.Pointer;
                button.onClick.AddListener(PlayClick);
            }
        }

        private void PlayClick()
        {
            if (_clickSound != null)
            {
                _clickSoundPlayer.Play();
            }
        }
    }
}
