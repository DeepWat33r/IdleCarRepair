using UnityEngine;

namespace UI
{
    public class LookAtObject : MonoBehaviour
    {
        public GameObject target;
        void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - target.transform.position);
        }
    }
}
