using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    private GameObject focalPoint;
    public GameObject powerupIndicator;

    private float speed = 400f;
    private float powerupStrength = 15f;

    public bool hasPowerUp = false;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");

        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput * Time.deltaTime);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0); // Sets position of powerup to players position
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            powerupIndicator.gameObject.SetActive(true); // Turns powerup on 
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        // Runs 5 second powerup timer
        yield return new WaitForSeconds(5);
        hasPowerUp = false;
        powerupIndicator.gameObject.SetActive(false); // Turns powerup off
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            // Gets rigidbody of the enemy
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            // Gets direction to send the enemy away from player
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            // Apply force to enemy away from player, times powerupStrenth variable, applied instantly
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            // Debug Log to show when collision occurs
            Debug.Log("Collided with" + collision.gameObject.name + "with powerup set to" + hasPowerUp);
        }
    }
}
