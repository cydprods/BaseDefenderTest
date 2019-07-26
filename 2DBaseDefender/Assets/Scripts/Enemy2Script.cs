using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Script : MonoBehaviour
{
    
    public int healthPoints = 40;
    public int damageAmount = 10;
    public float Speed = 3.5f;
    public bool attackPosition = false;

    private GameObject OtherGO;
    private PlayerScript Other;
    // Start is called before the first frame update
    void Awake()
    {
        OtherGO = GameObject.Find("Player");
        Other = OtherGO.gameObject.GetComponent<PlayerScript>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 2 + ((float)Math.Sin(Time.time * 5) * 0.5f), transform.position.z);
        if (attackPosition == false)
        {
            transform.Translate(Vector2.left * Time.deltaTime * Speed);
        }
        else
        {
            Debug.Log("Attacking");
        }
        if (healthPoints <= 0 || transform.position.x <= -5.5)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            healthPoints -= 45;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("MeleeAttack"))
        {
            attackPosition = true;
            InvokeRepeating("HittingWall", 0.7f, 1f);
        }
    }
    private void HittingWall()
    {
        if (attackPosition == true)
        {
            Other.health -= damageAmount;
            Other.healthBar.fillAmount =Other.health;
        }
    }
}
