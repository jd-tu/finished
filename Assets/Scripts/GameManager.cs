using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    public Button restartButton;
    public GameObject titleScreen;
    public GameObject[] DiamondPrefabs;
    public GameObject[] AnimalPrefabs;
    public GameObject player;
    // private float spawnInterval = 3f;  
    private int score;
    private float startDelay = 3;
    private float spawnRate = 10.0f;
    public bool isGameActive;
    private float playerZpos;

    private AudioSource playerAudio;
    public AudioClip gameOverSound;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameActive)
        {
            playerZpos = player.transform.position.z;      
        }
    }
     
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

  
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        UpdateScore(0);

        player = GameObject.Find("Player");
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        StartCoroutine(SpawnAnimals());
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
    }
    
    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(startDelay);
            int index = Random.Range(0, DiamondPrefabs.Length);
            Vector3 spawnPos = new Vector3(Random.Range(195, 205), 1.25f, Random.Range(playerZpos + 50, playerZpos + 100));
            Instantiate(DiamondPrefabs[index], spawnPos, DiamondPrefabs[index].transform.rotation);
        }
    }

    IEnumerator SpawnAnimals()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, AnimalPrefabs.Length);
            Vector3 spawnPos = new Vector3(Random.Range(195, 205), 1.25f, Random.Range(playerZpos + 50, playerZpos + 100));
            Instantiate(AnimalPrefabs[index], spawnPos, AnimalPrefabs[index].transform.rotation);
        }
    }
    
    public void GameOver()
    {
        playerAudio.PlayOneShot(gameOverSound, 0.5f);
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        Destroy(player);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
