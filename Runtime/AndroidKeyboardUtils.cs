/* Copyright (C) 2022 Vadimskyi - All Rights Reserved
 * Github - https://github.com/Vadimskyi
 * Website - https://www.vadimskyi.com/
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License.
 */

using UnityEngine;
using VadimskyiLab.Events;

namespace VadimskyiLab.Android
{
    public static class AndroidKeyboardUtils
    {
        private static GameObject _keyboardEventTracer;
        private static AndroidJavaClass _nativeUnityActivity;
        private static AndroidJavaObject _nativeUnityActivityCurrent;
        private static AndroidJavaObject _nativeUnityActivityCurrentPlayer;
        private static AndroidJavaObject _nativeUnityActivityView;

        static AndroidKeyboardUtils()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            _nativeUnityActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            _nativeUnityActivityCurrent = _nativeUnityActivity?.GetStatic<AndroidJavaObject>("currentActivity");
            _nativeUnityActivityCurrentPlayer = _nativeUnityActivityCurrent.Get<AndroidJavaObject>("mUnityPlayer");
            _nativeUnityActivityView = _nativeUnityActivityCurrentPlayer?.Call<AndroidJavaObject>("getView");
#endif
        }

        public static void StartKeyboardVisibilityTraceJob()
        {
            StopKeyboardVisibilityTraceJob();
            _keyboardEventTracer = new GameObject("AndroidKeyboardEventsHandler", typeof(AndroidKeyboardEventsHandler));
        }

        public static void StopKeyboardVisibilityTraceJob()
        {
            if(!_keyboardEventTracer || _keyboardEventTracer == null) return;
            UnityEngine.Object.Destroy(_keyboardEventTracer);
        }

        public static int GetHeight(bool includeInput)
        {
            int result = default;
#if !UNITY_EDITOR && UNITY_ANDROID
            if (_nativeUnityActivityView == null) return result;


            using (var rect = new AndroidJavaObject("android.graphics.Rect"))
            {
                _nativeUnityActivityView.Call("getWindowVisibleDisplayFrame", rect);
                result = Screen.height - rect.Call<int>("height");
            }

            if (!includeInput) return result;

            var softInputDialog = _nativeUnityActivityCurrentPlayer.Get<AndroidJavaObject>("mSoftInputDialog");
            var window = softInputDialog?.Call<AndroidJavaObject>("getWindow");
            var decorView = window?.Call<AndroidJavaObject>("getDecorView");

            if (decorView == null) return result;

            var decorHeight = decorView.Call<int>("getHeight");
            result += decorHeight;
#endif
            return result;
        }
    }


    public class VirtualKeyboardVisibilityChangedEvent : EventBase<VirtualKeyboardVisibilityChangedEvent, bool>
    {

    }


    public class VirtualKeyboardHeightChangedEvent : EventBase<VirtualKeyboardHeightChangedEvent, float>
    {

    }
}