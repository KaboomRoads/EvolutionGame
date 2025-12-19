using UnityEngine;

public class EvolutionData : MonoBehaviour
{
    public int points;
    public GameObject head;
    public GameObject leftLeg;
    public GameObject rightLeg;
    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject tail;
    public Brain.Brain brain;

    public void Eat(int points)
    {
        this.points += points;
    }
}