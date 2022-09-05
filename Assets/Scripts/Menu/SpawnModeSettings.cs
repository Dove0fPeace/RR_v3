using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SpawnMode
{
    Single = 0,
    Multiply = 1,
}

public class SpawnModeSettings : MonoBehaviour
{
    [SerializeField] private Text m_Text;

    private SpawnMode _spawnMode = 0;

    private void Start()
    {
        UpdateSpawnMode();
    }

    public void ChangeSpawnMode()
    {
        var length = System.Enum.GetNames(typeof(SpawnMode)).Length;
        _spawnMode = (SpawnMode)(((int)_spawnMode + 1) % length);
        UpdateSpawnMode();
    }

    private void UpdateSpawnMode()
    {
        PlayerPrefs.SetInt("SpawnMode", (int)_spawnMode);
        switch((int)_spawnMode)
        {
            case 0:
                m_Text.text = "SpawnMode - Single";
                break;
            case 1:
                m_Text.text = "SpawnMode - Multiply";
                break;
        }
    }
}
