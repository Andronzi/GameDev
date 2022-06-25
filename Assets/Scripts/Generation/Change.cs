using UnityEngine;

public class Change : MonoBehaviour
{
    public Vector3 cameraChangePos;
    public Vector3 plauerChangePos;
    private Camera cam;
    void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position += plauerChangePos;
            cam.transform.position += cameraChangePos;
        }
    }

}
