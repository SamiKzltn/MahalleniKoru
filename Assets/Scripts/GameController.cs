using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public GameObject[] Weapons;
    public GameObject[] _currentWeapons;
    private int _activeWeaponIndex = -1;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _currentWeapons = new GameObject[2];

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(1);
        }
    }
    public void ChangeWeapon(int index)
    {

        SetActiveWeapon(_activeWeaponIndex, false);
        SetActiveWeapon(index, true);
        _activeWeaponIndex = index;

    }

    public void AddWeapon(GameObject weaponObject, int index)
    {
        if (_currentWeapons == null && index >= _currentWeapons.Length) return;
        _currentWeapons[index] = weaponObject;
        if ( _activeWeaponIndex == index)
        {
            ChangeWeapon(index);
            return;
        }
        a();

    }

    public void RemoveWeapon(GameObject weaponObj)
    {
        if (TryGetIndex(weaponObj, out int index))
        {
            if (_activeWeaponIndex == index)
            {
                SetActiveWeapon(index, false);
                _currentWeapons[index] = null;
            }
        }
        a();

    }

    private bool TryGetIndex(GameObject weaponObject, out int index)
    {
        index = -1;
        for (int i1 = 0; i1 < _currentWeapons.Length; i1++)
        {
            if (_currentWeapons[i1] == weaponObject)
            {
                index = i1;
                return true;
            }
        }
        return false;
    }

    private void SetActiveWeapon(int index, bool value)
    {
        if (_currentWeapons == null || index >= _currentWeapons.Length || index < 0) return;
        _currentWeapons[index]?.SetActive(value);
    }

    private void a()
    {
        if (_activeWeaponIndex >= 0 && _currentWeapons[_activeWeaponIndex] != null) return;
        for (int i1 = 0; i1 < _currentWeapons.Length; i1++)
        {
            if (_currentWeapons[i1] != null)
            {
                ChangeWeapon(i1);
                break;
            }
        }
    }
}
