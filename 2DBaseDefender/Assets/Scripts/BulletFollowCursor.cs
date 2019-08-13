using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletFollowCursor : MonoBehaviour
{
    public float Speed = 5;
    private Vector2 mousePos;
    /*
    private GameObject OtherGO;
    private PlayerScript Other;
    private float SpeedValue;
    private float SizeValue;
    */
    // Start is called before the first frame update
    void Start()
    {
        //OtherGO = GameObject.Find("Player");
        //Other =  OtherGO.GetComponent<PlayerScript>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //float.TryParse(Other.SpeedText.text, out SpeedValue);
        //Speed = SpeedValue;
        //float.TryParse(Other.SizeText.text, out SizeValue);
        //transform.localScale = new Vector3(SizeValue, SizeValue, 1);
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
