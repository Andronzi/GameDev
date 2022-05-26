using DefaultNamespace;
using UnityEngine;

public class Move : MonoBehaviour, IMove
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float jumpStrength;
    
    private Rigidbody2D _playerRigidbody;

    public Animator animator;
    private bool _grounded = true;
    
    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();

        if (_playerRigidbody == null)
        {
            Debug.LogError("Rigidbody2D has been missed for Player");
        }
    }
    
    private bool CheckGround()
    {
        var transformProp = transform;
        var position = transformProp.position;
        var groundHit = Physics2D.Raycast(new Vector2(position.x, position.y), Vector2.down, 0.001f);
        
        if (groundHit.collider == null) return false;
        return groundHit.collider.CompareTag("Ground");
    }
    
    public void MoveObject()
    {
        var horizontal = Input.GetAxis("Horizontal");
        _playerRigidbody.velocity = new Vector2(horizontal * movementSpeed, _playerRigidbody.velocity.y);
        animator.SetFloat("ControllerSpeed", System.Math.Abs(horizontal * movementSpeed));
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CheckGround())
        {
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, jumpStrength);
        }

        _grounded = CheckGround();
        animator.SetBool("Grounded", _grounded);
        //for debug jumping
        //Debug.Log(_grounded);
    }

    private void Update()
    {
        MoveObject();
        Jump();

        //debug ray finding
        Debug.DrawLine(new Vector2(transform.position.x, transform.position.y), Vector2.down, Color.red);
    }
}
