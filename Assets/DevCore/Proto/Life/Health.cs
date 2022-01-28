using UnityEngine;
using UnityEngine.Events;
using DevCore.Core;

/// <summary>
/// Base health system for game objects
/// </summary>
public class Health : MonoBehaviour {
    #region Settings
    [SerializeField, Min(0)] private int maxHealth = 100;
    [SerializeField, Min(0f)] private float recoveryTime = 0f;
    public bool invicible = false;

    [Header("Death")]
    [SerializeField] private DeathBehaviour deathBehaviour = null;
    [SerializeField] private Object deathBehaviourParameter = null;

    [SerializeField] private IntEvent onInitHealth = new IntEvent();
    [SerializeField] private IntEvent onInitMaxHealth = new IntEvent();
    [SerializeField] private IntEvent onHeal = new IntEvent();
    [SerializeField] private IntEvent onTakeDamages = new IntEvent();
    [SerializeField] private UnityEvent onAbsorbDamages = new UnityEvent();
    [SerializeField] private UnityEvent onRecover = new UnityEvent();
    [SerializeField] private UnityEvent onEndRecover = new UnityEvent();
    [SerializeField] private IntEvent onAddMaxHealth = new IntEvent();
    [SerializeField] private UnityEvent onDie = new UnityEvent();
    #endregion

    #region Classes
    [System.Serializable] public class IntEvent : UnityEvent<int> { }
    #endregion

    #region Currents
    private int currentHealth = 100;
    private Timer recoveryTimer = null;
    #endregion

    #region Properties
    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;
    public float CurrentHealthRatio => (float)currentHealth / (float)maxHealth;
    public bool IsRecovering => recoveryTimer != null && recoveryTimer.isPlaying;
    #endregion

    #region Callbacks
    private void Start() {
        currentHealth = maxHealth;
        recoveryTimer = new Timer(recoveryTime, () => onEndRecover?.Invoke());
        onInitHealth?.Invoke(currentHealth);
        onInitMaxHealth?.Invoke(maxHealth);
    }
    #endregion

    #region Update Health
    public void Heal(int amount) {
        if (amount < 0) {
            return;
        }

        currentHealth += amount;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }

        onHeal?.Invoke(currentHealth);
    }

    public void InflictDamages(int amount) {
        if (amount < 0 || invicible || IsRecovering) {
            if (invicible) {
                onAbsorbDamages?.Invoke();
            }
            return;
        }

        currentHealth -= amount;
        if (currentHealth <= 0) {
            Die();
        }

        if(recoveryTime > 0f) {
            if (recoveryTimer.isPlaying) {
                recoveryTimer.Cancel();
            }
            recoveryTimer.Play();
            onRecover?.Invoke();
        }

        onTakeDamages?.Invoke(currentHealth);
    }

    public void AddMaxHealth(int amount) {
        maxHealth += amount;
        onAddMaxHealth?.Invoke(maxHealth);
    }
    #endregion

    #region Die
    public void Die() {
        onDie?.Invoke();
        if (deathBehaviour != null) {
            deathBehaviour.Execute(this, deathBehaviourParameter);
        } else {
            Destroy(gameObject);
        }
    }
    #endregion
}
