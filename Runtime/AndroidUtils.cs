/* Copyright (C) 2021 Vadimskyi - All Rights Reserved
 * Github - https://github.com/Vadimskyi
 * Website - https://www.vadimskyi.com/
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License.
 */
using UnityEngine;

namespace VadimskyiLab.Android
{
    public static class AndroidUtils
    {
        /// <summary>
        /// Lazy initialization
        /// </summary>
        private static AndroidNavigationBarUtils NavAndStatusBar => _navAndStatusBar ?? (_navAndStatusBar = new AndroidNavigationBarUtils());
        private static AndroidNavigationBarUtils _navAndStatusBar;

        private static AndroidScreenUtils ScreenUtils => _screenUtils ?? (_screenUtils = new AndroidScreenUtils());
        private static AndroidScreenUtils _screenUtils;

        private static AndroidJavaClass IpAddressUtils => _ipAddressUtils ?? (_ipAddressUtils = new AndroidJavaClass("com.vadimskyi.ipaddress.IpAddressUtils"));
        private static AndroidJavaClass _ipAddressUtils;

        private static AndroidJavaClass WifiUtils => _wifiUtils ?? (_wifiUtils = new AndroidJavaClass("com.vadimskyi.wifiutils.WifiUtils"));
        private static AndroidJavaClass _wifiUtils;


        public static void SetNavigationBarVisibility(bool show)
        {
            if (show) NavAndStatusBar.ShowNavBar();
            else NavAndStatusBar.HideNavBar();
        }

        public static void SetStatusBarVisibility(bool show)
        {
            if (show) NavAndStatusBar.ShowStatusBar();
            else NavAndStatusBar.HideStatusBar();
        }

        public static void SetNavigationBarColor(Color color)
        {
            NavAndStatusBar.SetColor(color);
        }

        /// <param name="brightness">0 - 1</param>
        public static void SetScreenBrightness(float brightness)
        {
            ScreenUtils.SetBrightness(brightness);
        }

        public static bool IsAppInstalledExternally()
        {
            return Application.persistentDataPath.Contains("sdcard");
        }

        public static void RequestNativeIpAddress()
        {
            IpAddressUtils.CallStatic("GetIpAddress");
        }

        public static void SetWifiEnabled(bool enabled)
        {
            WifiUtils.CallStatic("EnableWifiTest");
        }

        public static bool IsWifiEnabled()
        {
            using (AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"))
            {
                try
                {
                    using (var wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi"))
                    {
                        return wifiManager.Call<bool>("isWifiEnabled");
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
