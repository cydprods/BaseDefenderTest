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
    public Text SizeText;
    public Text SpeedText;
    public int hpWall;
    private float Range;
    private float SpeedValue;
    private float SizeValue;
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
        if (Input.GetKeyDown("w"))
        {
            float.TryParse(SpeedText.text, out SpeedValue);
            SpeedValue += 0.1f;
            SpeedText.text = SpeedValue.ToString("0.00");
        }
        if (Input.GetKeyDown("s"))
        {
            float.TryParse(SpeedText.text, out SpeedValue);
            SpeedValue -= 0.1f;
            SpeedText.text = SpeedValue.ToString("0.00");
        }
        if (Input.GetKeyDown("q"))
        {
            float.TryParse(SizeText.text, out SizeValue);
            SizeValue += 0.1f;
            SizeText.text = SizeValue.ToString("0.00");
        }
        if (Input.GetKeyDown("a"))
        {
            float.TryParse(SizeText.text, out SizeValue);
            SizeValue -= 0.1f;
            SizeText.text = SizeValue.ToString("0.00");
        }
    }
    IEnumerator Reload()
    {
        yield return new WaitForSecondsRealtime(ReloadTime);
        ShootAvailable = true;
    }
}
