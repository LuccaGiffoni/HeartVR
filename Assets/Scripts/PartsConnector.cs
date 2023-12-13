using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsConnector : MonoBehaviour
{
    [Header("Zone Object")]
    [SerializeField] private Transform snapZone;

    [Header("Zone Parameters")]
    [SerializeField] private float positionSnapDistance = 0.5f;
    [SerializeField] private float rotationSnapAngle = 10f;

    [Header("Audio")]
    [SerializeField] private AudioSource snapSource;

    private bool isSnapped = false;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SnapObjectWhenDropped()
    {
        if (!isSnapped)
        {
            float positionDistance = Vector3.Distance(transform.position, snapZone.position);
            float rotationAngle = Quaternion.Angle(transform.rotation, snapZone.rotation);

            if (positionDistance < positionSnapDistance && rotationAngle < rotationSnapAngle)
            {
                SnapToPosition();
            }
            else
            {
                Debug.Log("Object is too far from the Anchor!");
            }
        }
    }

    private void SnapToPosition()
    {
        transform.SetPositionAndRotation(snapZone.position, snapZone.rotation);

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false; 
        }

        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        isSnapped = true;
        snapSource.Play();
    }
}
