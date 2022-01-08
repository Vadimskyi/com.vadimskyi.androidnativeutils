using UnityEngine;

namespace VadimskyiLab.Android
{
    internal class AndroidKeyboardEventsHandler : MonoBehaviour
    {
        private bool _cachedKeyboardState;
        private bool _trackKeyboardHeightChange;
        private float _keyboardHeightTimeTracker;
        private int _cachedKeyboardHeight;

        private const float _keyboardHeightTrackerTimeout = 2f;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            _cachedKeyboardState = TouchScreenKeyboard.visible;
        }

        private void Update()
        {
            var kState = TouchScreenKeyboard.visible;
            if (_cachedKeyboardState != kState)
            {
                VirtualKeyboardVisibilityChangedEvent.Invoke(kState);
                StartTrackingHeightChange();
            }

            if (_keyboardHeightTimeTracker > _keyboardHeightTrackerTimeout)
            {
                StopTrackingHeightChange();
            }

            if (_trackKeyboardHeightChange)
            {
                _keyboardHeightTimeTracker += Time.deltaTime;
                if (AndroidKeyboardUtils.GetHeight(includeInput: true) is int height 
                    && !Mathf.Approximately(height, _cachedKeyboardHeight))
                {
                    _cachedKeyboardHeight = height;
                    VirtualKeyboardHeightChangedEvent.Invoke(height);
                }
            }

            _cachedKeyboardState = kState;
        }

        private void StartTrackingHeightChange()
        {
            _trackKeyboardHeightChange = true;
            _keyboardHeightTimeTracker = 0;
            _cachedKeyboardHeight = AndroidKeyboardUtils.GetHeight(includeInput: true);
        }

        private void StopTrackingHeightChange()
        {
            _trackKeyboardHeightChange = false;
            _keyboardHeightTimeTracker = 0;
        }
    }
}