using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;

    

    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }

    [SerializeField]
    private int _health = 100;
    private int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health <= 0)
                IsAlive = false;
        }
    }

    private bool _isAlive = true;

    private bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            animator.SetBool(param_isAlive, _isAlive);
        }
    }

    [Header("iFrames")]
    [SerializeField] private float iFrameTime;
    [SerializeField] private int flashNumber;
    private SpriteRenderer render;

    public bool IsHit { get; private set; }

    [SerializeField]
    private float invincibilityTime = 0.25f;
    private float timeSinceHit = 0;
    private bool isInvincible = false;

    private string param_isAlive = "isAlive";
    [SerializeField] private float knockbackPower = 1.5f;

    Animator animator;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        animator.SetBool(param_isAlive, IsAlive);
    }

    public void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                // can be hit again
                isInvincible = false;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    public void Hit(int damage, Vector2 knockbackForce)
    {
        if (IsAlive && !isInvincible)
        {
            // Run any checks and modifications you need to the damage before doing the final apply
            // I.e. resistances, immunities
            Health -= damage;

            Debug.Log(gameObject.name + " took " + damage);

            // This one is for objects that want to know when ANY damageable was hit
            //CharacterEvents.characterHit?.Invoke(this, damage);

            // This one is for handling when this SPECIFIC component was hit 
            damageableHit.Invoke(damage, knockbackForce);

            animator.SetBool(AnimationStrings.isHit, true);
            timeSinceHit = 0;
            isInvincible = true;
            StartCoroutine(Invulnerability());
            IsHit = false;

        }
    }

    // Seperate function since any resistances or modifiers will impact healing differently than being hit for damage
    public void Heal(int healthReceived)
    {
        if (IsAlive)
        {
            int amountCanBeHealed = MaxHealth - Health;

            int healthRestored = Mathf.Min(healthReceived, amountCanBeHealed);

            Health += healthRestored;

           // CharacterEvents.characterHealed?.Invoke(this, healthRestored);
        }
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        for (int i = 0; i < flashNumber; i++)
        {
            render.color = new Color(1, 0.6f, 0.6f, 0.5f);
            yield return new WaitForSeconds(iFrameTime / (flashNumber * 2));
            render.color = Color.white;
            yield return new WaitForSeconds(iFrameTime / (flashNumber * 2));
        }

        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
}
