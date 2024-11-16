using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SearchService;

public class PlayerGrenadeManager : MonoBehaviour
{
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] float launchSpeed;

    Vector3 spawnOffset = new Vector3(-0.5f, -0.1f, 0.1f);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject newGrenade = Instantiate(grenadePrefab, transform.position+(spawnOffset+transform.forward), Quaternion.Euler(45f, 45f, 45f));
            newGrenade.GetComponent<Rigidbody>().AddForce(transform.forward * launchSpeed, ForceMode.Impulse);
        }
    }

}
