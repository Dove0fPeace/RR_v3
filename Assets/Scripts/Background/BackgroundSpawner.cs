using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField] private BackgroundObject[] _BackgroundPrefabs;

    [SerializeField] private BackgroundObject[] _BGSwitchPrefabs;

    [SerializeField] private BackgroundObject _CurrentBG;

    [SerializeField] private BackgroundObject _NextBG;

    [SerializeField] public static List <BackgroundObject> PastBG = new List<BackgroundObject>();

    private bool _nextIsSwitch;

    private int _removeBuffer = 4;

    private int _genCycle;

    private void Start()
    {
        _nextIsSwitch = true;
    }

    private void Update()
    {
        Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, 0));

        if(cameraPosition.y >= _CurrentBG.TopPosition.y)
        {
            GenerateNextBackground();
        }

    }

    private void GenerateNextBackground()
    {
        BackgroundObject nextBG = null;

        switch(_nextIsSwitch)
        {
            case true:
                nextBG = Instantiate(_BGSwitchPrefabs[Random.Range(0, _BGSwitchPrefabs.Length)]);
                _nextIsSwitch = !_nextIsSwitch;
                break;

            case false:
                nextBG = Instantiate(_BackgroundPrefabs[Random.Range(0, _BackgroundPrefabs.Length)]);
                _nextIsSwitch = !_nextIsSwitch;
                break;
        }

        nextBG.transform.position = new Vector2(_NextBG.TopPosition.x ,_NextBG.TopPosition.y + nextBG.HalfYSize);

        PastBG.Add(_CurrentBG);
        _CurrentBG = _NextBG;
        _NextBG = nextBG;

        _genCycle++;
        if(_genCycle == _removeBuffer)
        {
            BackgroundObject bg = PastBG[0];
            Destroy(bg.gameObject);

            _genCycle = 0;
            _removeBuffer = 2;
        }
    }

}
