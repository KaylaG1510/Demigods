using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class MovementTest
    {
        private Movement movement;

        [UnityTest]
        public IEnumerator Move_Character_Left_Using_Left_Arrow_Key()
        {
            GameObject gameGameObject =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("Scripts/CharacterMovement"));
            movement = gameGameObject.GetComponent<Movement>();
            
            float initialXPos = movement.transform.position.x;
            yield return new WaitForSeconds(0.1f);

            Assert.Less(movement.transform.position.x, initialXPos);
            Object.Destroy(movement.gameObject);
        }
    }
}
