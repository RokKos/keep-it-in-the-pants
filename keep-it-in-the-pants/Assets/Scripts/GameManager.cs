﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public float dickRadius;
    public float positionSendingInterval;

    private void Awake() {
        if (!Instance) {
            Instance = this;
        }
    }
}
