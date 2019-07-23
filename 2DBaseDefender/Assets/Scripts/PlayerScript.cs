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
    public bool ShootAvailable = true;
    public float ReloadTime = 0.5f;
    public Text hpWallText;
    public Text WaveText;
    public int hpWall;
    private float Range;
    // Start is called before the first frame update
    void Start()
    {
        hpWall = 200;
        ReloadTime = 0.5f;
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
    // Update is called once per frame
    void Update()
    {
        hpWallText.text = hpWall + "/200";
        if (Input.GetMouseButtonDown(1))
        {
            EnemyRespawn();
        }
        if (Input.GetMouseButtonDown(0) && ShootAvailable==true)
        {

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
    IEnumerator Reload()
    {
        yield return new WaitForSecondsRealtime(ReloadTime);
        ShootAvailable = true;
    }
}
