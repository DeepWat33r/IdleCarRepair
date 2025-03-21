using System.Collections.Generic;
using InventorySystem;
using UnityEngine;

namespace Cars
{
    public class CarRepairNeeds : MonoBehaviour
    {
        public List<Item> requiredParts = new List<Item>();

        [Header("Item Pool")]
        public Item[] allPossibleParts; 

        public void GeneratePartToRepair()
        {
            requiredParts.Clear();

            int partsToRepair = Random.Range(1, 5);

            List<Item> shuffledParts = new List<Item>(allPossibleParts);
            
            for (int i = 0; i < shuffledParts.Count; i++)
            {
                Item temp = shuffledParts[i];
                int randomIndex = Random.Range(i, shuffledParts.Count);
                shuffledParts[i] = shuffledParts[randomIndex];
                shuffledParts[randomIndex] = temp;
            }

            for (int i = 0; i < partsToRepair; i++)
            {
                requiredParts.Add(shuffledParts[i]);
            }
        }
    }
}
