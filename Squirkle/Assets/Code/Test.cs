using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SendGameTime (float time);

    [SerializeField] private TextMeshProUGUI label;

    void Update()
    {
        #if UNITY_WEBGL == true && UNITY_EDITOR == false
        SendGameTime(Time.time);
        #endif
    }

    public void ShowText(string text)
    {
        label.text = text;
    }
}
