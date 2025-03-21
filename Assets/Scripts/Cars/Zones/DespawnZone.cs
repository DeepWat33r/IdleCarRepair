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
                CarMovement carMovement = other.GetComponent<CarMovement>();
                carMovement.isNeedRepair = true;
                carMovement.isNeedFuel = true;
                ObjectPool.Instance.ReturnToPool(carTag, other.gameObject);
            }
        }
    }
}