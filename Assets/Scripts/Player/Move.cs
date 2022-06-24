using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private Rigidbody2D _playerRigidbody;
    private Transform _transform;
    private Vector2 _movement;
    
        public Animator animator;
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Idle = Animator.StringToHash("Idle");

    private void Start()
    {
        _transform = transform;
    }

    public void MovePlayer()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        switch (horizontal)
        {
            case < 0:
                _transform.eulerAngles = new Vector2(_transform.eulerAngles.x, 180);
                break;
            case > 0:
                _transform.eulerAngles = new Vector2(_transform.eulerAngles.x, 0);
                break;
            default:
            {
                var eulerAngles = _transform.eulerAngles;
                eulerAngles = new Vector2(eulerAngles.x, eulerAngles.y);
                _transform.eulerAngles = eulerAngles;
                break;
            }
        }
        
        _playerRigidbody.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
    
        //try for new solution
    }

    private void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
        
        switch (_movement.x)
        {
            case < 0:
                _transform.eulerAngles = new Vector2(_transform.eulerAngles.x, 180);
                break;
            case > 0:
                _transform.eulerAngles = new Vector2(_transform.eulerAngles.x, 0);
                break;
            default:
            {
                var eulerAngles = _transform.eulerAngles;
                eulerAngles = new Vector2(eulerAngles.x, eulerAngles.y);
                _transform.eulerAngles = eulerAngles;
                break;
            }
        }
        
        // Debug.Log(_movement.x + " " + _movement.y);
        animator.SetFloat("RunValue", Mathf.Abs(_movement.x) + Mathf.Abs(_movement.y));
    }

    private void FixedUpdate()
    {
        _playerRigidbody.MovePosition(_playerRigidbody.position + _movement * movementSpeed * Time.fixedDeltaTime);
    }
}