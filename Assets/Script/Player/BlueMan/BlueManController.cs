using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterState;
using UniRx;

public class BlueManController : BasePlayer
{
    // private void Awake()
    // {
    //     StateProcessor.State.Value = StateIdle;
    //     StateIdle.ExecAction = Idle;
    //     StateRun.ExecAction = Run;
    //     StateAir.ExecAction = Air;
    //     StateAttack.ExecAction = Attack;
    //
    //     _playerInput = new PlayerInput();
    //     _playerMover = new PlayerMover(m_rigidbody2D);
    // }

    private void Start()
    {
        //ステートの値が変更されたら実行処理を行うようにする
        StateProcessor.State
            .Where(_ => StateProcessor.State.Value.GetStateName() != _prevStateName)
            .Subscribe(_ =>
            {
                Debug.Log("Now State:" + StateProcessor.State.Value.GetStateName());
                _prevStateName = StateProcessor.State.Value.GetStateName();
                StateProcessor.Execute();
            })
            .AddTo(this);
    }

    // Update is called once per frame
  protected override void Update()
    {
        _playerInput.Inputting();
        m_isGround = m_rigidbody2D.IsTouching(_groundFilter2D);

        _playerMover.Move(NON_REVERSE, playerParameter.RUN_SPEED, _playerInput.X, m_isGround, m_animator);
        if (m_isGround)
        {
            if (_playerInput.Jump == true)
            {
                _playerMover.Jump(m_animator, playerParameter.JUMP_POWER);
                StateProcessor.State.Value = StateAir;
            }
            else if (Mathf.Abs(_playerInput.X) > 0)
            {
                StateProcessor.State.Value = StateRun;
                var rot = transform.rotation;
                transform.rotation = Quaternion.Euler(rot.x, Mathf.Sign(_playerInput.X) == 1 ? 0 : 180, rot.z);
            }
            else
            {
                StateProcessor.State.Value = StateIdle;
            }
        }
        base.Update();
    }
  
  protected override void OnTriggerEnter2D(Collider2D other)
  {
      base.OnTriggerEnter2D(other);
      other.GetComponent<IGetableBlueHeart>()?.GotBlueHeart();
  }
}
