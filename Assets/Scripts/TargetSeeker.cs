using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetSeeker : MonoBehaviour {
    public abstract List<EnemyController> SearchForTargets();
}
