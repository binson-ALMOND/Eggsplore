using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Egg : MonoBehaviour
{
    public GameObject babyBirdPrefab;
    private bool isPlayerNear = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) isPlayerNear = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player")) isPlayerNear = false;
    }

    void Update()
    {
        if (isPlayerNear && Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Hatch();
        }
    }

    void Hatch()
    {
        if (ScoreManager.Instance != null) ScoreManager.Instance.AddPoint();

        if (babyBirdPrefab != null)
        {
            Instantiate(babyBirdPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
