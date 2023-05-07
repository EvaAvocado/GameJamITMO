using UnityEngine;

public class Player : Creature
{
    [SerializeField] private float jumpHeight;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private Animator _animator;

    public PlaySoundsComponent pentaSounds;
    
    public PlayerState playerState;
    public bool hasSecondLife;
    public bool isCanToRandomFloor;

    public enum PlayerState
    {
        Default,
        Die
    }

    private Rigidbody2D _rb;

    private bool _isCanMove = true;
    private float _velocityY;
    private float _velocityX;
    
    private static readonly int IsMove = Animator.StringToHash("is-move");
    private static readonly int IsGrounded = Animator.StringToHash("is-grounded");
    private static readonly int IsTearing = Animator.StringToHash("is-tearing");
    private static readonly int IsThrow = Animator.StringToHash("is-throw");
    private static readonly int IsPee= Animator.StringToHash("is-pee");
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        //_animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        isGrounded = true;
        _animator.SetBool(IsGrounded, true);
    }

    public void SetSecondLife(bool status)
    {
        hasSecondLife = status;
    }
    
    public void Move(Vector2 direction)
    {
        if (!_isCanMove) return;
        
        if (direction.x != 0)
        {
            _rb.velocity = new Vector2(direction.x * Speed + Time.fixedDeltaTime,  _rb.velocity.y);
            transform.localScale = new Vector3(direction.x > 0 ? 1 : -1, 1, 1);
            
            _animator.SetBool(IsMove, true);
        }
        else
        {
            _rb.velocity = new Vector2(direction.x, _rb.velocity.y);
            _animator.SetBool(IsMove, false);
        }
    }

    public void Jump()
    {
        if (!_isCanMove) return;
        
        if (isGrounded) // || isPlatform
        {
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * _rb.gravityScale));
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    public void SetGrounded(bool status)
    {
        isGrounded = status;
        //isPlatform = false;
        _animator.SetBool(IsGrounded, isGrounded);
    }

    public void SomethingBroken(Thing.ThingName thingName)
    {
        _isCanMove = false;

        if (thingName == Thing.ThingName.Paper)
        {
            _animator.SetTrigger(IsTearing);
        }
        else if (thingName == Thing.ThingName.Bottle)
        {
            _animator.SetTrigger(IsThrow);
        } else if (thingName == Thing.ThingName.Bed)
        {
            _animator.SetTrigger(IsPee);
        }
    }

    public void PentaAnim()
    {
        _isCanMove = false;
        _animator.SetTrigger(IsTearing);
        pentaSounds.PlayRandomSoundComponent();
    }

    public void SetIsCanMoveTrue()
    {
        _isCanMove = true;
    }
}
