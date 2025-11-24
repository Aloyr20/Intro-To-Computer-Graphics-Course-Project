using UnityEngine;

public class LevelClearController : MonoBehaviour
{
    public int totalEnemiesWave1;
    public int totalEnemiesWave2;
    int enemiesKilled;
    int wave = 1;
    public GameObject Wave2Skeletons;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public LutController lutController;

    private void Start()
    {
        Wave2Skeletons.SetActive(false);

        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
    }

    void Update()
    {
        if (wave == 1)
        {
            if (enemiesKilled == totalEnemiesWave1)
            {
                Wave1Complete();
            }
        }
        else if (wave == 2)
        {
            if (enemiesKilled == totalEnemiesWave2)
            {
                Wave2Complete();
            }
        }
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
    }

    void Wave1Complete()
    {
        Debug.Log("Wave 1 complete");
        enemiesKilled = 0;
        wave = 2;

        lutController.SetHorror();

        Wave2Skeletons.SetActive(true);

        // Enable custom LUT and orb stencil shader
    }

    void Wave2Complete()
    {
        Debug.Log("Wave 2 complete");
        enemiesKilled = 0;

        lutController.SetOff();

    }

    public void Win()
    {
        Time.timeScale = 0f;
        WinScreen.SetActive(true);
    }

    public void Lose()
    {
        Time.timeScale = 0f;
        LoseScreen.SetActive(true);
    }
}
