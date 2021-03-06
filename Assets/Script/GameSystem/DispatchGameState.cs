using UnityEngine;

//Sceneに遷移した直後にGameManagerのStateを切り替える
public class DispatchGameState : MonoBehaviour
{
    [SerializeField]
    private GameManager.GameState nowGameState;
    private void Start()
    {
        GameManager.Instance.dispatch(nowGameState);
    }
}