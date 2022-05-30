using DefaultNamespace;
using UnityEngine;

public class Move : MonoBehaviour, IMove
{
    [SerializeField]
    private float movementSpeed;
    private Rigidbody2D _playerRigidbody;
    private Transform _transform;
    
    public Animator animator;
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Idle = Animator.StringToHash("Idle");

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _transform = transform;

        if (_playerRigidbody == null)
        {
            Debug.LogError("Rigidbody2D has been missed for Player");
        }
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

        if (horizontal != 0 || vertical != 0)
        {
            animator.SetTrigger(Run); 
        }
        else if(horizontal == 0 && vertical == 0)
        {
            animator.SetTrigger(Idle); 
        }
    }
    
    private void Update()
    {
        MovePlayer();

        //debug ray finding
        // Debug.DrawLine(new Vector2(transform.position.x, transform.position.y), Vector2.down, Color.red);
    }
}
