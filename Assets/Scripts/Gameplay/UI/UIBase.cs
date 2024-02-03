using UnityEngine;

namespace Gameplay
{
    public abstract class UIBase : MonoBehaviour
    {
        [SerializeField] protected GameObject goUIRoot = null;

        public abstract void InitUI();

        public virtual void ShowUI() 
        {
            goUIRoot.SetActive(true);
        }

        public virtual void HideUI() 
        {
            goUIRoot.SetActive(false);
        }

        public virtual void UpdateUI() { }
    }
}