using DefaultNamespace;
using UnityEngine;

public class Move : MonoBehaviour, IMove
{
    [SerializeField]
    private float movementSpeed;

    private Rigidbody2D _playerRigidbody;

    public Animator animator;
    private static readonly int ControllerSpeed = Animator.StringToHash("ControllerSpeed");
    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();

        if (_playerRigidbody == null)
        {
            Debug.LogError("Rigidbody2D has been missed for Player");
        }
    }
    
    public void MoveObject()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var transformVariable = transform;

        switch (horizontal)
        {
            case < 0:
                transformVariable.eulerAngles = new Vector2(transformVariable.eulerAngles.x, 180);
                break;
            case > 0:
                transformVariable.eulerAngles = new Vector2(transformVariable.eulerAngles.x, 0);
                break;
            default:
            {
                var eulerAngles = transformVariable.eulerAngles;
                eulerAngles = new Vector2(eulerAngles.x, eulerAngles.y);
                transformVariable.eulerAngles = eulerAngles;
                break;
            }
        }

        _playerRigidbody.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
        animator.SetFloat(ControllerSpeed, System.Math.Abs(horizontal * movementSpeed));
        //Debug.Log(System.Math.Abs(horizontal * movementSpeed));
    }
    
    private void Update()
    {
        MoveObject();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger(Attack);
        }

        //debug ray finding
        // Debug.DrawLine(new Vector2(transform.position.x, transform.position.y), Vector2.down, Color.red);
    }
}
