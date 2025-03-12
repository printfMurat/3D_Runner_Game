using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{

    public TextMeshProUGUI goldCount; // Altýn sayýsý
    private float gold = 0; 
   

    public void AddGold(float amount)
    {
        gold += amount; 
        goldCount.text = gold.ToString();
    } 
}


