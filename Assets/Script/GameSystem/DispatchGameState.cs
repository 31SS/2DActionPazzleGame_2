using UnityEngine;

public class DispatchGameState : MonoBehaviour
{
    [SerializeField]
    private GameManager.GameState nowGameState;
    private void Start()
    {
        GameManager.Instance.dispatch(nowGameState);
    }
}