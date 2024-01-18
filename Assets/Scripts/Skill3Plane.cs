using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3Plane : MonoBehaviour
{
    public GameObject boomPrefabs;
    // Start is called before the first frame update

    private void OnEnable()
    {
        StartCoroutine(PlaneCorutine());
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(7f * Time.deltaTime, 0f, 0f);

    }

    IEnumerator PlaneCorutine()
    {
        while (true)
        {
            Instantiate(boomPrefabs, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);

            if (transform.position.x > 16f)
            {
                gameObject.SetActive(false);
                transform.position = new Vector2(-16f, transform.position.y);
            }
        }
    }
}
