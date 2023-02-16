using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourcesLoader
{
    public static Sprite LoadSprite(ResourcePath path) => Resources.Load<Sprite>(path.PathResource);

    public static GameObject LoadPrefab(ResourcePath path) => Resources.Load<GameObject>(path.PathResource);
}
