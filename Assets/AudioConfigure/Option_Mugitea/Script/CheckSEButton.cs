using KanKikuchi.AudioManager;
using UnityEngine;

public class CheckSEButton : MonoBehaviour
{
    public void Clicked()
    {
        SEManager.Instance.Play(SEPath.SYSTEM23);
    }
}
