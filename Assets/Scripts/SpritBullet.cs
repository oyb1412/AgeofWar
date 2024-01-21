using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpritBullet : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D col;
    float damage;
    towerType bulletType;
    public GameObject bloodPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        float ran = Random.Range(-1f, 1f);
        rb.AddForce(new Vector3(ran, 3f), ForceMode2D.Impulse);
        StartCoroutine(ColCorutine());
    }
    private void Update()
    {
        if (transform.position.y <= -4.2f)
            Destroy(gameObject);
    }
    IEnumerator ColCorutine()
    {
        col.enabled = false;
        yield return new WaitForSeconds(1f);
        col.enabled = true;
    }
    public void Init(float attackDamage,int towerType)
    {
        damage = attackDamage * 0.5f;
        bulletType = (towerType)towerType;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
            return;


        switch (bulletType)
        {
            case towerType.MIKATA:
                if (collision.CompareTag("TekiChar"))
                {
                    var target = collision.GetComponent<TekiChar>();
                    var trans = Instantiate(bloodPrefabs, null);
                    trans.transform.position = target.transform.position;
                    target.charCurrentHP -= damage;
                    Destroy(gameObject);
                }
                break;
            case towerType.TEKI:
                if (collision.CompareTag("MikataChar"))
                {
                    var target = collision.GetComponent<MikataChar>();
                    var trans = Instantiate(bloodPrefabs, null);
                    trans.transform.position = target.transform.position;
                    target.charCurrentHP -= damage;
                    Destroy(gameObject);
                }
                break;
        }
    }
}
