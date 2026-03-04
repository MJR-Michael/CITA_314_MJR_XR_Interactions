using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TheWall : MonoBehaviour
{
    [SerializeField] GameObject wallCubePrefab;
    [SerializeField] GameObject socketWallPrefab;
    [SerializeField] XRSocketInteractor wallSocket;
    [SerializeField] GameObject[] wallCubes;
    [SerializeField] float cubeSpacing = 0.005f;
    [SerializeField] Vector3 cubeSize;
    [SerializeField] Vector3 spawnPostition;

    void Start()
    {
        if(wallCubePrefab != null)
        {
            cubeSize = wallCubePrefab.GetComponent<Renderer>().bounds.size;
        }

        spawnPostition = transform.position;
        BuildWall();
    }

    private void BuildWall()
    {
        wallCubes = new GameObject[2];
        if (wallCubePrefab != null)
        {
            wallCubes[0] = Instantiate(wallCubePrefab, spawnPostition, transform.rotation, transform);
        }

        spawnPostition.y += cubeSize.y + cubeSpacing;

        if (socketWallPrefab != null)
        {
            wallCubes[1] = Instantiate(socketWallPrefab, spawnPostition, transform.rotation, transform);
            wallSocket = wallCubes[0].GetComponentInChildren<XRSocketInteractor>();

            if (wallSocket != null)
            {
                wallSocket.selectEntered.AddListener(OnSocketEnter);
                wallSocket.selectExited.AddListener(OnSocketExited);
            }
        }

        for (int i = 0; i < wallCubes.Length; i++)
        {
            if (wallCubes[i] != null)
            {
                wallCubes[i].transform.SetParent(transform);
            }
        }
    }
    private void OnSocketEnter(SelectEnterEventArgs arg0)
    {
        for (int i = 0; i < wallCubes.Length; i++)
        {
            if (wallCubes[i] != null)
            {
                Rigidbody rb = wallCubes[i].GetComponent<Rigidbody>();
                rb.isKinematic = false;
            }
        }
    }

    private void OnSocketExited(SelectExitEventArgs arg0)
    {
        for (int i = 0; i < wallCubes.Length; i++)
        {
            if (wallCubes[i] != null)
            {
                Rigidbody rb = wallCubes[i].GetComponent<Rigidbody>();
                rb.isKinematic = true;
            }
        }
    }
}
