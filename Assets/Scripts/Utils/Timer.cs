using System;
using UnityEngine;

namespace Utils
{
    public class Timer
    {
        public float duration;
        public float elapsedTime;
        public bool isActive;
        public Action onComplete;
        
        public Timer(float duration, Action onComplete)
        {
            this.duration = duration;
            this.onComplete = onComplete;
        }
        
        public void Update()
        {
            if (!isActive) return;
            
            elapsedTime += Time.deltaTime;
            Debug.Log("Elapsed time: " + elapsedTime);
            if (elapsedTime >= duration)
            {
                Reset();
                isActive = false;
                onComplete?.Invoke();
            }
        }

        private void Reset()
        {
            elapsedTime = 0;
        }
    }
}
