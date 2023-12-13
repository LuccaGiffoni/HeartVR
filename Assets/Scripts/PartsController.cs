using Autohand;
using System.Collections;
using UnityEngine;

public class PartsController : MonoBehaviour
{
    [Header("Heart Model")]
    [SerializeField] private Grabbable heartGrabbable;
    [SerializeField] private SphereCollider heartMeshCollider;
    public GameObject[] heartParts;

    [Header("Fraction")]
    [SerializeField] private Transform[] fractionPositions;
    private Grabbable[] fractionGrabbable;
    private MeshCollider[] fractionMeshCollider;

    [Header("Unit")]
    [SerializeField] private Transform unitAnchorObject;
    [SerializeField] private Transform[] anchorPositions;

    [Header("Movement")]
    [SerializeField] private float lerpSpeed = 5f;

    private bool isSeparated = false;

    void Start()
    {
        fractionGrabbable = new Grabbable[heartParts.Length];
        for (int i = 0; i < heartParts.Length; i++)
        {
            fractionGrabbable[i] = heartParts[i].GetComponent<Grabbable>();
        }

        fractionMeshCollider = new MeshCollider[heartParts.Length];
        for(int i = 0; i < heartParts.Length; i++)
        {
            fractionMeshCollider[i] = heartParts[i].GetComponent<MeshCollider>();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            AnimateHeart();
        }
    }

    public void AnimateHeart()
    {
        if (isSeparated)
        {
            ReassembleHeart();
        }
        else
        {
            SeparateHeart();
        }
    }

    public void SeparateHeart()
    {
        heartGrabbable.enabled = false;

        for (int i = 0; i < heartParts.Length; i++)
        {
            Vector3 currentStartPosition = heartParts[i].transform.position;
            Quaternion currentStartRotation = heartParts[i].transform.rotation;
            StartCoroutine(MoveObject(heartParts[i].transform, fractionPositions[i].position, 
                fractionPositions[i].rotation, currentStartPosition, currentStartRotation, lerpSpeed));
            heartMeshCollider.enabled = false;
            fractionGrabbable[i].enabled = true;
            fractionMeshCollider[i].enabled = true;
            fractionMeshCollider[i].isTrigger = false;
        }

        isSeparated = true;
    }

    public void ReassembleHeart()
    {
        heartGrabbable.enabled = true;

        for (int i = 0; i < heartParts.Length; i++)
        {
            Vector3 currentStartPosition = heartParts[i].transform.position;
            Quaternion currentStartRotation = heartParts[i].transform.rotation;
            StartCoroutine(MoveObject(heartParts[i].transform, anchorPositions[i].position, 
                anchorPositions[i].rotation, currentStartPosition, currentStartRotation, lerpSpeed));
            heartMeshCollider.enabled = true;
            fractionGrabbable[i].enabled = false;
            fractionMeshCollider[i].enabled = false;
            fractionMeshCollider[i].enabled = true;
        }

        isSeparated = false;
    }

    private IEnumerator MoveObject(Transform objTransform, Vector3 targetPosition, Quaternion targetRotation, 
        Vector3 currentStartPosition, Quaternion currentStartRotation, float speed)
    {
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            objTransform.position = Vector3.Lerp(currentStartPosition, targetPosition, elapsedTime);
            objTransform.rotation = Quaternion.Lerp(currentStartRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }

        objTransform.position = targetPosition;
        objTransform.rotation = targetRotation;
    }
}