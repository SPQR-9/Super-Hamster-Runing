using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplitudeScreenSaver : MonoBehaviour
{
    public void OnGameInitialize(int count)
    {
        Amplitude.Instance.logging = true;
        Amplitude.Instance.init("05bb8262fd30504c784c86e3c3de99ba");
        Amplitude.Instance.logEvent("game_start", new Dictionary<string, object>()
        {
            {"count",count }
        });
    }
}
