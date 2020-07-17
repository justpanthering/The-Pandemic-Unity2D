using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTraits
{
    public bool _isBad;
    public bool _isInfected;
}

public class InfectedNPCTraits
{
    public float spreadChance;

    public InfectedNPCTraits(float spreadChance)
    {
        this.spreadChance = spreadChance;
    }
}

public class HealthyNPCTraits
{
    public float vulnerability;

    public HealthyNPCTraits(float vulnerability)
    {
        this.vulnerability = vulnerability;
    }
}
