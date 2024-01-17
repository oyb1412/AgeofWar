using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovemnet : MonoBehaviour
{
    public float cameraSpeed;
    public float rimitLeft;
    public float rimitRight;
    

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        var mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (transform.position.x <= rimitLeft)
        {
            if (mousepos.x <= rimitLeft)
                mousepos.x = rimitLeft;
        }
        else if (transform.position.x >= rimitRight)
        {
            if (mousepos.x >= rimitRight)
                mousepos.x = rimitRight;
        }

        if(mousepos.y < 2f)
        transform.position = Vector3.Lerp(transform.position, new Vector3(mousepos.x, 0f, -10f), cameraSpeed * Time.deltaTime);
    }
}
