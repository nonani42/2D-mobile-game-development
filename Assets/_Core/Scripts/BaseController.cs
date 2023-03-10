using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class BaseController : IDisposable
{
    private List<BaseController> _baseControllers;
    private List<GameObject> _gameObjects;
    private bool _isDisposed;


    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        _isDisposed = true;

        DisposeBaseControllers();
        DisposeGameObjects();

        OnDispose();
    }

    private void DisposeBaseControllers()
    {
        if(_baseControllers == null)
        {
            return;
        }

        foreach (BaseController controller in _baseControllers)
        {
            controller.Dispose();
        }

        _baseControllers.Clear();
    }

    private void DisposeGameObjects()
    {
        if (_gameObjects == null)
        {
            return;
        }

        foreach (GameObject gameObject in _gameObjects)
        {
            Object.Destroy(gameObject);
        }

        _gameObjects.Clear();

    }

    protected virtual void OnDispose() {    }

    protected void AddGameObject(GameObject gameObject)
    {
        _gameObjects = (_gameObjects != null) ? _gameObjects : new List<GameObject>();
        _gameObjects.Add(gameObject);
    }

    protected void AddController(BaseController controller)
    {
        _baseControllers = (_baseControllers != null) ? _baseControllers : new List<BaseController>();
        _baseControllers.Add(controller);
    }
}
