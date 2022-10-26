using UnityEngine;
using UnityEngine.AI;


public class Player : Fighter
{
    // Ссылки
    private NavMeshAgent agent;
    private Animator anim;

    // Перемещение
    Vector2 input;
    Vector3 movementVector;

    /*// Для прицела
    public Transform pointer;           // прицел       
    public bool aiming = true;          // прицеливание   
    public LayerMask layerMask;         // маска для прицела   */

    //---------------------------------------------------------------------------------------------------------------------------------------------------------\\


    void Start()
    {        
        //pointer = transform.Find("Sphere_Aim").gameObject.GetComponent<Transform>(); 
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();    
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------\\

    private void Update()
    {

        float x = Input.GetAxis("Horizontal");          // принимаем значения осей (управление с клавиатуры (A,D))
        float z = Input.GetAxis("Vertical");            // принимаем значения осей (управление с клавиатуры (W,S))
        input = new Vector2(x, z);                      // создаем вектор из этих значений
        input.Normalize();                              // нормализируем, чтобы по диагонали оставалось значение 1
        UpdateMotor(input);                             // вызываем функцию движения (возможно нужно перенести в FixedUpdate)


        if (Input.GetKeyDown(KeyCode.T))
        {
            
        }



        /*//-------------------------- Прицел -----------------------\\
        //Ray ray1 = new Ray(transform.position, transform.forward);
        Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(transform.position, transform.forward * 100f, Color.yellow);
        RaycastHit hit;
        if (Physics.Raycast(ray1, out hit, Mathf.Infinity, layerMask))
        {
            *//*            var direction = hit.point - transform.position;
                        direction.y = 0f;
                        direction.Normalize();
                        transform.forward = direction;*//*

            pointer.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);

            //hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.yellow;           
            
            //Material mat1 = hit.collider.gameObject.GetComponent<Renderer>().material;            
            //ChangeAlpha(mat1, 0.5f);
        }              

        if (aiming)         // или weapon.attacking
        {
            // Находим угол 
            Vector3 lookDir = pointer.position - transform.position;
            float angle = Mathf.Atan2(lookDir.x, lookDir.z) * Mathf.Rad2Deg;
            //float angleRound = Mathf.Round(angle);

            // Устанавливаем этот угол
            Quaternion qua1 = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);

            // Делаем Lerp      
            transform.rotation = Quaternion.Lerp(transform.rotation, qua1, Time.fixedDeltaTime * 15f);            
        }*/
    }


    private void FixedUpdate()
    {      
        /*
       // Для разворота без прицеливания

       if (aiming == false && (x != 0 ||  z != 0) && !weapon.attacking)
       {          
               // Находим угол             
           float angle = Mathf.Atan2(motorVect.x, motorVect.z) * Mathf.Rad2Deg;            

               // Устанавливаем этот угол
           qua1 = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);

               // Делаем Lerp      
           transform.rotation = Quaternion.Lerp(transform.rotation, qua1, Time.fixedDeltaTime * 15f);
       }
       */
    }

    void UpdateMotor(Vector2 input)             
    {
        movementVector = new Vector3(input.x * moveSpeed, 0, input.y * moveSpeed);      // создаем вектор куда нужно переместится        
        agent.Move(movementVector * Time.deltaTime);                                    // перемещаем с учётом дельтаТайм
    }



    //---------------------------------------------------------------------------------------------------------------------------------------------------------\\


    protected override void TakeDamage(int dmg)
    {
/*        if (!isAlive)
            return;*/
        base.TakeDamage(dmg);
        //anim.SetTrigger("Take_Hit");
    }


    protected override void Heal(int healingAmount)
    {
        base.Heal(healingAmount);
    }


    public void Respawn()
    {
        Heal(maxHealth);
        isAlive = true;
    }


    protected override void Death()
    {                
        //anim.SetTrigger("Death");                                       // вкл анимацию поражения        
    }
}