using UnityEngine;
using Utils;

namespace Cars.Zones
{
    public class DespawnZone : MonoBehaviour
    {
        public string carTag = "Car";

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(carTag))
            {
                ObjectPool.Instance.ReturnToPool(carTag, other.gameObject);
            }
        }
    }
}