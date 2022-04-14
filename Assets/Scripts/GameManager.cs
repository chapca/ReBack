using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] float maxTimer;
    float timer;

    [SerializeField] GameObject winDow;
    [SerializeField] GameObject player;

    [SerializeField] TextMeshProUGUI timeLeft;

    [SerializeField] bool hasRespawed;
    bool isWin;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWin)
        {
            timer -= Time.deltaTime;
            timeLeft.text = timer.ToString("0.00");
        }

        if (timer <= 0)
        {
            timer = maxTimer;
            Respawn();
        }
    }

    void Respawn()
    {
        player.transform.position = transform.position;
        hasRespawed = true;
    }

    public void Win()
    {
        winDow.SetActive(true);
    }

    public bool GetHasRespawned()
    {
        return hasRespawed;
    }
    public void SetHasRespawned(bool value)
    {
        hasRespawed = value;
    }

    public void SetTimer(float val)
    {
        timer = val;
    }
}
