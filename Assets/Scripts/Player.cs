using UnityEngine;

public class Player : Creature
{
    [SerializeField] private float jumpHeight;
    [SerializeField] private bool isGrounded = true;
    public PlayerState playerState;
    public bool isCanToRandomFloor;
    
    public enum PlayerState
    {
        Default,
        Die
    }
    
    private Rigidbody2D _rb;
    //private Animator _animator;
    private float _velocityY;
    private float _velocityX;
    
    //private static readonly int IsMove = Animator.StringToHash("is-move");
    //private static readonly int IsGrounded = Animator.StringToHash("is-grounded");
    //private static readonly int IsInteract = Animator.StringToHash("is-interact");
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        //_animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        isGrounded = true;
        //_animator.SetBool(IsGrounded, true);
    }
    
    public void Move(Vector2 direction)
    {
        if (direction.x != 0)
        {
            _rb.velocity = new Vector2(direction.x * Speed + Time.fixedDeltaTime,  _rb.velocity.y);
            transform.localScale = new Vector3(direction.x > 0 ? 1 : -1, 1, 1);
            
            //_animator.SetBool(IsMove, true);
        }
        else
        {
            _rb.velocity = new Vector2(direction.x, _rb.velocity.y);
            //_animator.SetBool(IsMove, false);
        }
    }

    public void Jump()
    {
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
        //_animator.SetBool(IsGrounded, isGrounded);
    }
}
