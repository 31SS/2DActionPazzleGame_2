using UnityEngine;
//PlayerかBombの爆発に触れれば消滅するBlock
public class SoilBlock : MonoBehaviour, IBreakable
{
    [SerializeField] private GameObject damageEffect;
    public void Breaked()
    {
        var nowTransform = transform;
        // PointController.instance.AddSoil();
        Instantiate(damageEffect, nowTransform.position, nowTransform.rotation);
        Destroy(gameObject);
    }
}
