using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace TestPause
{
    public class PauseTest : UITest
    {
        [UnityTest]
        public IEnumerator pauseMenuDisplays()
        {
            yield return LoadScene("LevelOne");

            yield return new WaitForSecondsRealtime(2);

            yield return Press("PauseButton");

            yield return WaitFor(new ObjectAppeared("PauseMenu"));
        }

        [UnityTest]
        public IEnumerator gameplayFreezes()
        {
            yield return LoadScene("LevelOne");

            yield return Press("PauseButton");

            yield return WaitFor(new ObjectAppeared("PauseMenu"));

            //yield return WaitFor(new BoolCondition(() => Time.timeScale == 0));


            GameObject playerObject = GameObject.FindGameObjectWithTag("Player").gameObject;
            //this.animator.GetCurrentAnimatorStateInfo(0).IsName("YourAnimationName");
            //playerObject = GameObject.Instantiate(Resources.Load("Prefabs/Hero")) as GameObject;

            Animator currentAnim = playerObject.GetComponent<Animator>();

            yield return WaitFor(new BoolCondition(() => currentAnim.updateMode == AnimatorUpdateMode.Normal));
        }

        [UnityTest]
        public IEnumerator sceneRestarts()
        {
            yield return null;
        }
    }
}
