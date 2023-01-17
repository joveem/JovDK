using UnityEngine.UI;

using TMPro;

public static class ButtonTools
{

    public static void SetTextInButton(this Button _button, string _text)
    {

        if (_button.GetComponentInChildren<TextMeshProUGUI>() != null)
        {

            _button.GetComponentInChildren<TextMeshProUGUI>().text = _text;

        }

    }

}