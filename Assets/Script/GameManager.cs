using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Level Temel Objeler")]
    [SerializeField] private GameObject Platform;
    [SerializeField] private GameObject Pota;
    [SerializeField] private GameObject PotaBuyutme;
    [SerializeField] private GameObject[] OzellikNoktalari;
    [SerializeField] private AudioSource[] Ses;
    [SerializeField] private ParticleSystem[] Efekt;

    [Header("UI OBJELER")]
    [SerializeField] private Image[] BasariliMiIconlari;
    [SerializeField] private Sprite BasariliSprite;
    [SerializeField] private int AtilmasiGerekenTop;
    [SerializeField] private GameObject[] Paneller;
    [SerializeField] private TextMeshProUGUI levelSayisiText;
    int BasketSayisi;
    float ParmakPozX;

    void Start()
    {
        //int TopIndex = 0;
        //foreach (var item in BasariliMiIconlari)
        //{
        //    if(TopIndex < AtilmasiGerekenTop)
        //    {
        //        if (!item.gameObject.activeInHierarchy)
        //        {
        //            item.gameObject.SetActive(true);
        //            TopIndex++;
        //        }
        //        else
        //        {
        //            TopIndex = 0;
        //            break;
        //        }
        //    }            
        //}

        for (int i = 0; i < AtilmasiGerekenTop; i++)
        {
            BasariliMiIconlari[i].gameObject.SetActive(true);
        }
        BasketSayisi = 0;
        //Invoke(nameof(OzellikOlustur), 5f);
        //levelSayisiText.text = PlayerPrefs.GetInt("SonLevel").ToString();
        levelSayisiText.text = "Level: " + SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.Mouse0))
        //{
        //    if (Input.GetAxis("Mouse X") < 0)
        //    {
        //        Platform.transform.position = Vector3.Lerp(Platform.transform.position, new Vector3(Platform.transform.position.x - .1f, Platform.transform.position.y, Platform.transform.position.z), .3f); 
        //    }
        //    if (Input.GetAxis("Mouse X") > 0)
        //    {
        //        Platform.transform.position = Vector3.Lerp(Platform.transform.position, new Vector3(Platform.transform.position.x + .1f, Platform.transform.position.y, Platform.transform.position.z), .3f);
        //    }
        //}

        if(Time.timeScale != 0) //Pc test icin.
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (Platform.transform.position.x > -1.3)
                    Platform.transform.position = Vector3.Lerp(Platform.transform.position, new Vector3(Platform.transform.position.x - .1f, Platform.transform.position.y, Platform.transform.position.z), .3f);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (Platform.transform.position.x < 1.3)
                    Platform.transform.position = Vector3.Lerp(Platform.transform.position, new Vector3(Platform.transform.position.x + .1f, Platform.transform.position.y, Platform.transform.position.z), .3f);
            }
        }

        if (Time.timeScale != 0) //Mobil icin.
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 TouchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        ParmakPozX = TouchPos.x - Platform.transform.position.x;
                        break;
                    case TouchPhase.Moved:
                        if(TouchPos.x - ParmakPozX > -1.15 && TouchPos.x - ParmakPozX < 1.15)
                        {
                            Platform.transform.position = Vector3.Lerp(Platform.transform.position, new Vector3(TouchPos.x - ParmakPozX, Platform.transform.position.y, Platform.transform.position.z), 5f);
                        }
                        break;               
                }
            }
        }
    }

    public void Basket(Vector3 Poz)
    {
        //foreach (var item in BasariliMiIconlari)
        //{
        //    if(BasketSayisi < AtilmasiGerekenTop)
        //    {
        //        if(item.gameObject.activeInHierarchy)
        //        {
        //            BasariliMiIconlari[BasketSayisi].gameObject.GetComponentInChildren<Image>().sprite = BasariliSprite;
        //            BasketSayisi++;
        //            break;
        //        }
        //    }
        //}

        BasketSayisi++;
        BasariliMiIconlari[BasketSayisi - 1].sprite = BasariliSprite;
        Efekt[0].transform.position = Poz;
        Efekt[0].gameObject.SetActive(true);
        Ses[1].Play();
        if (BasketSayisi == AtilmasiGerekenTop)
        {
            Kazandin();
        }

        if(BasketSayisi == 1)
        {
            OzellikOlustur();
        }
    }

    public void Kaybettin()
    {
        Paneller[2].SetActive(true);
        Ses[2].Play();
        Time.timeScale = 0;
    }

    public void Kazandin()
    {
        Paneller[1].SetActive(true);
        Ses[3].Play();
        PlayerPrefs.SetInt("SonLevel", PlayerPrefs.GetInt("SonLevel") + 1);
        Time.timeScale = 0;
    }

    public void PotaBuyut(Vector3 Poz)
    {
        Ses[0].Play();
        Efekt[1].transform.position = Poz;
        Efekt[1].gameObject.SetActive(true);
        Pota.transform.localScale = new Vector3(55f, 55f, 55f);
    }

    public void OzellikOlustur()
    {
        int randsomSayi = Random.Range(0, OzellikNoktalari.Length-1);
        PotaBuyutme.transform.position = OzellikNoktalari[randsomSayi].transform.position;
        PotaBuyutme.SetActive(true);
    }

    public void Buttonlarin›slemleri(string islem)
    {
        switch (islem)
        {
            case "Durdur":
                Paneller[0].SetActive(true);
                Time.timeScale = 0;
                break;
            case "Resume":
                Paneller[0].SetActive(false);
                Time.timeScale = 1;
                break;
            case "TryAgain":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                break;
            case "Next":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Time.timeScale = 1;
                break;
            case "Settings":
                break;
            case "Quit":
                Application.Quit();
                break;
        }
    }
}
