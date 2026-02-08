using System;
using UnityEngine;

public class EvolutionData : MonoBehaviour
{
    public static EvolutionData instance;

    private void Awake()
    {
        if (instance is null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public int points;
    public BodyPart head;
    public BodyPart leftLeg;
    public BodyPart rightLeg;
    public BodyPart leftArm;
    public BodyPart rightArm;
    public BodyPart tail;
    public Brain.Brain brain;

    public BodyPart LimbFromName(string name)
    {
        return name switch
        {
            "head" => head,
            "leftLeg" => leftLeg,
            "rightLeg" => rightLeg,
            "leftArm" => leftArm,
            "rightArm" => rightArm,
            "tail" => tail,
            _ => throw new ArgumentOutOfRangeException(nameof(name), name, null)
        };
    }

    public bool AnyEcho()
    {
        return head.enablesEcholocation || leftLeg.enablesEcholocation || rightLeg.enablesEcholocation || leftArm.enablesEcholocation || rightArm.enablesEcholocation || tail.enablesEcholocation;
    }

    public void Eat(int points)
    {
        this.points += points;
    }
}