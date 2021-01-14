using System.Collections;
using System.Linq;
using SOHNE.Accessibility.Colorblindness;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEditor;

public enum ColorblindTypesFrench
{
    Normal,
    Protanopie,
    Protanomalie,
    Deutéronome,
    Deutéranomalie,
    Tritanopie,
    Tritanomalie,
    Achromatopsie,
    Achromatomalie,
}

public class CustomColorblindness : MonoBehaviour
{
    public KeyCode changeKey = KeyCode.F1;

    // TODO: Clear saved settings

    private Volume[] _volumes;
    private VolumeComponent _lastFilter;

    private int _maxType;
    private int _currentType = 0;

    public int CurrentType
    {
        get => _currentType;

        set
        {
            if (_currentType >= _maxType) _currentType = 0;
            else _currentType = value;
        }
    }

    private void SearchVolumes() => _volumes = FindObjectsOfType<Volume>();

    #region Enable/Disable

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;

    #endregion

    public static CustomColorblindness Instance { get; private set; }

    [UnityEditor.Callbacks.DidReloadScripts]
    private static void OnScriptsReloaded()
    {
#if !RENDERPIPELINE
        //Debug.LogError("There is no type of <b>SRP</b> included in this project.");
#endif
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _maxType = (int) System.Enum.GetValues(typeof(ColorblindTypes)).Cast<ColorblindTypes>().Last();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SearchVolumes();

        if (_volumes == null || _volumes.Length <= 0) return;

        Change(-1);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Accessibility.ColorblindType"))
            CurrentType = PlayerPrefs.GetInt("Accessibility.ColorblindType");
        else
            PlayerPrefs.SetInt("Accessibility.ColorblindType", 0);

        SearchVolumes();
        StartCoroutine(ApplyFilter());
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(changeKey)) InitChange();
    }

    public void Change(int filterIndex = -1)
    {
        filterIndex = filterIndex <= -1 ? PlayerPrefs.GetInt("Accessibility.ColorblindType") : filterIndex;
        CurrentType = Mathf.Clamp(filterIndex, 0, _maxType);
        StartCoroutine(ApplyFilter());
    }

    public virtual void InitChange()
    {
        if (_volumes == null) return;
#if UNITY_EDITOR
        // TODO: Use a public event system to announce the change of the activated filter
        Debug.Log($"Color changed to <b>{(ColorblindTypesFrench) CurrentType} {CurrentType}</b>/{_maxType}");
#endif
        StartCoroutine(ApplyFilter());

        PlayerPrefs.SetInt("Accessibility.ColorblindType", CurrentType);
        CurrentType++;
    }

    private IEnumerator ApplyFilter()
    {
        ResourceRequest loadRequest = Resources.LoadAsync<VolumeProfile>($"Colorblind/{(ColorblindTypes) CurrentType}");

        do yield return null;
        while (!loadRequest.isDone);

        var filter = loadRequest.asset as VolumeProfile;

        if (filter == null)
        {
            Debug.LogError("An error has occured! Please, report");
            yield break;
        }

        if (_lastFilter != null)
        {
            foreach (var volume in _volumes)
            {
                volume.profile.components.Remove(_lastFilter);

                foreach (var component in filter.components)
                    volume.profile.components.Add(component);
            }
        }

        _lastFilter = filter.components[0];
    }
}