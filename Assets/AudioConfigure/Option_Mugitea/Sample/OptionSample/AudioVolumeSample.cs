using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mugitea.Option;
using UnityEngine.UI;
using KanKikuchi.AudioManager;

public class AudioVolumeSample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BGMManager.Instance.Play(BGMPath.BATTLE27);
    }

    public void OpenOption()
    {
        SEManager.Instance.Play(SEPath.SYSTEM23);
        OptionManager.Instance.OpenOptionWindow();
    }
}
