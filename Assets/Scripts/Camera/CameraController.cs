using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float offsetX = 0.5f;
    [SerializeField]
    private float offsetY = 0.2f;
    private Transform _player;
    private Vector3 _position;
    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _position = transform.position;
    }

    private void Update()
    {
        Vector3 delta = Vector3.zero;

        float deltaX = _player.position.x - transform.position.x;
        if (deltaX > offsetX || deltaX < -offsetX)
        {
            if (transform.position.x < _player.position.x)
            {
                delta.x = deltaX - offsetX;
            }
            else
            {
                delta.x = deltaX + offsetX;
            }
        }
    
        float deltaY = _player.position.y - transform.position.y;
        if (deltaY > offsetY || deltaY < -offsetY)
        {
            if (transform.position.y < _player.position.y)
            {
                delta.y = deltaY - offsetY;
            }
            else
            {
                delta.y = deltaY + offsetY;
            }
        }

        _position += new Vector3(delta.x, delta.y, 0);
    }
}
