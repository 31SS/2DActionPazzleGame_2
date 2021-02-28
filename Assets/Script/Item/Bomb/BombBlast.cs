using UnityEngine;
//Bombの爆発処理
public class BombBlast : MonoBehaviour, IDamageable
{
    private BombAnimation _bombAnimation;

    private void Awake()
    {
        _bombAnimation = new BombAnimation(GetComponent<Animator>());
    }

    private void Update()
    {
        if (_bombAnimation.DestroyJudge())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var breakableBlock = other.GetComponent<IBreakable>();
        if (breakableBlock != null)
        {
            breakableBlock.Breaked();
            
        }
    }

    public void ApplyDamage()
    {
        
    }
}
