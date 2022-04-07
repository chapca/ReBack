using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] float maxTimer;
    float timer;

    [SerializeField] GameObject winDow;
    [SerializeField] GameObject player;

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
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = maxTimer;
            Respawn();
        }
    }

    void Respawn()
    {
        player.transform.position = transform.position;
    }

    public void Win()
    {
        winDow.SetActive(true);
    }
}
