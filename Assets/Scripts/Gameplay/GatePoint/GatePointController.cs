using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class GatePointController : MonoBehaviour
    {
        [SerializeField] TMP_Text textRegisteredCount = null;
        [SerializeField] GameObject goPlatform = null;
        [SerializeField] Transform trLeftGate = null;
        [SerializeField] Transform trRightGate = null;

        [Space]
        [SerializeField] int targetRegisterCount = 0;

        private Action<bool> _onOperationComplete = null;

        private bool _canRegisterItems = false;
        private int registeredItemCount = 0;

        private List<CollectibleItem> collectedItems = new List<CollectibleItem>();

        public void InitController(int targetValue) 
        {
            targetRegisterCount = targetValue;
        }

        private void Awake()
        {
            goPlatform.SetActive(false);
            UpdateUI();
        }

        private void OnTriggerEnter(Collider other)
        {
            CollectItem(other.gameObject);
        }

        public void StartGateOperation(Action<bool> onOperationComplete) 
        {
            _onOperationComplete = onOperationComplete;

            _canRegisterItems = true;

            StartCoroutine(StartGateSequence());
        }

        public GatePointLevelInfoModel GetLevelInfoModel() 
        {
            return new GatePointLevelInfoModel(transform.position.z, targetRegisterCount);
        }

        const float WAIT_TIME = 3f;

        private IEnumerator StartGateSequence() 
        {
            yield return new WaitForSeconds(WAIT_TIME);

            _canRegisterItems = false;

            DestroyCollectibles();

            if (registeredItemCount >= targetRegisterCount) //success
                StartSuccessSequence();
            else //fail
                _onOperationComplete.Invoke(false);

        }

        private void StartSuccessSequence() 
        {
            const float ANIM_TIME = 1f;
            const float GATE_ROT_Z = 90f;

            goPlatform.SetActive(true);
            textRegisteredCount.gameObject.SetActive(false);

            trLeftGate.DORotate(new Vector3(0f, 0f, GATE_ROT_Z), ANIM_TIME);
            trRightGate.DORotate(new Vector3(0f, 0f, -GATE_ROT_Z), ANIM_TIME);

            goPlatform.transform.DOMoveY(0f, ANIM_TIME).OnComplete(() => _onOperationComplete.Invoke(true));
        }

        private void CollectItem(GameObject goCollided) 
        {
            CollectibleItem item = goCollided.gameObject.GetComponent<CollectibleItem>();

            if (item == null)
                return;

            collectedItems.Add(item);

            if (_canRegisterItems)
                RegisterItem();
        }

        private void RegisterItem() 
        {
            registeredItemCount++;
            UpdateUI();
        }

        private void UpdateUI() 
        {
            textRegisteredCount.text = $"{registeredItemCount}/{targetRegisterCount}";
        }

        private void DestroyCollectibles() 
        {
            foreach (CollectibleItem item in collectedItems)
            {
                //Play Particle
                item.gameObject.SetActive(false);
            }

        }
    }
}