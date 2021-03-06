using System;
using DevCore.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public SceneBundle sceneToReload;
    public static GameLooper looper;

    public static GameManager instance;
    
    private float timer = 2f;

    private bool ending = false;
    private bool reseting = false;
    private float endspeed;
    private float enddistance;

    public static bool inputEnabled = true;
    
    public static event Action onGameStart;
    public static event Action onGameEnd;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        looper = Component.FindObjectOfType<GameLooper>();
        PlayerAttribute.onHitTaken += OnHitTakenHandler;
    }

    // Update is called once per frame
    void Update()
    {
        if (reseting)
        {
            return;
        }
        
        if (looper.started)
        {
            if (timer <= 0)
            {
                Debug.Log("Speed : " + PlayerAttribute.speed);
                timer = 2;
            }

            if (PlayerAttribute.speed < PlayerAttribute.maxSpeed)
            {
                PlayerAttribute.speed += (0.2f * Time.deltaTime);
            }
            timer -= Time.deltaTime;
        }

        if (ending)
        {
            GameObject.FindGameObjectWithTag("PlayerPrefab").transform.position += new Vector3 (0,0, enddistance / PlayerAttribute.distance * endspeed * Time.deltaTime);
            if (inputEnabled &&
                (Input.GetKeyDown(KeyCode.Return) ||
                 Input.GetKeyDown(KeyCode.Space) ||
                 (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))) {
                ResetScene();
            }
        }
    }

    public void StartGame() {
        if (looper.started) return;
        
        looper.StartGameLoop();
        PlayerAttribute.speed = 1;
        Cursor.visible = false;
        onGameStart?.Invoke();
    }
    
    void OnHitTakenHandler()
    {
        PlayerAttribute.speed = Math.Max(1, PlayerAttribute.speed/2);
        looper.ResetSpawn();
        if (PlayerAttribute.life == 0)
            EndGame();
    }

    void EndGame()
    {
        looper.StopGameLoop();
        enddistance = Mathf.Abs((GameObject.FindGameObjectWithTag("Spawn").transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).z);
        endspeed = PlayerAttribute.speed;
        PlayerAttribute.speed = 0;
        ending = true;

        // UI
        Debug.Log("Score: " + PlayerAttribute.score);
        Score.PersonnalScore = PlayerAttribute.score;
        Death.instance.Execute();

        Cursor.visible = true;
        onGameEnd?.Invoke();
    }

    void ResetScene()
    {
        PlayerAttribute.Reset();
        sceneToReload.Load();
    }

}
