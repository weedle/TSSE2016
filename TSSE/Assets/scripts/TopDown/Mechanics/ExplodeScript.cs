using UnityEngine;
using System.Collections;

public class ExplodeScript : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //destroy engine fire;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ShipDefinitions.stringToFaction(animator.transform.tag) == ShipDefinitions.Faction.Enemy)
        {
            int score = PlayerPrefs.GetInt("score");
            if (animator.transform.GetComponent<ShipIntf>().getShipType() == 0)
                score += 100;
            else
                score += 200;
            PlayerPrefs.SetInt("score", score);
            PlayerPrefs.Save();
        }
        DestroyObject(animator.transform.parent.gameObject);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}