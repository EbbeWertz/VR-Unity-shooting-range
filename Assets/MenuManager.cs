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
            state.objTransform.position = state.position;
            state.objTransform.rotation = state.rotation;
            state.objTransform.localScale = state.scale;
        }
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.Save();
    }
}
