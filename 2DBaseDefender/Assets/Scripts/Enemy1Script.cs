using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Script : MonoBehaviour
{
    
    public int healthPoints = 100;
    public int damageAmount = 10;
    public float Speed = 2f;
    public bool attackPosition = false;
    public Animator animator;

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
        Other.GetComponent<PlayerScript>();
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
            Debug.Log(healthPoints);
        }
        if (other.CompareTag("MeleeAttack"))
        {
            attackPosition = true;
            animator.SetTrigger("Enemy1Attacking");
            InvokeRepeating("HittingWall", 0.7f, 0.8f);
        }
    }
    private void HittingWall()
    {
        if (attackPosition == true)
        {
            Other.hpWall -= damageAmount;
            // You need to make this operation (division) because it gives a percentage which is going to be given as a float value that the fillAmount method uses...
            Other.healthBar.fillAmount = Other.hpWall / Other.health;
        }
    }
}
