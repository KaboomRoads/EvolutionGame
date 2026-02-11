using System.Text;
using Brain;
using TMPro;
using UnityEngine;

public class CodepediaText : MonoBehaviour
{
    private TMP_Text tmpText;

    public void UpdateText()
    {
        var builder = new StringBuilder();
        var evolutionData = EvolutionData.instance;
        foreach (var entry in BrainInstructionRegistry.BY_NAME)
            if (entry.Value.unlockCriteria())
                builder.AppendLine("<color=\"yellow\">" + entry.Value.name + "</color>:").AppendLine("<color=#C0C0C0>" + entry.Value.description + "</color>").AppendLine();

        tmpText.SetText(builder);
    }

    private void Start()
    {
        tmpText = GetComponent<TMP_Text>();
        UpdateText();
    }
}