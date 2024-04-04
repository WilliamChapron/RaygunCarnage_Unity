using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int _maxHealth = 200;
    public int _currentHealth;


    public HealthBar _healthBar;


    //// Health go zero
    //public delegate void HealthZeroEvent(); //other class can follow
    //public event HealthZeroEvent OnHealthZero;

    void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
        //Debug.Log("Health COmponent : ");
    }

    void Update()
    {
        _healthBar.PosUpdate();
    }

    public void TakeDamage(int value)
    {
        _currentHealth -= value;
        _healthBar.SetHealth(_currentHealth);
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _healthBar.SetHealth(_currentHealth);
            //OnHealthZero?.Invoke();
        }
    }

    // Fonction pour restaurer la santé
    public void Heal(int healAmount)
    {
        _currentHealth += healAmount;
        _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
        _healthBar.SetHealth(_currentHealth);
    }
}
