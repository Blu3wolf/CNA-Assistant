using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class MapCameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float panspeed = 10.0f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            panspeed = 25.0f;
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector2 position = transform.position;
        position.x += panspeed * horizontal * Time.deltaTime;
        position.y += panspeed * vertical * Time.deltaTime;

        transform.position = position;
    }
}
