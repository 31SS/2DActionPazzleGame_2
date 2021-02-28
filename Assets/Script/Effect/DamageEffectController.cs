using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffectController : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    // Update is called once per frame
    void Update()
    {
        if (m_animator.GetAnimatorTransitionInfo(0).IsName("DamageEffect -> Exit"))
        {
            Destroy(gameObject);
        }
    }
}
