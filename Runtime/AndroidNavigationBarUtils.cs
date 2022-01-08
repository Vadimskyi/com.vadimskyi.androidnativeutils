/* Copyright (C) 2021 Vadimskyi - All Rights Reserved
 * Github - https://github.com/Vadimskyi
 * Website - https://www.vadimskyi.com/
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License.
 */
using UnityEngine;

namespace VadimskyiLab.Android
{
    internal class AndroidNavigationBarUtils
    {
        private static AndroidJavaClass _nativePlugin;

        public AndroidNavigationBarUtils()
        {
            _nativePlugin = new AndroidJavaClass("com.vadimskyi.navigationbar.NavigationBarUtils");
        }

        public void SetColor(Color color)
        {
            var hex = ColorUtility.ToHtmlStringRGB(color);
            _nativePlugin.CallStatic("SetNavBarColor", "#" + hex);
        }

        public void ShowNavBar()
        {
            Screen.fullScreen = false;
            _nativePlugin.CallStatic("ShowNavBar");
        }

        public void HideNavBar()
        {
            Screen.fullScreen = true;
            _nativePlugin.CallStatic("HideNavBar");
        }

        public void ShowStatusBar(int ifScreenHasCutout = 0)
        {
            _nativePlugin.CallStatic("ShowStatusBar", ifScreenHasCutout);
        }

        public void HideStatusBar()
        {
            _nativePlugin.CallStatic("HideStatusBar");
        }

        public int GetAndroidApiVersion()
        {
            return _nativePlugin.CallStatic<int>("GetApiNumber");
        }
    }
}
