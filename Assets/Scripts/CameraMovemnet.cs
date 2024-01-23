using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovemnet : MonoBehaviour
{
    public float cameraSpeed;
    public float rimitLeft;
    public float rimitRight;
    Vector3 currentPos;

 
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;
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

        if(mousepos.y < 1f)
        transform.position = Vector3.Lerp(transform.position, new Vector3(mousepos.x, 0f, -10f), cameraSpeed * Time.deltaTime);
    }

    public void ScreenShake()
    {
        currentPos = transform.position;
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 2f)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * 0.1f;

            transform.position = currentPos + shakeOffset;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

    }
}
