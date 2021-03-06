using UnityEngine;
public class BombAnimation
{
    private readonly Animator _animator;

    public BombAnimation(Animator animator)
    {
        _animator = animator;
    }
    //Playerに踏まれたら爆発Animation開始
    public void ExplosionFlag()
    {
        _animator.SetTrigger("Explode");
    }
    //Animationの遷移状況を確認しつつ爆発処理の実行を判別
    public bool DestroyJudge()
    {
        var animInfo = _animator.GetAnimatorTransitionInfo(0);
        if (animInfo.fullPathHash == Animator.StringToHash("Base Layer.Action -> Exit"))
        {
            return true;
        }
        return false;
    }
}
