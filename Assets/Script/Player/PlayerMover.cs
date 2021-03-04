using KanKikuchi.AudioManager;
using UnityEngine;
//Playerの移動とジャンプを行う処理
public class PlayerMover
{
    [SerializeField]private Rigidbody2D m_rigidbody2D;

    public PlayerMover(Rigidbody2D rigidbody2D)
    {
        m_rigidbody2D = rigidbody2D;
    }

    public void Move(int reverseFlag,float maxSpeed,float move, bool m_isGround, Animator m_animator)
    {
        m_rigidbody2D.velocity = new Vector2(reverseFlag * move * maxSpeed, m_rigidbody2D.velocity.y);
        m_animator.SetFloat("Horizontal", move);
        m_animator.SetFloat("Vertical", m_rigidbody2D.velocity.y);
        m_animator.SetBool("isGround", m_isGround);
    }
    public void Jump(Animator m_animator, float jumpPower)
    {
        m_animator.SetTrigger("Jump");
        m_rigidbody2D.AddForce(Vector2.up * jumpPower);
        SEManager.Instance.Play(SEPath.JUMP, volumeRate: 0.5f);
    }
}
