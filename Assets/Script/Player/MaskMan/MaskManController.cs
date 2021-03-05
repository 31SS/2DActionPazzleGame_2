using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterState;
using KanKikuchi.AudioManager;
using UniRx;

public class MaskManController: BasePlayer
{
    private void Start()
    {
        //ステートの値が変更されたら実行処理を行うようにする
        StateProcessor.State
            .Where(_ => StateProcessor.State.Value.GetStateName() != _prevStateName)
            .Subscribe(_ =>
            {
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

        _playerMover.Move(REVERSE, playerParameter.RUN_SPEED, _playerInput.X, m_isGround, m_animator);
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
        var isGetableMaskHeart = other.GetComponent<IGetableMaskHeart>();
        if (isGetableMaskHeart != null)
        {
            isGetableMaskHeart.GotMaskHeart();
            SEManager.Instance.Play(SEPath.GET_ITEM, volumeRate:0.5f);
        }
    }
}