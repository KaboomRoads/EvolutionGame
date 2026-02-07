using UnityEngine;

public class EvolutionData : MonoBehaviour
{
    public int points;
    public BodyPart head;
    public BodyPart leftLeg;
    public BodyPart rightLeg;
    public BodyPart leftArm;
    public BodyPart rightArm;
    public BodyPart tail;
    public Brain.Brain brain;

    public void Eat(int points)
    {
        this.points += points;
    }
}