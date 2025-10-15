using UnityEngine;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    [System.Serializable]
    public class OriginalState
    {
        public Transform objTransform;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }

    public List<GameObject> objectsToReset;
    private List<OriginalState> originalStates = new List<OriginalState>();
    public GameObject scoreManager;

    void Start()
    {
        foreach (GameObject obj in objectsToReset)
        {
            OriginalState state = new OriginalState();
            state.objTransform = obj.transform;
            state.position = obj.transform.position;
            state.rotation = obj.transform.rotation;
            state.scale = obj.transform.localScale;
            originalStates.Add(state);
        }
    }

    public void ResetObjects()
    {
        foreach (OriginalState state in originalStates)
        {
            Rigidbody rb = state.objTransform.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Stop alle beweging en rotatie
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                // Maak tijdelijk kinematisch om physics teleport te voorkomen
                rb.isKinematic = true;
            }

            // Reset transform
            state.objTransform.position = state.position;
            state.objTransform.rotation = state.rotation;
            state.objTransform.localScale = state.scale;

            if (rb != null)
            {
                // Physics weer inschakelen
                rb.isKinematic = false;
            }
        }
    }

    public void ResetScore()
    {
        scoreManager.GetComponent<ScoreManager>().ResetScore();
    }
}
