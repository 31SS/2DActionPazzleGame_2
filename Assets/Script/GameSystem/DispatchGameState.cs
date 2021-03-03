using UnityEngine;

public class DispatchGameState : MonoBehaviour
{
    [SerializeField]
    private GameManager.GameState nowGameState;
    private void Awake()
    {
        GameManager.Instance.dispatch(nowGameState);
    }
}
