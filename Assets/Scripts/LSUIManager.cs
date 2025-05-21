using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class LSUIManager : MonoBehaviour
{

    public static LSUIManager instance;

    public Text lNameText;

    public GameObject lNamePanel;

    public Text coinsText;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        instance.lNamePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
