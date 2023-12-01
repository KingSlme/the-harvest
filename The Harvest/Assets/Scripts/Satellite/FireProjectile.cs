using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    bool canFire = true;
    [SerializeField] GameObject bullet;

    [SerializeField] GameObject spendPopup;

    IEnumerator CannonCooldown() {
        canFire = false;
        yield return new WaitForSecondsRealtime(0.2f);
        canFire = true;
    }

    public void Fire() {
        if(canFire && FindObjectOfType<GameManager>().numSeeds > 0 && !PauseMenu.GameIsPaused) {
            Singleton.instance.PlayShootSound();
            FindObjectOfType<GameManager>().numSeeds -= 1;
            StartCoroutine(CannonCooldown());
            Instantiate(bullet, transform.position, transform.rotation);
            // Instantiate(spendPopup, new Vector3(-6.482f,2.13f,0), Quaternion.identity);
            Instantiate(spendPopup, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0), Quaternion.identity);
        }
    }
}