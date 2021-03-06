using UnityEngine;
using KanKikuchi.AudioManager;
using UniRx;

public class BlueManController : BasePlayer
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
        //入力処理
        _playerInput.Inputting();
        //接地判定
        m_isGround = m_rigidbody2D.IsTouching(_groundFilter2D);
        
        //横移動処理、MaskManとは違って矢印キーの方向に走る
        _playerMover.Move(NON_REVERSE, playerParameter.RUN_SPEED, _playerInput.X, m_isGround, m_animator);
        
        if (m_isGround)
        {
            //ジャンプ処理
            if (_playerInput.Jump)
            {
                _playerMover.Jump(m_animator, playerParameter.JUMP_POWER);
                StateProcessor.State.Value = StateAir;
            }
            //キーを押した方向にオブジェクトの向きを変える
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
  
  //TriggerEnter2Dで触れたものがBlueHeartどうか判別する
  protected override void OnTriggerEnter2D(Collider2D other)
  {
      base.OnTriggerEnter2D(other);
      var isGetableBlueHeart = other.GetComponent<IGetableBlueHeart>();
      if (isGetableBlueHeart != null)
      {
          isGetableBlueHeart.GotBlueHeart();
          SEManager.Instance.Play(SEPath.GET_ITEM, volumeRate:0.5f);
      }
  }
}
