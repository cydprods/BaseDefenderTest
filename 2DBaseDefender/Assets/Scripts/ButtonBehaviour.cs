using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    public int pointsNeeded;   //The minimum MP points this buttons needs to make it enable.
    public GameObject player;
    private Button button;

    private PlayerScript _player;
    

    void Awake()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }
        _player = player.gameObject.GetComponent<PlayerScript>();
        button = GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //button.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.mpPoints >= pointsNeeded)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
        
    }
}
