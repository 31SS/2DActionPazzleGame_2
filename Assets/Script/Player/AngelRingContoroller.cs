using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelRingContoroller : MonoBehaviour
{
    [SerializeField]private Animator m_animator;

    private void Update()
    {
        var stateInfo = m_animator.GetAnimatorTransitionInfo(0);
        if (stateInfo.IsName("AngelRingMotion -> Exit"))
        {
            Destroy(gameObject);
        }
    }
}
