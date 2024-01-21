using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharBullet : MonoBehaviour
{
    int type;
    float damage;
    public GameObject bloodPrefab;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public void Init(float damage, int type)
    {
        this.damage = damage;
        this.type = type;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(3f * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(type)
        {
            case (int)charType.MIKATA:
                if (collision.CompareTag("TekiChar"))
                {
                    Debug.Log("1");
                    var target = collision.GetComponent<TekiChar>();
                    target.charCurrentHP -= damage;
                    var trans = Instantiate(bloodPrefab, null).transform;
                    trans.position = target.transform.position;
                    Destroy(gameObject);
                }
                break;
            case (int)charType.TEKI:
                if (collision.CompareTag("MikataChar"))
                {
                    var target = collision.GetComponent<MikataChar>();
                    target.charCurrentHP -= damage;
                    var trans = Instantiate(bloodPrefab, null).transform;
                    trans.position = target.transform.position;
                    Destroy(gameObject);
                }
                break;
        }

    }
}
