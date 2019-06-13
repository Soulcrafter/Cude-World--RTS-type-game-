using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Manager {

    public static GameManager gameManager;

    public Manager () {

        if (gameManager == null) {
            gameManager = GameObject.FindObjectOfType<GameManager>();
        }
    }

    public abstract void Start();
    public virtual void Update(float dt) { }
}
