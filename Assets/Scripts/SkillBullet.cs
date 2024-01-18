using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBullet : MonoBehaviour
{
    public float damage;
    public GameObject effectPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("TekiChar"))
            return;

        
        var target = collision.GetComponent<TekiChar>();
        var pos = new Vector2(target.transform.position.x, -2.8f);
        Instantiate(effectPrefabs, pos,Quaternion.identity);
        target.currentHP -= damage;
        Destroy(gameObject);
    }
}
