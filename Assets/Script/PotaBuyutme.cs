using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotaBuyutme : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Sure;
    [SerializeField] private int BaslangicSuresi;

    [SerializeField] private GameManager _GameManager;

    private void Start()
    {
        Sure.text = BaslangicSuresi.ToString();
        StartCoroutine(SayacBaslasin());
    }

    IEnumerator SayacBaslasin()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            BaslangicSuresi--;
            Sure.text = BaslangicSuresi.ToString();

            if(BaslangicSuresi == 0)
            {
                gameObject.SetActive(false);
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Top"))
        {
            _GameManager.PotaBuyut(gameObject.transform.position);
            gameObject.SetActive(false);
        }
    }
}
