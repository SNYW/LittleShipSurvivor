using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private PlayerUnit _ship;

    public Vector3 offset;
    
    void Start()
    {
        _ship = FindObjectOfType<PlayerUnit>();
    }
    
    void Update()
    {
        transform.position = _ship.transform.position + offset;
    }
}
