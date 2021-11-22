/* Copyright (C) 2021 Vadimskyi - All Rights Reserved
 * Github - https://github.com/Vadimskyi
 * Website - https://www.vadimskyi.com/
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License.
 */
using UnityEngine;

namespace VadimskyiLab.Android
{
    public class AndroidScreenUtils
    {
        private AndroidJavaClass _nativePlugin;

        public AndroidScreenUtils()
        {
            _nativePlugin = new AndroidJavaClass("com.vadimskyi.screenbrightness.ScreenBrightnessUtils");
        }

        /// <param name="brightness">0 - 1</param>
        public void SetBrightness(float brightness)
        {
            _nativePlugin.CallStatic("SetAppBrightness", brightness);
        }
        
        public void GetSystemBrightness()
        {
            //_nativePlugin.CallStatic("GetSystemBrightness", brightness);
        }
    }
}