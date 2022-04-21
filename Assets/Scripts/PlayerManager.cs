using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerManager : BaseManager
{
    protected AIManager _aiManager;                         // access the AIManager script so we can effect its health value

    protected override void Start()
    {
        base.Start();

        _aiManager = GetComponent<AIManager>();
        if (_aiManager == null)
        {
            Debug.LogError("AIManager not found");
        }
    }

    #region Attack Functions
    public void OneShot()
    {
        DealDamage(_maxHealth);
        _aiManager.DealDamage(100f);
    }

    public void LightAttack()
    {
        _aiManager.DealDamage(30f);
    }

    public void HeavyAttack()
    {
        _aiManager.DealDamage(50f);
    }
    #endregion
}
