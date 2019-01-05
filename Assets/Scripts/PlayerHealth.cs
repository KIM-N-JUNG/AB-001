using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public GameObject MainCamera;
    public Slider HealthBar;
    public float Health = 100;

    private float _currentHealth;

    // Use this for initialization
    void Start () {
        _currentHealth = Health;
    }

    public void TakeDamage(float damage)
    {
        //Debug.Log("TakeDamage");
        _currentHealth -= damage;
        HealthBar.value = _currentHealth;

        if (_currentHealth <= 0)
        {
            MainCamera.GetComponent<PauseMenu>().End();
        }
    }
}
