using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPositioning : MonoBehaviour
{
    [Header("Models")]
    [SerializeField] private GameObject heart;
    [SerializeField] private GameObject fractionHeart;
    [SerializeField] private GameObject unitHeart;

    public void SetPosition()
    {
        fractionHeart.transform.SetPositionAndRotation(heart.transform.position, heart.transform.rotation);
        unitHeart.transform.SetPositionAndRotation(heart.transform.position, heart.transform.rotation);
    }
}