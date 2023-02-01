using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.AI;

public class EnemyDamageable : MonoBehaviour, IDamageable 
{
    private float _coinBonus = 10;
    private float _health = 100;
    private float _knock = 0;

    [SerializeField]
    private HealthBarRennder healthBarRennder = new HealthBarRennder();

    [SerializeField]
    private KnockDownBarRennder knockDownBarRennder = new KnockDownBarRennder();
    private SoundManager soundManager;
    public AudioClip     audioClip, deathAudioClip;
    [Range(0,1)]
    public float         volumeScale;

    public bool          isDead = false;
    public bool          isKnockDown = false; 
    public UnityEvent    OnEnemyDead;

    public bool isMelee, isGunner;

    private Animator     animator;
    private int          knockDownHash;
    private int          stateDeath;
    private int          hitHash;
    private int          laserHitHash;
    private int          deadHash;
    private float        standUpTimer = 2f;
    private bool         knockBack;
    private NavMeshAgent agent;
    private GameManager gameManager;
    // private ObjectPooler objectPooler;
    public UnityEvent OnHit;
    private void Awake() 
    {
        soundManager = SoundManager.Instance;
        gameManager = GameManager.Instance;
        // objectPooler = ObjectPooler.Instance;
        deadHash      = Animator.StringToHash("Dead");
        knockDownHash = Animator.StringToHash("KnockDown");
        hitHash       = Animator.StringToHash("Hit");
        laserHitHash  = Animator.StringToHash("LaserHit");
        stateDeath    = Animator.StringToHash("StateDeath");
        agent = GetComponent<NavMeshAgent>();

        if (isMelee)
        {
            animator = GetComponent<Animator>();
        }
        else if (isGunner)
        {
            animator = GetComponentInParent<Animator>();
        }

    }

    private void OnEnable() 
    {
        gameManager.OnStartGame.AddListener(StartGame);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void StartGame()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(knockBack) {
            agent.Move(-transform.forward * 10f * Time.deltaTime);
        }
    }

    private void LateUpdate() 
    {
        healthBarRennder.UpdateHealthBarRotation();
        knockDownBarRennder.UpdateKnockDownBarRotation();
    }

    public void TakeDamge(float damage)
    {
        if (isDead)
            return;

        // gameManager.HitedInUI();
        
        _health -= damage;
        healthBarRennder.UpdateHealthBarValue(_health);
        soundManager.PlayOneShot(audioClip);
        if (_health > 0)
        {
            TakeKnockDown(25);
            if (_knock >= 100)
            {
                KnockDown();
                _knock = 0;
                knockDownBarRennder.UpdateKnockDownBarValue(_knock);
            }
            else
            {
                Hited();
            }
        }

        if (_health <= 0 && !isDead )
        {
            soundManager.PlayOneShot(deathAudioClip);
            Dead();
            while (_coinBonus > 0)
            {
                // Debug.Log(123123123);
                // objectPooler.SpawnObject("Money", transform.position, transform.rotation);
                _coinBonus--;
            }
            isDead = true;
            knockDownBarRennder.DestroyKnockDownBar();
            healthBarRennder.DestroyHealbar();
        }
    }

    public void TakeKnockDown(float knock)
    {
        _knock += knock;
        knockDownBarRennder.UpdateKnockDownBarValue(_knock);
    }

    public void KnockDown()
    {
        agent.ResetPath();
        isKnockDown = true;
        knockBack = true;
        Invoke("CancelKnockBack", 0.3f);
        animator.SetTrigger(knockDownHash);
    }

    private void CancelKnockBack() {
        knockBack = false;
    }

    public void Hited()
    {
        animator.SetTrigger(hitHash);
    }

    public void Dead()
    {
        OnEnemyDead?.Invoke();
        float t = Random.Range(1, 4);
        animator.SetFloat(stateDeath,t);
        ScoreManager.Instance.CountEnemy();
    }
    
    protected virtual void StandUp()
    {
        StartCoroutine(StandUpAfterTime());
    }

    IEnumerator StandUpAfterTime()
    {
        yield return new WaitForSeconds(standUpTimer);
        isKnockDown = false;
    }

    public void setInit(float health, float coinBonus) {
        _health = health;
        _coinBonus = coinBonus;
        healthBarRennder.CreateHealthBar(transform, health);
    }

    public void setInitKnockDown(float knock)
    {
        _knock = knock;
        knockDownBarRennder.CreateKnockDownBar(transform,knock);
    }
    
    private void OnDisable() 
    {
        gameManager.OnStartGame.RemoveListener(StartGame);
    }


}
