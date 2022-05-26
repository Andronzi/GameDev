using UnityEngine;

public class Hero : MonoBehaviour, IMovablePlayer
{
    [SerializeField]
    private float movementSpeed;
    private Rigidbody2D _playerRigidbody;
    public float Health { get; set; } = 100f;
    public float MaxHealth => 100f;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();

        if (_playerRigidbody == null)
        {
            Debug.LogError("Rigidbody2D has been missed for Player");
        }
    }
    
    public void Move()
    {
        var horizontal = Input.GetAxis("Horizontal");
        _playerRigidbody.velocity = new Vector2(horizontal * movementSpeed, _playerRigidbody.velocity.y);
    }
}
