using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoBehaviours
{
    public class SetAnimation : MonoBehaviour
    {
        [SerializeField] private AnimatorOverrideController[] overrideControllers;
        [SerializeField] private AnimatorOverrider overrider;

        public void set(int value)
        {
            overrider.SetAnimations(overrideControllers[value]);
        }
    }
}