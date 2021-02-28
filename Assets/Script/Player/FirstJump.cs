using UnityEngine;
//IncreaceItemによって生成されたプレイヤ―が、生成時にSpaceキーを押しっぱなしにすることで、
//一度だけ空中でジャンプさせる
public class FirstJump : MonoBehaviour
{
    private PlayerMover _playerMover;
    private UnityChan2DController _unityChan2DController;
    [SerializeField]private float jumpPower = 1000f;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _playerMover = new PlayerMover(_rigidbody2D);
        _unityChan2DController = GetComponent<UnityChan2DController>();
    }
    void Start()
    {
        if (Input.GetButton("Jump"))
        {
            _unityChan2DController.M_JumpFlag = true;
        }
    }
}
