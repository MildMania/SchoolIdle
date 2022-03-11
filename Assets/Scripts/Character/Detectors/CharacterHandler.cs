using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    [SerializeField] private BaseUncollectCommand _uncollectCommand;
    [SerializeField] private CollectibleController _collectibleController;

    private BaseCharacterDetector[] _characterDetectors;
    private List<Collectible> _collectedCollectibles;
    public Action<Character> OnCharacterCollided { get; set; }

    private Character _character;

    public BaseCharacterDetector[] CharacterDetectors
    {
        get
        {
            if (_characterDetectors == null)
            {
                _characterDetectors = GetComponentsInChildren<BaseCharacterDetector>();
            }

            return _characterDetectors;
        }
    }

    private void Awake()
    {
        _collectedCollectibles = _collectibleController.CollectedCollectibles;

        foreach (var characterDetector in CharacterDetectors)
        {
            characterDetector.OnDetected += OnDetected;
        }
    }

    private void OnApplicationQuit()
    {
        foreach (var characterDetector in CharacterDetectors)
        {
            characterDetector.OnDetected -= OnDetected;
        }
    }

    private void OnDetected(Character character)
    {
        Debug.Log("ONDETECTED");
        BaseUncollectCommand uncollectCommandClone = null;
        if (_uncollectCommand != null)
        {
            _character = character;
            uncollectCommandClone = CreateCommand();
        }

        Collectible collectible = GetLastCollectible();
        if (collectible != null)
        {
            collectible.OnUncollected += OnUncollected;
            collectible.TryUncollect(uncollectCommandClone);
        }

        character.OnCollided += OnCollided;
        character.TryCollide();
    }

    private void OnUncollected(Collectible collectible)
    {
        collectible.OnUncollected -= OnUncollected;
        _collectedCollectibles.Remove(collectible);
    }

    private void OnCollided(Character character)
    {
        OnCharacterCollided?.Invoke(character);
    }

    private BaseUncollectCommand CreateCommand()
    {
        BaseUncollectCommand uncollectCommandClone = Instantiate(_uncollectCommand);
        uncollectCommandClone.CollectedCollectibles = _collectedCollectibles;
        uncollectCommandClone.Character = _character;
        return uncollectCommandClone;
    }

    private Collectible GetLastCollectible()
    {
        Collectible collectible = null;
        if (_collectedCollectibles.Count > 0)
        {
            collectible = _collectedCollectibles[_collectedCollectibles.Count - 1];
        }

        return collectible;
    }

    private Collectible GetFirstCollectible()
    {
        Collectible collectible = null;
        if (_collectedCollectibles.Count > 0)
        {
            collectible = _collectedCollectibles[0];
        }

        return collectible;
    }
}
