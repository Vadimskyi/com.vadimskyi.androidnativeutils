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
        public static bool IsAppInstalledExternally()
        {


            return Application.persistentDataPath.Contains("sdcard");
        }

        public static void RequestNativeIpAddress()
        {
            var nativePlugin = new AndroidJavaClass("com.vadimskyi.ipaddress.IpAddressUtils");
            nativePlugin.CallStatic("GetIpAddress");
        }

        public static void SetWifiEnabled(bool enabled)
        {
            var nativePlugin = new AndroidJavaClass("com.vadimskyi.wifiutils.WifiUtils");
            nativePlugin.CallStatic("EnableWifiTest");
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
