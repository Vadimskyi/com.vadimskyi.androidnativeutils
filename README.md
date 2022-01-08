# Unity Android native utilities package
[![openupm](https://img.shields.io/npm/v/com.vadimskyi.androidnativeutils?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.vadimskyi.androidnativeutils/)

Contains native helpers methods for wifi management, navigation bar visibility and color, screen brightness, ip related information and more.

## Table of Contents

- [Installation](#installation)
    - [Unity Package](#unity-package)
    - [UPM CLI](#upm-cli)
- [NavBar and StatusBar](#navbar-and-statusbar)
- [Screen brightness](#screen-brightness)
- [WiFi management](#wifi-management)
- [Screen density utils](#screen-density-utils)
- [Virtual keyboard](#virtual-keyboard)
- [Author Info](#author-info)

## Installation

This library is distributed via Unity's built-in package manager. Required Unity 2018.3 or later.

### Unity Package
- Open Unity project
- Download and run .unitypackage file from the latest release

### UPM CLI
- You need to have upm-cli installed: https://github.com/openupm/openupm-cli#openupm-cli
- Open Git-Bash, CMD, or PowerShell for Windows console
```console
# go to the unity project folder
$ cd ~/Document/projects/hello-openupm

# add package
$ openupm add com.vadimskyi.androidnativeutils
```
- Open Unity for package to be installed

## NavBar and StatusBar
    
```csharp
public class ApplicationBehaviour : MonoBehaviour
{
    public void Awake()
    {
        AndroidUtils.SetNavigationBarVisibility(show: true);

        AndroidUtils.SetStatusBarVisibility(show: true);

        AndroidUtils.SetNavigationBarColor(color: Color.magenta);
    }
}
```

## Screen brightness
    
```csharp
public class ApplicationBehaviour : MonoBehaviour
{
    public void Awake()
    {
        AndroidUtils.SetScreenBrightness(brightness: 0.8f);
    }
}
```

## WiFi management
    
```csharp
public class ApplicationBehaviour : MonoBehaviour
{
    public void Awake()
    {
        if(!AndroidUtils.IsWifiEnabled())
            AndroidUtils.SetWifiEnabled(enabled:true);
    }
}
```

## Screen density utils
    
```csharp
public class ApplicationBehaviour : MonoBehaviour
{
    [SerializeField]
    private RectTransform _adaptivePanel;

    public void Awake()
    {
        _adaptivePanel.sizeDelta = new Vector2(
            x:_adaptivePanel.sizeDelta.x * AndroidMetrics.Density, 
            y:_adaptivePanel.sizeDelta.y * AndroidMetrics.Density);
    }
}
```

## Virtual keyboard
    
```csharp
public class ApplicationBehaviour : MonoBehaviour
{
    [SerializeField]
    private RectTransform _adaptivePanel;
    [SerializeField]
    private InputField _inputField;

    private void Awake()
    {
        VirtualKeyboardHeightChangedEvent.Subscribe(OnVirtualKeyboardHeightChanged);
        VirtualKeyboardVisibilityChangedEvent.Subscribe(OnVirtualKeyboardVisibilityChanged);
        OnVirtualKeyboardVisibilityChanged(TouchScreenKeyboard.visible);
        AndroidKeyboardUtils.StartKeyboardVisibilityTraceJob();
    }

    private void OnVirtualKeyboardHeightChanged(float height)
    {
        _adaptivePanel.anchoredPosition = new Vector2(
            x: _adaptivePanel.anchoredPosition.x,
            y: AndroidKeyboardUtils.GetHeight(includeInput: !_inputField.shouldHideMobileInput));
    }

    private void OnVirtualKeyboardVisibilityChanged(bool visible)
    {
        _adaptivePanel.gameObject.SetActive(visible);
    }

    private void OnDestroy()
    {
        VirtualKeyboardHeightChangedEvent.Unsubscribe(OnVirtualKeyboardHeightChanged);
        VirtualKeyboardVisibilityChangedEvent.Unsubscribe(OnVirtualKeyboardVisibilityChanged);
        AndroidKeyboardUtils.StopKeyboardVisibilityTraceJob();
    }
}
```


## Author Info

Vadim Zakrzhewskyi (a.k.a. Vadimskyi) a software developer from Ukraine.

~10 years of experience working with Unity3d, mostly freelance/outsource.

* Twitter: [https://twitter.com/vadimskyi](https://twitter.com/vadimskyi) (English/Russian)