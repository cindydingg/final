using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] Transform ObjectToFollow;
    [SerializeField] Transform[] cameraBounds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(ObjectToFollow.position.x, ObjectToFollow.position.y, transform.position.z);

        Camera cam = Camera.main;

        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 bottomRight = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));
        Vector3 topLeft = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.nearClipPlane));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        float newX = transform.position.x;
        float newY = transform.position.y;
        if(bottomLeft.x < cameraBounds[0].position.x) {
            newX += cameraBounds[0].position.x - bottomLeft.x;
        } else if (topRight.x > cameraBounds[1].position.x) {
            newX += cameraBounds[1].position.x - topRight.x;
        }
        if(bottomLeft.y < cameraBounds[0].position.y) {
            newY += cameraBounds[0].position.y - bottomLeft.y;
        } else if (topRight.y > cameraBounds[1].position.y) {
            newY += cameraBounds[1].position.y - topRight.y;
        }
        transform.position = new Vector3(newX, newY, transform.position.z);

    }
}
