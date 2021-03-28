using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketMechanics : MonoBehaviour
{
    Rigidbody rocketBody;
    AudioSource audioSource;

    [SerializeField]
    float thrustForce = 1000.0f;

    [SerializeField]
    float rotationForce = 100.0f;

    private bool soundFadeOut = false;

    // Start is called before the first frame update
    void Start()
    {
        rocketBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Thrust();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("ok");
                break;
            case "Fuel":
                print("fuel gained");
                break;
            case "Finish":
                print("wow good job!");
                break;
            default:
                ReloadScene();
                break;
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Rotate()
    {
        rocketBody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.right * rotationForce * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.left * rotationForce * Time.deltaTime);
        }
        rocketBody.freezeRotation = false;
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rocketBody.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
            soundFadeOut = false;
            audioSource.volume = 0.48f;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            soundFadeOut = true;
        }

        if (soundFadeOut && audioSource.volume > 0.001f)
        {
            audioSource.volume *= 0.8f;
        }
    }
}
