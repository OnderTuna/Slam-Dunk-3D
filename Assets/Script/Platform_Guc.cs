using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Guc : MonoBehaviour
{
    [SerializeField] float Guc;
    [SerializeField] float Aci;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Top"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Aci,90,0) * Guc, ForceMode.Force); /*Carpistigi objenin rigidbodysine erisip guc uygula.*/
        }
    }
}
