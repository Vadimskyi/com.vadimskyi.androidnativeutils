/* Copyright (C) 2021 Vadimskyi - All Rights Reserved
 * Github - https://github.com/Vadimskyi
 * Website - https://www.vadimskyi.com/
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License.
 */
using UnityEngine;

namespace VadimskyiLab.Android
{
    public static class AndroidKeyboardUtils
    {

        public static int GetHeight()
        {
            return GetHeight(false);
        }

        public static int GetHeight(bool includeInput)
        {
#if !UNITY_EDITOR && UNITY_ANDROID
			using ( var unityClass = new AndroidJavaClass( "com.unity3d.player.UnityPlayer" ) )
			{
				var currentActivity = unityClass.GetStatic<AndroidJavaObject>( "currentActivity" );
				var unityPlayer = currentActivity.Get<AndroidJavaObject>( "mUnityPlayer" );
				var view = unityPlayer.Call<AndroidJavaObject>( "getView" );

				if ( view == null ) return 0;

				int result;

				using ( var rect = new AndroidJavaObject( "android.graphics.Rect" ) )
				{
					view.Call( "getWindowVisibleDisplayFrame", rect );
					result = Screen.height - rect.Call<int>( "height" );
				}

				if ( !includeInput ) return result;

				var softInputDialog = unityPlayer.Get<AndroidJavaObject>( "mSoftInputDialog" );
				var window = softInputDialog?.Call<AndroidJavaObject>( "getWindow" );
				var decorView = window?.Call<AndroidJavaObject>( "getDecorView" );

				if ( decorView == null ) return result;

				var decorHeight = decorView.Call<int>( "getHeight" );
				result += decorHeight;

				return result;
			}
#else
            var area = TouchScreenKeyboard.area;
            var height = Mathf.RoundToInt(area.height);
            //return Screen.height <= height ? 0 : height;
            return 600;
#endif
        }
    }
}