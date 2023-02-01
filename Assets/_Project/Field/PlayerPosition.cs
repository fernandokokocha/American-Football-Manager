using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

namespace AmericanFootballManager {
    public enum Position { QB, others };
    public class PlayerPosition : MonoBehaviour {
        public Position Position = Position.others;
    }
}
