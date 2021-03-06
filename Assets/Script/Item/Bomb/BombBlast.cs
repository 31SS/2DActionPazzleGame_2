using KanKikuchi.AudioManager;
using UnityEngine;
//Bombの爆風処理
public class BombBlast : MonoBehaviour, IDamageable
{
    private BombAnimation _bombAnimation;

    private void Awake()
    {
        _bombAnimation = new BombAnimation(GetComponent<Animator>());
    }

    private void Start()
    {
        SEManager.Instance.Play(SEPath.EXPLOSION);
    }

    //爆風のAnimationが終わったらObjectを破壊
    private void Update()
    {
        if (_bombAnimation.DestroyJudge())
        {
            Destroy(gameObject);
        }
    }
    
    //爆風で壊せるものに当たったらメソッドを呼び出す
    private void OnTriggerEnter2D(Collider2D other)
    {
        var breakable = other.GetComponent<IBreakable>();
        if (breakable != null)
        {
            breakable.Breaked();
            
        }
    }

    public void ApplyDamage()
    {
        //do nothing
    }
}
