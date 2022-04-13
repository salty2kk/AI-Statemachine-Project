using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseManager : MonoBehaviour
{
    //protected is basically private, but inherited classes also have access to it.
    //All classes that inherit from this script will have these variables in the inspector.
    [SerializeField] protected float _health = 100;
    [SerializeField] protected float _maxHealth = 100;          
    [SerializeField] protected Text _healthText;

    //virtual allows the function to be "overridden" by child classes
    //override replaces parent class's function (must be marked virtual)
    protected virtual void Start()
    {
        UpdateHealthText();
    }

    //abstract classes cannot be used, only childern of abstract classes
    //abstract function (inside an abstract class) has to be implemented by child classes


    public void UpdateHealthText()
    {
        if (_healthText != null)                                                   // if health is above 0
        {
            _healthText.text = _health.ToString("HP = 0");                         // update health text
        }
    }


    public void DealDamage(float damage)
    {
        _health = Mathf.Max(_health - damage, 0);

        if (_health <= 0)
        {
            //_health = 0;
            Debug.Log("I Died");
        }

        UpdateHealthText();
    }
}

