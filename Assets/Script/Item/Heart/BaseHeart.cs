using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//HeartObjectの基底クラス
public class BaseHeart : MonoBehaviour
{
    //Heartが取得されたときに生成するEffect
    [SerializeField] protected GameObject getItemEffect;
}
