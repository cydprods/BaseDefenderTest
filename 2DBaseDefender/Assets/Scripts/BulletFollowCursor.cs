using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletFollowCursor : MonoBehaviour
{
    public int Speed = 5;
    private Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, mousePos, Speed * Time.deltaTime);
        if (new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y) == mousePos)
        {
            Destroy(gameObject);
        }
    }
}
