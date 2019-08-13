using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public GameObject spawnPointA;    //the spawn point which will spawn the enemies.
    public GameObject spawnPointB;    //the second spawn point which will spawn the enemies.
    public GameObject[] enemies;      //the array containing the desired number of enemies.
    public GameObject ShopUI;         //the UI of the shop.
    public int waves;                 //number of desired waves.
    public Text waveText;             //the text which will display the number of the current wave.
    public float startTime;           //Time before the first wave will spawn.
    public float waveTime;            //Time between waves.
    public float nextEnemyTime;       //Time for enemies to spawn between them.
    public int numberOfEnemies;       //the number of enemies that the wave has.

    private int enemiesIndex;         //the index for the enemies array.
    private int wave;                 //the number of the current wave.
    private Transform SPA;            //the transform component of the spawnPointA.
    private Transform SPB;            //the transform component of the spawnPointB.
    private int range = 2;            //the range of options that will be picked up when a random number is given. It will increase in order to expand the range of options available.
    private GameObject player;
    private PlayerScript playerScript;

    void Awake()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
        wave = 1;  //Because a wave 0 is kind of wierd.
        //Lets get the position of the spawnpoints.
        SPA = spawnPointA.GetComponent<Transform>(); 
        SPB = spawnPointB.GetComponent<Transform>();
        //The Coroutine will start here because it only needs to be executed only once and not constantly, so it doesnt go in the update function
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        //Lets update the text of the wave text, that will keep updated the number of waves we have survived.
        waveText.text = "Wave " + wave + " of " + waves;

        if (Time.timeScale == 0)
        {
            //If ShootAvailable is true, the player can shoot bullets when game gets paused and because the timescale will be stopped the bullets will get a bizarre behaviour.
            playerScript.ShootAvailable = false;
        }
        else
        {
            //So the time is not stopped, then lets restore the ability to shoot bullets.
            playerScript.ShootAvailable = true;
        }
    }

    //This function will spawn the enemies.
    IEnumerator SpawnEnemy()
    {
        //Lets give the player a small time to be prepared for battle.
        yield return new WaitForSeconds(startTime);

        //Then it will start this loop forever, unless we decide to "break" it.
        while (true)
        {
            //This loop spawn the round of enemies.
            for (int i = 0; i < numberOfEnemies; i++)
            {
                //Before we spawn anything lets pick which enemy will spawn by picking a random number to put in the index of the array of enemies (options) we have.
                enemiesIndex = Random.Range(0, range);

                //How we will decide which enemy will appear in where??? by looking in its tag, FlyingEnemy for bats or anything like them, and Enemy for grounded hostiles.
                if (enemies[enemiesIndex].tag == "FlyingEnemy")
                {
                    Instantiate(enemies[enemiesIndex], new Vector2(SPA.position.x, SPA.position.y), Quaternion.identity);
                }
                else
                {
                    Instantiate(enemies[enemiesIndex], new Vector2(SPB.position.x, SPB.position.y), Quaternion.identity);
                }

                //now lets give some time before a new enemy spawns. With less time enemies spawn faster and vice versa.
                yield return new WaitForSeconds(nextEnemyTime);
            }
            
            //Once the last enemy appears in the scene, a short time will pass before the next wave of enemies start.
            yield return new WaitForSeconds(waveTime);
            //Now that the time has passed the new wave will be ready to start but before the shop UI menu will show.
            ShowShop();
            wave++;
            //The wave counter has increased, because we want to put new enemies after a certain number of waves, lets check if the variable range needs to be modified.
            CheckRange();
        }
    }

    //This function will show the Shop UI menu. This function goes better in a Game Manager.
    void ShowShop()
    {
        if(Time.timeScale > 0.0f)
        {
            //Lets show the Shop UI
            ShopUI.SetActive(true);
            //Lets stop time.
            Time.timeScale = 0f;
        }      
    }

    //This will disable (close) the Shop UI when a UI button is pressed. This function goes better in a Game Manager.
    public void CloseShop()
    {
        if (Time.timeScale == 0.0f)
        {
            //Lets hide the UI.
            ShopUI.SetActive(false);
            //Finally, lets restore the time as it was.
            Time.timeScale = 1f;
        }
    }

    //This function will check the variable range and modify it, if certain requirements are met.
    void CheckRange()
    {
        switch (wave)
        {
            case 5:
                //In case that accidentally we declare not enough enemies in the corresponding array, nothing will happens, null exception errors will be avoided.
                if (enemies.Length > range) { range = 3; }
                break;
            case 10:
                if (enemies.Length > range) { range = 4; }
                break;
            case 15:
                if (enemies.Length > range) { range = 5; }
                break;
            default:
                //Debug.Log("<color=white>Range NOT expanded.</color>");
                //Debug.Log("<color=white>Range: </color>" + range);
                //Debug.Log("<color=white>Length: </color>" + enemies.Length);
                break;
        }
    }
    
}
