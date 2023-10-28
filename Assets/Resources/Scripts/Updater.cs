using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updater : MonoBehaviour {

    public SpriteRenderer drawer;

    private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite> ();

    public bool LoadSpritesFromAtlas (string path, string overrideBasePath = null, bool isLogging = false, bool skipSpriteName = false) {

        var atlas = Resources.LoadAll<Sprite> (path);

        if (atlas != null && atlas.Length > 0) {

            foreach (var sprite in atlas) {

                if (!sprites.ContainsKey ((overrideBasePath ?? path) + (skipSpriteName ? "" : "/" + sprite.name))) {

                    sprites.Add ((overrideBasePath ?? path) + (skipSpriteName ? "" : "/" + sprite.name), sprite);
                }
            }

            return true;
        }

        return false;
    }

    public Sprite LoadSprite (string path, bool useAliases = true) {

        if (sprites.ContainsKey (path)) {

            return sprites [path];
        } else {

            if (LoadSpritesFromAtlas (path, skipSpriteName: true)) {

                return LoadSprite (path);
            } else {

                Debug.Log ("No sprite '" + path + "'");
                return null;
            }
        }
    }

    List<string> animations = new List<string> ();

    static float deltaFrames = 1 / 20f;

    int animationIndex = 4;
    List<string> currentAnimation = new List<string> ();

    int plus = 1;
    int index = 0;
    float time = deltaFrames;

    Sprite sprite {

        set {

            drawer.sprite = value;
        }
    }

    void LoadAnimation () {

        currentAnimation = new List<Texture> (Resources.LoadAll <Texture> ("Effects/" + animations [animationIndex])).ConvertAll (a => a.name);

        //currentAnimation = new List<string> ((Resources.Load ("Effects/" + animations [animationIndex] + "/List") as TextAsset)
        //    .text.Split ('\n'));

        //currentAnimation.RemoveAll (a => a.Length < 6);
        //for (int i = 0; i < currentAnimation.Count; i++) {

        //    if (currentAnimation [i].Length > 6)
        //    currentAnimation [i] = currentAnimation [i].Remove (currentAnimation [i].Length - 6, 6);
        //}

        //currentAnimation.RemoveRange (0, 2);

        //Debug.Log (currentAnimation.Count);
        //for (int i = 0; i < currentAnimation.Count; i++) {

        //    Debug.Log (currentAnimation [i]);
        //}
    }

    void Start () {

        animations = new List<string> ((Resources.Load ("Effects/List") as TextAsset).text.Split ('\n'));

        for (int i = 0; i < animations.Count; i++) {

            animations [i] = animations [i].Remove (animations [i].Length - 1);
        }

        LoadAnimation ();
    }

    // Update is called once per frame
    void FixedUpdate () {

        var deltaTime = Time.fixedDeltaTime;

        if (currentAnimation.Count > 0) {

            time -= deltaTime;

            if (time < 0) {

                time += deltaFrames;

                index = (index + 1) % currentAnimation.Count;

                if (index == 0) {

                    time += 1f;
                }

                //if (index < 0) {

                    //    index = 0;
                    //    plus = -plus;
                    //}

                    //if (index >= currentAnimation.Count) {

                    //    index = currentAnimation.Count - 1;
                    //    plus = -plus;
                    //}

                    sprite = LoadSprite ("Effects/" + animations [animationIndex] + "/" + currentAnimation [index]);
            }
        }
    }

    private void OnGUI () {

        if (GUI.Button (new Rect (10, 10, 100, 50), "Previous")) {

            animationIndex = (animationIndex - 1);

            if (animationIndex < 0) {

                animationIndex = animations.Count - 1;
            }

            index = 0;
            LoadAnimation ();
            time = 0f;
        }

        if (GUI.Button (new Rect (10, 60, 100, 50), "Next")) {

            animationIndex = (animationIndex + 1) % animations.Count;
            index = 0;
            LoadAnimation ();
            time = 0f;
        }
    }
}
