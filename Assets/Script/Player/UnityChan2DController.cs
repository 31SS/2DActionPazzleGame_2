using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(BoxCollider2D))]
//Playerの動作を統括するスクリプト
public class UnityChan2DController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float jumpPower = 100f;
    public Vector2 backwardForce = new Vector2(-4.5f, 5.4f);

    public LayerMask whatIsGround;
    public LayerMask whatIsBomb;

    private PlayerInput _playerInput;
    private PlayerMover _playerMover;
    private Animator m_animator;
    private BoxCollider2D m_boxcollier2D;
    [SerializeField]private Rigidbody2D m_rigidbody2D;
    private bool m_isGround;
    private bool m_isBomb;
    private bool m_jumpFlag;
    private const float m_centerY = 1.5f;

    private State m_state = State.Normal;

    void Reset()
    {
        Awake();

        // UnityChan2DController
        maxSpeed = 10f;
        jumpPower = 1000;
        backwardForce = new Vector2(-4.5f, 5.4f);
        whatIsGround = 1 << LayerMask.NameToLayer("Standable");
        whatIsBomb = 1 << LayerMask.NameToLayer("Bomb");

        // Transform
        transform.localScale = new Vector3(1, 1, 1);

        // Rigidbody2D
        m_rigidbody2D.gravityScale = 3.5f;
        m_rigidbody2D.freezeRotation = true;

        // BoxCollider2D
        m_boxcollier2D.size = new Vector2(1, 2.5f);
        m_boxcollier2D.offset = new Vector2(0, -0.25f);

        // Animator
        m_animator.applyRootMotion = false;
    }

    void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_boxcollier2D = GetComponent<BoxCollider2D>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        _playerInput = new PlayerInput();
        _playerMover = new PlayerMover(m_rigidbody2D);
    }

    void Update()
    {

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, duration, false);
        if (m_state != State.Damaged)
        {
            _playerInput.Inputting();
            //左右の向き
            if (Mathf.Abs(_playerInput.X) > 0)
            {
                Quaternion rot = transform.rotation;
                transform.rotation = Quaternion.Euler(rot.x, Mathf.Sign(_playerInput.X) == 1 ? 0 : 180, rot.z);
            }
            _playerMover.Move(1, maxSpeed, _playerInput.X, m_isGround, m_animator);
            if ((_playerInput.Jump && m_isGround) || m_jumpFlag)//Bombを踏んだ時かSpaceキーを押したときにジャンプ
            {
                _playerMover.Jump(m_animator, jumpPower);
                if (m_jumpFlag)
                {
                    m_jumpFlag = false;
                }
            }
        }
    }

    //地面に立っているか判別する処理
    void FixedUpdate()
    {
        Vector2 pos = transform.position;
        Vector2 groundCheck = new Vector2(pos.x, pos.y - (m_centerY * transform.localScale.y));
        Vector2 groundArea = new Vector2(m_boxcollier2D.size.x * 0.49f, 0.05f);

        m_isGround = Physics2D.OverlapArea(groundCheck + groundArea, groundCheck - groundArea, whatIsGround);
        m_animator.SetBool("isGround", m_isGround);
    }

    //SoilBlockや拾えるアイテムに触れた時の処理
    private void OnTriggerEnter2D(Collider2D other)
    {
        var breakableBlock = other.GetComponent<IBreakable>();
        if (breakableBlock != null)
        {
            breakableBlock.Breaked();
        }
        var pickupable = other.GetComponent<IPickupable>();
        if (pickupable != null)
        {
            // pickupable.PickedUp(this);
        }
    }

    //ダメージを受けるオブジェクト(HierarchyのSpikeHead)に触れた時
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "DamageObject" && m_state == State.Normal)
        {
            m_state = State.Damaged;
            StartCoroutine(INTERNAL_OnDamage());
        }
    }

    //踏めるオブジェクト(Bomb)に上から触れた時
    private void OnCollisionEnter2D(Collision2D other)
    {
        var stepedOnable = other.gameObject.GetComponent<ISteponable>();
        Vector2 pos = transform.position;
        Vector2 bombCheck = new Vector2(pos.x, pos.y - (m_centerY * transform.localScale.y));
        Vector2 bombArea = new Vector2(m_boxcollier2D.size.x * 0.48f, 0.05f);
        m_isBomb = Physics2D.OverlapArea(bombCheck + bombArea, bombCheck - bombArea, whatIsBomb);
        if (stepedOnable != null && m_isBomb && m_rigidbody2D.velocity.y <= 0f)
        {
            if (Input.GetButton("Jump"))
            {
                M_JumpFlag = true;
                SendMessage("Jump", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                _playerMover.Jump(m_animator, jumpPower);
            }
            // stepedOnable.StepedOn(this);
        }
    }

    IEnumerator INTERNAL_OnDamage()
    {
        m_animator.Play(m_isGround ? "Damage" : "AirDamage");
        m_animator.Play("Idle");

        SendMessage("OnDamage", SendMessageOptions.DontRequireReceiver);

        m_rigidbody2D.velocity = new Vector2(transform.right.x * backwardForce.x, transform.up.y * backwardForce.y);

        yield return new WaitForSeconds(.2f);

        while (m_isGround == false)
        {
            yield return new WaitForFixedUpdate();
        }
        m_animator.SetTrigger("Invincible Mode");
        m_state = State.Invincible;
    }

    void OnFinishedInvincibleMode()
    {
        m_state = State.Normal;
    }

    enum State
    {
        Normal,
        Damaged,
        Invincible,
    }

    public Animator M_Animator
    {
        get { return m_animator; }
        private set { m_animator = value; }
    }

    public bool M_JumpFlag
    {
        get { return m_jumpFlag; }
        set { m_jumpFlag = value; }
    }
}
