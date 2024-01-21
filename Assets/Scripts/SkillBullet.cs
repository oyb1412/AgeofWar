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
        var trans = Instantiate(effectPrefabs, null);
        trans.transform.position = transform.position;
        target.charCurrentHP -= damage;
        Destroy(gameObject);
    }
}
