using UnityEngine;
//PlayerかBombの爆発に触れれば消滅するBlock
public class SoilBlock : MonoBehaviour, IBreakable
{
    public AudioClip breakBlock;

    public void Breaked()
    {
        // PointController.instance.AddSoil();
        AudioSourceController.instance.PlayOneShot(breakBlock);
        Destroy(gameObject);
    }
}
