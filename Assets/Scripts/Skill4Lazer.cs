using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill4Lazer : MonoBehaviour
{
    public float damage;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(8f * Time.deltaTime, 0f, 0f);
        if (transform.position.x > 16f)
        {
            gameObject.SetActive(false);
            transform.position = new Vector2(-16f, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("TekiChar"))
            return;

        var target = collision.GetComponent<TekiChar>();
        target.charCurrentHP -= damage;
    }

}
