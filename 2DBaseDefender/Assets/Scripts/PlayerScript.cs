using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject Enemy1Prefab;
    public GameObject Enemy2Prefab;
    public GameObject ShootingPosition;
    public Image healthBar;
    public Image mpBar;
    public bool ShootAvailable = true;
    public float ReloadTime = 0.5f;
    public Text hpWallText;


    public int hpWall = 200;       //This is the current health
    public int mpPoints;           //The MP points the player can collect
    public int bulletSize;         //The size of the bullet
    public int bulletSpeed;        //The speed of the bullet

    [HideInInspector]
    public float health;           //This is the base value needed in order to get the current value in percentage.
    [HideInInspector]
    public float mana;             //This is the base value needed in order to get the current value in percentage, this also the limit points the player can collect. 

    private float Range;
    private int MPcost;                //Cost of MP points.

    // Start is called before the first frame update
    void Start()
    {
        ReloadTime = 0.5f;
        health = hpWall;
        mana = 100.0f;
    }
    
    // Update is called once per frame
    void Update()
    {
        hpWallText.text = hpWall + "/200";

        if (Input.GetKeyDown("up"))
        {
            bulletSize++;
        }
        if (Input.GetKeyDown("down"))
        {
            bulletSize--;
            if (bulletSize < 0)
            {
                bulletSize = 0;
            }
        }

        if (Input.GetKeyDown("right"))
        {
            bulletSpeed++;
        }
        if (Input.GetKeyDown("left"))
        {
            bulletSpeed--;
            if (bulletSpeed < 0)
            {
                bulletSpeed = 0;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            EnemyRespawn();
        }
        if (Input.GetMouseButtonDown(0) && ShootAvailable==true)
        {

            // Changing the size of the bullet
            BulletPrefab.transform.localScale = new Vector3(1, 1, 1) * bulletSize;

            // Changing the speed of the bullet

            if (BulletPrefab.GetComponent<BulletFollowCursor>().Speed != 0)
            {
                BulletPrefab.GetComponent<BulletFollowCursor>().Speed = bulletSpeed;
            }

            //ShootAnimation.Play("PlayerShooting");
            Instantiate(BulletPrefab, ShootingPosition.transform.position, Quaternion.identity);

            StartCoroutine(Reload());
            ShootAvailable = false;
        }
        if (hpWall <= 0)
        {
            ShootAvailable = false;
            Time.timeScale = 0;
            hpWallText.text = "You lost";
        }
        
    }

    void EnemyRespawn()
    {
        Range = Random.Range(-1, 1);
        //Debug.Log(Range);
        if (Range == 0)
        {
            Instantiate(Enemy1Prefab, new Vector2(6, -2), Quaternion.identity);
        }
        else
        {
            Instantiate(Enemy2Prefab, new Vector2(6, 1), Quaternion.identity);
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSecondsRealtime(ReloadTime);
        ShootAvailable = true;
    }

    // This function will manage what the stuff related to what the spell button will do.
    public void LaunchSpell(int spell)
    {
        switch (spell)
        {
            case 1: MPcost = 10;
                //Do other related stuff...
                Debug.Log("Spell " + spell + " applied." );
                RemoveMP();
                break;
            case 2:
                MPcost = 20;
                //Do other related stuff...
                Debug.Log("Spell " + spell + " applied.");
                RemoveMP();
                break;
            case 3:
                MPcost = 30;
                //Do other related stuff...
                Debug.Log("Spell " + spell + " applied.");
                RemoveMP();
                break;
            case 4:
                MPcost = 40;
                //Do other related stuff...
                Debug.Log("Spell " + spell + " applied.");
                RemoveMP();
                break;
            case 5:
                MPcost = 50;
                //Do other related stuff...
                Debug.Log("Spell " + spell + " applied.");
                RemoveMP();
                break;
        }
    }

    //Function to remove the MP that the player have used in the game
    void RemoveMP()
    {
        float currentvalue;
        mpPoints -= MPcost;
        currentvalue = mpPoints / mana;

        if(currentvalue <= 0.0f)
        {
            currentvalue = 0.0f;
            mpPoints = 0;
            mpBar.fillAmount = currentvalue;
        }
        else
        {
            mpBar.fillAmount = mpPoints / mana;
        }

        Debug.Log("fillAmount = " + mpBar.fillAmount);
        Debug.Log("Total points = " + mpPoints);
    }
}
