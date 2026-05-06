using UnityEngine;

public class TestTarget : MonoBehaviour
{
    [SerializeField] private int minDamage = 5;
    [SerializeField] private int maxDamage = 50;
    [SerializeField] private int minHeal = 10;
    [SerializeField] private int maxHeal = 60;
    [SerializeField] private float criticalChange = 0.2f;
    [SerializeField] private float missChange = 0.1f;
    [SerializeField] private float statusEffectChange = 0.15f;

    private string[] statusEffects = { "Posison", "Burn", "Freeze", "Stun", "Blind", "Silence" };

    private void ShowDamage(int amount , bool isCritical)
    {
        if(DamageEffectManager.instance != null)
        {
            Vector3 position = transform.position;
            position += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1.0f, 1.5f), 0);
            DamageEffectManager.instance.ShowDamage(position, amount, isCritical);

        }
    }
   

    private void ShowHeal(int amount, bool isCritical)
    {
        if(DamageEffectManager.instance != null)
        {
            Vector3 position = transform.position;
            position += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1.0f, 1.5f), 0);
            DamageEffectManager.instance.ShowHeal(position, amount, isCritical);
        }
    }

    private void ShowMiss()
    {
        if (DamageEffectManager.instance != null)
        {
            Vector3 position = transform.position;
            position += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1.0f, 1.5f), 0);
            DamageEffectManager.instance.ShowMiss(position);
        }
     
    }

    private void ShowStatusEffect(string effectName)
    {
        if (DamageEffectManager.instance != null)
        {
            Vector3 position = transform.position;
            position += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1.0f, 1.5f), 0);
            DamageEffectManager.instance.ShowStatusEffect(position, effectName); 
        }
    }

    private void OnMouseDown()
    {
        float randomVaule = Random.value;

        if(randomVaule < missChange)
        {
            ShowMiss();
        }
        else if(randomVaule < 0.5f)
        {
            bool isCritical = Random.value < criticalChange;
            int damage = Random.Range(minDamage, maxDamage + 1);

            if (isCritical) damage *= 2;

            ShowDamage(damage, isCritical);

            if(Random.value < statusEffectChange)
            {
                string statusEffect = statusEffects[Random.Range(0, statusEffects.Length)];
                ShowStatusEffect(statusEffect);
            }
        }
        else
        {
            bool isCritical = Random.value < criticalChange;
            int heal = Random.Range(minHeal, maxHeal + 1);

            if (isCritical) heal = Mathf.RoundToInt(maxHeal * 1.5f);
            ShowHeal(heal, isCritical);
           
        }
    }
}
