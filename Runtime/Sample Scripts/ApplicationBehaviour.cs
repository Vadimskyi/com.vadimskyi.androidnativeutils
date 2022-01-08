/* Copyright (C) 2022 Vadimskyi - All Rights Reserved
 * Github - https://github.com/Vadimskyi
 * Website - https://www.vadimskyi.com/
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License.
 */


using UnityEngine;
using UnityEngine.UI;
using VadimskyiLab.Android;

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