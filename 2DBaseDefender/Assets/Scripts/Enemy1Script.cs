using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Script : MonoBehaviour
{
    
    public int healthPoints = 100;
    public int damageAmount = 10;
    public int mpValue = 10;             //The Mana value in points that this enemy is worth.
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
            //Debug.Log("Attacking");
        }
        if (healthPoints <= 0 || transform.position.x <= -5.5)
        {
            Destroy(gameObject);
            //Lets now give to the player the value points this enemy is worth.
            GiveMP();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            
            healthPoints -= 45;
            Destroy(other.gameObject);
            //Debug.Log(healthPoints);
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

    // This function will give to the player the amount of points this enemy is worth and then will get the percentage to pass it to the image component in the mpBar fillAmount method.
    private void GiveMP()
    {
        float currentValue; //Needed to store the math division we are going to do similar to the one in HittingWall
        Other.mpPoints += mpValue;
        //fillAmount only accept values ranging between 0 to 1, so lets check that we aren't going to pass a value greater than 1.
        currentValue = Other.mpPoints / Other.mana;

        if (currentValue >= 1.0f)
        {
            currentValue = 1.0f;
            Other.mpPoints = (int)Other.mana;
            Other.mpBar.fillAmount = currentValue;
        }
        else
        {
            Other.mpBar.fillAmount = Other.mpPoints / Other.mana;
        }

        //Debug.Log("fillAmount = " + Other.mpBar.fillAmount);
        //Debug.Log("Total points = " + Other.mpPoints);
    }
}
