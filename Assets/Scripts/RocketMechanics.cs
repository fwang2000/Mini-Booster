using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMechanics : MonoBehaviour
{
    Rigidbody rocketBody;
    AudioSource audioSource;

    [SerializeField]
    float thrustForce = 1000.0f;

    [SerializeField]
    float rotationForce = 100.0f;

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

    private void Rotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rocketBody.freezeRotation = true;
            transform.Rotate(Vector3.right * rotationForce * Time.deltaTime);
            rocketBody.freezeRotation = false;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rocketBody.freezeRotation = true;
            transform.Rotate(Vector3.left * rotationForce * Time.deltaTime);
            rocketBody.freezeRotation = false;
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rocketBody.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
