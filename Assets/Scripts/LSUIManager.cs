using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

/// <summary>
/// Controlador de la interfaz de usuario para la selección de niveles.
/// Maneja el panel de información del nivel y la visualización del texto asociado.
/// </summary>
public class LSUIManager : MonoBehaviour
{
    /// <summary>
    /// Instancia única del LSUIManager, accesible globalmente.
    /// </summary>
    public static LSUIManager instance;

    /// <summary>
    /// Componente de texto que muestra el nombre del nivel actual.
    /// </summary>
    public Text lNameText;

    /// <summary>
    /// Panel de UI que contiene la información del nombre del nivel.
    /// </summary>
    public GameObject lNamePanel;

    /// <summary>
    /// Componente de texto que muestra la cantidad de monedas recolectadas en el nivel.
    /// </summary>
    public Text coinsText;

    /// <summary>
    /// Se llama al iniciar el objeto. Inicializa la instancia y oculta el panel de nombre de nivel.
    /// </summary>
    void Awake()
    {
        instance = this;
        instance.lNamePanel.SetActive(false);
    }

    /// <summary>
    /// Se llama una vez por frame. Actualmente no se utiliza.
    /// </summary>
    void Update()
    {
        
    }
}
