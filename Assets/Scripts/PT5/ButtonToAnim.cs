using UnityEngine;
using UnityEngine.UI;

public class ButtonToAnim : MonoBehaviour
{
    [SerializeField] GameObject model;
    [SerializeField] Button idleButton;
    [SerializeField] Button skillButton;
    [SerializeField] Button deathButton;

    Animator animator;

    private void Start() 
    { 
        animator = model.GetComponent<Animator>();

        idleButton.onClick.AddListener(StartIdle);
        skillButton.onClick.AddListener(StartSkill);
        deathButton.onClick.AddListener(StartDeath);
    }

    void StartIdle() => animator.SetTrigger("Idle");
    void StartSkill() => animator.SetTrigger("Skill");
    void StartDeath() => animator.SetTrigger("Death");
     
    private void OnEnable() 
    {
        idleButton.onClick.AddListener(StartIdle);
        skillButton.onClick.AddListener(StartSkill);
        deathButton.onClick.AddListener(StartDeath);
    }
}
