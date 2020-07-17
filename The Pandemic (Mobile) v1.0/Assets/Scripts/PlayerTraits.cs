using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTraits
{
    public bool _isInfected;
}

public class InfectedPlayerTraits
{
    public float spreadChance;

    public InfectedPlayerTraits(float spreadChance)
    {
        this.spreadChance = spreadChance;
    }
}

public class HealthyPlayerTraits
{
    public float vulnerability;

    public HealthyPlayerTraits(float vulnerability)
    {
        this.vulnerability = vulnerability;
    }
}
