using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskHeartController : BaseHeart, IGetableMaskHeart
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotMaskHeart()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Playing_Heart0)
        {
            GameManager.Instance.dispatch(GameManager.GameState.Playing_Heart1);
            Destroy(gameObject);
            return;
        }
        GameManager.Instance.dispatch(GameManager.GameState.Clear);
        Destroy(gameObject);
    }
}