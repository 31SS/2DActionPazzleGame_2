using UnityEngine;
//キーボードからのPlayerの入力処理
public class PlayerInput : IPlayerInput
{
    [SerializeField]private float _x;
    [SerializeField]private bool _jump;

    public void Inputting()
    {
        _x = Input.GetAxis("Horizontal");
        _jump = Input.GetButtonDown("Jump");
    }

    public float X
    {
        get { return this._x; }
        set { this._x = value; }
    }

    public bool Jump
    {
        get { return this._jump; }
        set { this._jump = value; }
    }
}
