using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : BaseCharacterManager
{
    public new void Start()
    {
        base.Start();
        Health = 2;
    }

    public new void Update()
    {
        Wander();
        base.Update();
    }

    private void Wander()
    {

    }
}
