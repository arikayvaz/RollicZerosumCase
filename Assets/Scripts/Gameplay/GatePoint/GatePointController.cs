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
        [SerializeField] GameObject[] goWalls = null;
        [SerializeField] Collider colliderEnterance = null;

        [Space]
        [SerializeField] int targetRegisterCount = 0;

        private Action<bool, bool> onOperationComplete = null;

        private bool canRegisterItems = false;
        private int registeredItemCount = 0;
        private bool isLastGate = false;

        private List<CollectibleItem> collectedItems = new List<CollectibleItem>();

        public void InitController(int targetRegisterCount, bool isLastGate) 
        {
            this.targetRegisterCount = targetRegisterCount;
            this.isLastGate = isLastGate;

            colliderEnterance.enabled = true;

            goPlatform.SetActive(false);
            UpdateUI();
        }

        private void OnTriggerEnter(Collider other)
        {
            CollectItem(other.gameObject);
        }

        public void StartGateOperation(Action<bool, bool> onOperationComplete) 
        {
            this.onOperationComplete = onOperationComplete;

            colliderEnterance.enabled = false;
            canRegisterItems = true;

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

            canRegisterItems = false;

            DestroyCollectibles();

            if (registeredItemCount >= targetRegisterCount) //success
                StartSuccessSequence();
            else //fail
                onOperationComplete.Invoke(false, isLastGate);

        }

        private void StartSuccessSequence() 
        {
            const float ANIM_TIME = 1f;
            const float GATE_ROT_Z = 90f;

            goPlatform.SetActive(true);
            textRegisteredCount.gameObject.SetActive(false);

            if (!isLastGate)
            {
                trLeftGate.DORotate(new Vector3(0f, 0f, GATE_ROT_Z), ANIM_TIME);
                trRightGate.DORotate(new Vector3(0f, 0f, -GATE_ROT_Z), ANIM_TIME);
            }

            goPlatform.transform.DOMoveY(0f, ANIM_TIME).OnComplete(() => { 
                onOperationComplete.Invoke(true, isLastGate);

                foreach (GameObject wall in goWalls)
                {
                    wall.gameObject.SetActive(false);
                }
            });
        }

        private void CollectItem(GameObject goCollided) 
        {
            CollectibleItem item = goCollided.gameObject.GetComponent<CollectibleItem>();

            if (item == null)
                return;

            collectedItems.Add(item);

            if (canRegisterItems)
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