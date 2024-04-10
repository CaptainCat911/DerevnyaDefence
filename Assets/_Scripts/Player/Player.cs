using UnityEngine;
using UnityEngine.AI;


public class Player : Fighter
{
    // Ссылки
    NavMeshAgent agent;
    [HideInInspector] public Animator anim;
    
    [Header("Параметры курсора")]
    public Transform pointer;                       // прицел               
    public bool aiming = true;                      // прицеливание         
    public LayerMask cursorLayerMask;               // маска для прицела   

    // Перемещение
    Vector2 input;                                  // вектор для приёма значений с клавиатуры (WASD)
    Vector3 movementVector;                         // вектор перемещения    
    float locomationAnimationSmoothTime = 0.1f;     // сглаживание скорости перемещения для анимации


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
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            
        }


        //-------------------------- Перемещение -----------------------\\

        float x = Input.GetAxis("Horizontal");          // принимаем значения осей (управление с клавиатуры (A,D))
        float z = Input.GetAxis("Vertical");            // принимаем значения осей (управление с клавиатуры (W,S))
        input = new Vector2(x, z);                      // создаем вектор из этих значений
        input.Normalize();                              // нормализируем, чтобы по диагонали оставалось значение 1
        UpdateMotor(input);                             // вызываем функцию движения


        //-------------------------- Прицел -----------------------\\

        Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.yellow);
        RaycastHit hit;
        if (Physics.Raycast(ray1, out hit, Mathf.Infinity, cursorLayerMask))
        {
            /*            Vector3 direction = hit.point - transform.position;
                        direction.y = 0f;
                        direction.Normalize();
                        transform.forward = direction;*/

            pointer.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);

            //hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            //Material mat1 = hit.collider.gameObject.GetComponent<Renderer>().material;            
            //ChangeAlpha(mat1, 0.5f);
        }


        //-------------------------- Поворот -----------------------\\

        if (aiming)
        {
            Vector3 lookDir = pointer.position - transform.position;
            float angle = Mathf.Atan2(lookDir.x, lookDir.z) * Mathf.Rad2Deg;                                                // находим угол 
            //float angleRound = Mathf.Round(angle);        
            Quaternion qua1 = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);                    // создаем этот угол            
            transform.rotation = Quaternion.Lerp(transform.rotation, qua1, Time.fixedDeltaTime * agent.angularSpeed);       // делаем Lerp  (НЕ РАБОТАЕТ ПОЧЕМУ ТО)
        }

        /*        // Для разворота без прицеливания

        if (aiming == false && (x != 0 || z != 0))
        {
            // Находим угол             
            float angle = Mathf.Atan2(motorVect.x, motorVect.z) * Mathf.Rad2Deg;

            // Устанавливаем этот угол
            qua1 = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);

            // Делаем Lerp      
            transform.rotation = Quaternion.Lerp(transform.rotation, qua1, Time.fixedDeltaTime * 15f);
        }*/
    }


    private void FixedUpdate()
    {
        
    }

    void UpdateMotor(Vector2 input)             
    {
        movementVector = new Vector3(input.x * agent.speed, 0, input.y * agent.speed);                      // создаем вектор куда нужно переместится        
        agent.Move(movementVector * Time.deltaTime);                                                        // перемещаем с учётом дельтаТайм

        // Для анимаций
        float velocityX = Vector3.Dot(movementVector.normalized, transform.right);                          // вычисляем скорость по горизонтали (x) 
        float velocityZ = Vector3.Dot(movementVector.normalized, transform.forward);                        // вычисляем скорость по вертикали (z) 
        anim.SetFloat("speedPlayerX", velocityX, locomationAnimationSmoothTime, Time.deltaTime);            // анимация скорости по x 
        anim.SetFloat("speedPlayerZ", velocityZ, locomationAnimationSmoothTime, Time.deltaTime);            // анимация скорости по z
        anim.SetFloat("speed", movementVector.magnitude, locomationAnimationSmoothTime, Time.deltaTime);    // анимация скорости общей, чтобы менять бег и ходьбу        
    }



    //---------------------------------------------------------------------------------------------------------------------------------------------------------\\


    protected override void TakeDamage(int dmg)
    {
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