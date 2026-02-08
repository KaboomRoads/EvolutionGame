using UnityEngine;

public class Food : MonoBehaviour
{
    public int reward;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            var evolutionData = EvolutionData.instance;
            evolutionData.points += reward;
            Destroy(gameObject);
        }
    }
}