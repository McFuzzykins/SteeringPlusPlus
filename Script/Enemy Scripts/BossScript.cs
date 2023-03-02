using UnityEngine;

public class BossScript : MonoBehaviour 
{
    int health;
    int travelSpeed;
    int fireSpeed;
    int hitPower;
    int score;

    [SerializeField]
	float verticalSpeed = 2;
	[SerializeField]
	float verticalAmplitude = 1;
	Vector3 sineVer;
	float time;

    void OnTriggerEnter(Collider other)
    {
        // if the player or their bullet hits you....
        if (other.tag == "Player")
        {
            if (health > 0)
            {
                health -= other.GetComponent<IActorTemplate>().SendDamage();
            }
            else
            {
                GameManager.Instance.GetComponent<ScoreManager>().SetScore(score);
                Die();
            }
        }
    }

    void FixedUpdate()
    {
        Attack();
    }

    public void Die()
    {
        GameObject explode = GameObject.Instantiate(Resources.Load("Prefab/explode")) as GameObject;
        explode.transform.position = this.gameObject.transform.position;
        Destroy(this.gameObject);
    }

    public void TakeDamage(int incomingDamage)
    {
        health -= incomingDamage;
    }

    public int SendDamage()
    {
        return hitPower;
    }

    public void Attack()
    {
        time += Time.deltaTime;
        sineVer.y = Mathf.Sin(time * verticalSpeed) * verticalAmplitude;
        transform.position = new Vector3(transform.position.x + travelSpeed * Time.deltaTime,
        transform.position.y + sineVer.y,
        transform.position.z);
    }
}
