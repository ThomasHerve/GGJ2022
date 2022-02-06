using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;


struct Obst{
    [SerializeField]
    public GameObject Object;
    [SerializeField]
    public long length;
}

public class GameLooper : MonoBehaviour {
    [Header("Spawn")]
    [SerializeField] private SpawnSettings[] m_SpawnSettings = {};
    [SerializeField] private float m_SpawnSettingSamplingMaxTime = 90f;
    [SerializeField] private AnimationCurve m_SpawnSettingSamplingOverTime = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [SerializeField] private Transform m_SpawnPoint = null;
    
    [Header("Environments")]
    [SerializeField]
    private float no_env_time_max;
    private float no_env_time_max_real;

    [SerializeField]
    private float no_env_time_min;
    private float no_env_time_min_real;

    [SerializeField]
    private AnimationCurve damageCurve;

    private Volume damagesVolume;

    private Scheduler scheduler;
    private float timer = 2.0f;
    private float timerEnv = 5.0f;
    

    private GameObject obstacle;
    private Volume speedVolume;

    private bool m_Started = false;
    public bool started => m_Started;
    private float m_CurrentLoopTime = 0f; 
    
    
    float patternchance = 0.2f;
    int pattern = 0;

    // Start is called before the first frame update
    void Start()
    {
        scheduler = Component.FindObjectOfType<Scheduler>();
        ObstacleTimer.InObstacle += InObstacleHandler;
        ObstacleTimer.OutObstacle += OutObstacleHandler;
        PlayerAttribute.onHitTaken += HitTakenHandler;
        PlayerAttribute.onSpeedChange += SpeedChangeHandler;
        damagesVolume = GameObject.FindGameObjectWithTag("damageVolume").GetComponent<Volume>();
        speedVolume = GameObject.FindGameObjectWithTag("speedVolume").GetComponent<Volume>();

        ResetSpawn();
    }

    // Update is called once per frame
    void Update()
    {

        // Environment
        timerEnv -= Time.deltaTime;
        if (timerEnv <= 0)
        {
            scheduler.NextEnv();
            timerEnv = UnityEngine.Random.Range(no_env_time_min_real, no_env_time_max_real);
        }

        if (!started)
            return;

        //Hits
        if (obstacle != null && !PlayerAttribute.invincible)
        {
            if (obstacle.GetComponent<ObstacleAttribute>().color != PlayerAttribute.color)
            {
                PlayerAttribute.LoseLife();
            }
            else
            {
                PlayerAttribute.score += 1;
            }
        }

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //Spawn
            var setting = GetCurrentSettings();
            int patternCount = Random.Range(setting.randomPatternCount.x, setting.randomPatternCount.y + 1);
            for (int i = 0; i < patternCount; i++) {
                scheduler.Next(m_SpawnPoint.position, i * setting.patternSpacing);
            }

            timer = Random.Range(setting.randomSpawnTime.x, setting.randomSpawnTime.y) +
                    (patternCount - 1) * (setting.patternSpacing /(m_SpawnPoint.position.z / PlayerAttribute.distance * PlayerAttribute.speed));
        }

        m_CurrentLoopTime += Time.deltaTime;
    }


    private SpawnSettings GetCurrentSettings() {
        float time = m_CurrentLoopTime < m_SpawnSettingSamplingMaxTime ? m_CurrentLoopTime : m_SpawnSettingSamplingMaxTime;
        float difficultyRatio = m_SpawnSettingSamplingOverTime.Evaluate(time / m_SpawnSettingSamplingMaxTime);

        if (difficultyRatio < 1f) {
            int settingsCount = m_SpawnSettings.Length;
            float remappedRatio = difficultyRatio * settingsCount;
            return m_SpawnSettings[Mathf.FloorToInt(remappedRatio)];
        } else {
            return m_SpawnSettings[m_SpawnSettings.Length - 1];
        }
    }

    public void StartGameLoop() {
        m_CurrentLoopTime = 0f;
        m_Started = true;
    }

    public void StopGameLoop() {
        m_Started = false;
    }
    public void InObstacleHandler(object sender, ObstacleEventArg e)
    {
        Debug.Log("In Obstacle");
        obstacle = e.obstacleGameObject;

    }

    void OutObstacleHandler(object sender, ObstacleEventArg e)
    {
        Debug.Log("Out of Obstacle");
        obstacle = null;

    }
    
    public void ResetSpawn()
    {
        no_env_time_min_real = no_env_time_min;
        no_env_time_max_real = no_env_time_max;
    }

    private void HitTakenHandler() {
        StartCoroutine(HitTakenCoroutine());
    }

    private IEnumerator HitTakenCoroutine() {
        float timer = 0;
        while (timer < 2)
        {
            timer += Time.deltaTime;
            damagesVolume.weight = damageCurve.Evaluate(timer);
            yield return null;
        }
    }

    private void SpeedChangeHandler() {
        speedVolume.weight = PlayerAttribute.speed / PlayerAttribute.maxSpeed;
    }
}
