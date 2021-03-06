using UnityEngine;

//Itemが消滅したときにエフェクトを出す
public class EffectController : MonoBehaviour
{
    [SerializeField] private Animator m_animator;

    [SerializeField] private string endTransitionName;
    // Update is called once per frame
    void Update()
    {
        if (m_animator.GetAnimatorTransitionInfo(0).IsName(endTransitionName))
        {
            Destroy(gameObject);
        }
    }
}
