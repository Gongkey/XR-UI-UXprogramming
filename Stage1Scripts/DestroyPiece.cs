using UnityEngine;

public class DestroyPiece : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody rig;
    float time = 3;
    
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rig.useGravity == true) // 여기는 직접 구현 ( deltaTime을 이용하여 컴퓨터별로 프레임 차이 완화)
        {
            time -= Time.deltaTime;
        }

        if(time <= 0)  // 여기는 직접 구현  (부서진 객체를 3초 후에 사라지게 함)
        {
            Destroy(gameObject);
        }

    }


}
