using DefaultNamespace;
using UnityEngine;

public class Move : MonoBehaviour, IMove
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float jumpStrength;
    
    private Rigidbody2D _playerRigidbody;

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
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CheckGround())
        {
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, jumpStrength);
        }
    }

    private void Update()
    {
        MoveObject();
        Jump();
        //test for jumping
        Debug.DrawLine(new Vector2(transform.position.x, transform.position.y), Vector2.down, Color.red);
    }
}
