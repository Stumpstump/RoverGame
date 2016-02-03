﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using ScionEngine;

public class PodAnimator : MonoBehaviour
{
    public Animator animator;
    public Animator playerAnimator;
    public PlayerStats player;
    public AnimationClip fallAnimation;
    public Transform environment;
    public Transform landingSpot;
    public Transform reorientingSpot;
    public ScionPostProcess scion;

    public Light hazardLight;
    public BatterySlot batterySlot;
	
    public void OpenDoor()
    {
        if(batterySlot.HasItem)
        {
            animator.SetBool("Open", true);
        }
    }


    public void Fall()
    {
        StartCoroutine(FallCoroutine());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") StartCoroutine(FallCoroutine());
    }


    private IEnumerator FallCoroutine()
    {
        float fadeOutTime = 0f;
        float fadeInTime = 4f;
        float getupTime = 3f;
        float elapsedTime = 0f;
        float cachedBloomIntensity = scion.bloomIntensity;
        Transform playerTransform = player.transform;
        FadeMenu fadeMenu = UIManager.GetMenu<FadeMenu>();

        player.DisableMovement();

        player.GetComponentInChildren<Rigidbody>().useGravity = false;
        scion.GetComponent<CameraShake>().Shake(5f, 0.02f);
        playerAnimator.enabled = true;
        animator.SetBool("Fall", true);
        playerAnimator.SetBool("Fall", true);
        yield return new WaitForSeconds(5.3f);

        fadeMenu.Fade(fadeOutTime, Color.clear, Color.black);

        yield return new WaitForSeconds(5f);

        playerTransform.SetParent(environment);
        playerTransform.position = landingSpot.position;
        playerTransform.rotation = landingSpot.rotation;
        yield return new WaitForSeconds(4f);

        fadeMenu.Fade(fadeInTime, Color.black, Color.clear);
        playerTransform.position = landingSpot.position;
        playerTransform.rotation = landingSpot.rotation;

        scion.grainIntensity = 1f;
        yield return new WaitForSeconds(1f);
        scion.grainIntensity = 0f;
        yield return new WaitForSeconds(0.4f);
        scion.grainIntensity = 1f;
        yield return new WaitForSeconds(0.6f);
        scion.grainIntensity = 0f;

        while (elapsedTime <= getupTime)
        {
            playerTransform.position = Vector3.Lerp(landingSpot.position, reorientingSpot.position, elapsedTime / getupTime);
            playerTransform.rotation = Quaternion.Lerp(landingSpot.rotation, reorientingSpot.rotation, elapsedTime / getupTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        playerTransform.position = reorientingSpot.position;
        playerTransform.rotation = reorientingSpot.rotation;
        fadeMenu.Close();
        player.EnableMovement();

        this.gameObject.SetActive(false);
    }
}