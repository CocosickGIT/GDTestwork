using System;
using UnityEngine;

namespace Enemy
{
    public class VisibleStateProvider : MonoBehaviour
    {
        public event Action<bool> OnVisibleStateChanged;

        public void ClearCallbacks() => OnVisibleStateChanged = null;
        private void OnBecameInvisible() => OnVisibleStateChanged?.Invoke(false);
        private void OnBecameVisible() => OnVisibleStateChanged?.Invoke(true);
    }
}
