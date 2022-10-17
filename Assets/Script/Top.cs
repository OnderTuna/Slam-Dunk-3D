using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private AudioSource TopSesi;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Skor"))
        {
            _GameManager.Basket(gameObject.transform.position);
        }

        if (other.gameObject.CompareTag("OyunBitti"))
        {
            _GameManager.Kaybettin();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        TopSesi.Play(); /*Herhangi bir carpismada oynasin isteriz bu nedenle tagsiz.*/
    }
}
