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
    public bool ShootAvailable = true;
    public float ReloadTime = 0.5f;
    public Text hpWallText;
    public Text WaveText;
    public int hpWall;
    public int bulletSize;  //The size of the bullet
    public int bulletSpeed; //The speed of the bullet

    [HideInInspector]
    public float health;

    private float Range;

    // Start is called before the first frame update
    void Start()
    {
        hpWall = 200;
        ReloadTime = 0.5f;

        health = hpWall;
    }
    
    // Update is called once per frame
    void Update()
    {
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

        hpWallText.text = hpWall + "/200";
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
        Debug.Log(Range);
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
}
