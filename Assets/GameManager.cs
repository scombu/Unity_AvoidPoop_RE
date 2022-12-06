using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }
    
    private int score;

    [SerializeField]
    private GameObject poop;
    [SerializeField]
    private Text scoreTxt;

    [SerializeField]
    private Text bestScore;
    [SerializeField]
    private GameObject panel;

    public AudioSource bgmplayer;
    public AudioClip gameplay;
    public AudioClip gameover;

    public bool stopTrigger = true;

    private bool isgameover = false;

    void Start()
    {
        bgmplayer = GetComponent<AudioSource>();
    }

    public void GameStart()
    {
        score = 0;
        scoreTxt.text = "Score : " + score;
        isgameover = false;
        bgmplayer.Stop();
        bgmplayer.clip = gameplay;
        bgmplayer.Play();
        stopTrigger = true;
        StartCoroutine(CreatepoopRoutine());
        panel.SetActive(false);
    }
        public void GameOver()
    {
        bgmplayer.Stop();
        bgmplayer.clip = gameover;
        bgmplayer.Play();
        isgameover = true;
        stopTrigger = false;      
        StopCoroutine(CreatepoopRoutine());

        if (score >= PlayerPrefs.GetInt("BestScore", 0))
            PlayerPrefs.SetInt("BestScore", score);

        bestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();

        panel.SetActive(true);      
    }

    public void Score()
    {
        if (isgameover != true)
        {
            score++;
            scoreTxt.text = "Score : " + score;
        }
       
    }

    IEnumerator CreatepoopRoutine()
    {
        while(isgameover != true)
        {
            CreatePoop();
            yield return new WaitForSeconds(0.4f);
        }
    }
    private void CreatePoop()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.0f, 1.0f),1.1f, 0));
        pos.z = 0.0f;
        Instantiate(poop,pos,Quaternion.identity);
    }
}
