using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using EasyTransition;
using Bardent;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    readonly List<int> usedLevels = new();
    int regularLevelsPlayed = 0; // Track the number of regular levels played.
    public int enemyCount = 0;
    public TransitionSettings transition;
    public float loadDelay = 0.75f;
    private Transform playerPos;

    public GameObject item;
    public GameObject gate;

    public KeyCode[] CheatCode;
    public KeyCode[] HiddenScenes;
    public KeyCode[] Volume;

    private int _index = 0;
    private int _index2 = 0;
    private int _index3 = 0;


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

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(CheatCode[_index]))
            {
                _index++;
            }
            else if (Input.GetKeyDown(HiddenScenes[_index2]))
            {
                _index2++;
            }
            else if (Input.GetKeyDown(Volume[_index3]))
            {
                _index3++;
            }
            else
            {
                ResetCheatInput();
            }
        }

        if (_index == CheatCode.Length)
        {
            ResetCheatInput();
            ScriptableObjectReset.instance.Cheat();
        }
        if (_index2 == HiddenScenes.Length)
        {
            ResetCheatInput();
            TransitionManager.Instance().Transition(9, transition, loadDelay);
        }
        if (_index3 == Volume.Length)
        {
            ResetCheatInput();
            AudioManager.Instance.Cheat();
        }
    }

    void ResetCheatInput()
    {
        _index = 0;
        _index2 = 0;
        _index3 = 0;
    }

    public void PlayerFuckingDied()
    {
        QuitToLibrary();
        //reset scriptable objects
        ResetUsedLevels();


        ScriptableObjectReset.instance.ResetAll();
    }

    public void SpawnEnemy()
    {
        // Spawn the enemy.
        enemyCount++;
    }

    public void EnemyDefeated()
    {
        // Enemy defeated or destroyed.
        enemyCount--;

        // Check if there are no enemies left.
        if (enemyCount == 0)
        {
            AllEnemiesDead();
        }
    }

    public void AllEnemiesDead()
    {
        if (enemyCount == 0)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerPos = player.transform;

            GameObject newItem = Instantiate(item, playerPos.position, Quaternion.identity);

            Rigidbody2D itemrb = newItem.GetComponent<Rigidbody2D>();

            if (itemrb != null)
            {
                Vector2 forceDirection = new Vector2(5.0f, 2.0f); // Adjust the direction and force as needed.
                itemrb.AddForce(forceDirection, ForceMode2D.Impulse);
            }


            //instantiate item after all enemies are dead and make sure no Overlap
        }
    }

    public void CreateGate(Vector3 position)
    {
        Vector3 newPosition = position + new Vector3(0, 2.5f, 0);
        Instantiate(gate, newPosition, Quaternion.identity);
    }


    #region Level Handling
    public void NextLevel()
    {

        TransitionManager.Instance().Transition("BetweenLevels", transition, loadDelay);

        if (regularLevelsPlayed < 4)
        {
            // Play the next regular level.
            Invoke("NextNormalLevel", 3f);
            regularLevelsPlayed++;
        }
        else
        {
            // Play the boss level.
            Invoke("ToBossLevel", 3f);
        }
    }

    public void NextNormalLevel()
    {
        int currentscene = SceneManager.GetActiveScene().buildIndex;

        if (currentscene == 1)
        {
            usedLevels.Clear();
        }

        int totalNormalLevels = 5;

        if (usedLevels.Count >= totalNormalLevels)
        {
            Debug.Log("All normal levels played.");
            usedLevels.Clear();
            return;
        }

        int nextLevel;

        // Loop until a valid, unused level is found.
        do
        {
            nextLevel = Random.Range(2, 6);
        } while (usedLevels.Contains(nextLevel));

        usedLevels.Add(nextLevel);

        StartCoroutine(AudioManager.Instance.PlayMusicFade("Levels", 2f));

        TransitionManager.Instance().Transition(nextLevel, transition, loadDelay);

    }

    public void ToBossLevel()
    {
        TransitionManager.Instance().Transition(7, transition, loadDelay);


        StartCoroutine(AudioManager.Instance.PlayMusicFade("Boss", 2f));
    }

    public void ResetUsedLevels()
    {
        usedLevels.Clear();
        regularLevelsPlayed = 0;
        enemyCount = 0;
    }
    #endregion

    #region Go back

    public void QuitToLibrary()
    {
        TransitionManager.Instance().Transition(1, transition, loadDelay);
        StartCoroutine(ResumeAfterDelay(loadDelay*1.2f));
        ScriptableObjectReset.instance.ResetAll();

        ResetUsedLevels();
        InventoryManager.instance.ClearItems();
    }

    public void QUitToMain()
    {
        TransitionManager.Instance().Transition(0, transition, loadDelay);
        StartCoroutine(ResumeAfterDelay(loadDelay*1.2f));
        ScriptableObjectReset.instance.ResetAll();

        ResetUsedLevels();
        InventoryManager.instance.ClearItems();
    }
    #endregion

    private IEnumerator ResumeAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Resume();
    }
    public void Pause()
    {
        // Pause the game.
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        // Resume the game.
        Time.timeScale = 1f;
    }

}
