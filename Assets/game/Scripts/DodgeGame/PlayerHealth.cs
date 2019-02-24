using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public GameObject shuttle;
    public GameObject MainCamera;
    public Slider HealthBar;
    public float Health;
    public CameraFollower camera;
    public ParticleSystem particle;
    public ParticleSystem smallExplosionParticle;
    public ParticleSystem bigExplosionParticle;
    public Timer timer;
    public Score score;

    private float _currentHealth;

    // Use this for initialization
    void Start () {
        _currentHealth = Health;
    }

    public void TakeDamage(float damage, Vector3 position)
    {
        camera.ShakeCamera(0.05f, 1.0f);
        particle.Play();
        smallExplosionParticle.transform.position = new Vector3(position.x, position.y, position.z);
        smallExplosionParticle.Play();
        Handheld.Vibrate();

        SoundManager.instance.playSound();

        _currentHealth -= damage;
        HealthBar.value = _currentHealth;

        if (_currentHealth <= 0)
        {
            timer.Pause();
            score.Pause();
            shuttle.SetActive(false);
            Vector3 sPos = shuttle.transform.position;
            bigExplosionParticle.transform.position = new Vector3(sPos.x, sPos.y, sPos.z);
            bigExplosionParticle.Play();
            camera.ShakeCamera(0.0f, 0.0f);
            Invoke("EndGame", 1);

            return;
        }
    }

    private void EndGame()
    {
        MainCamera.GetComponent<PauseMenu>().End();
    }
}
