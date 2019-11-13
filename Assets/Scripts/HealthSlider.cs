using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{

    Player player;
    Slider healthBar;
    void Start()
    {
        healthBar = GetComponent<Slider>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = player.GetHealth();
    }
}
