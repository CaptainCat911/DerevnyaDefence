using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;         // инстанс (объект одиночка ?)

    [Header("Ссылки")]
    [Tooltip("Ссылка на игрока")]
    public Player player;                       // ссылка на игрока
    public Terrain terrain;                     // ссылка на террейн   


    //---------------------------------------------------------------------------------------------------------------------------------------------------------\\


    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);            
            return;
        }
        // присваем instance (?) этому обьекту и по ивенту загрузки запускаем функцию загрузки
        instance = this;      
    }

        

    public void Start()
    {
        
    }

//---------------------------------------------------------------------------------------------------------------------------------------------------------\\


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            
        }
    }    
       

//---------------------------------------------------------------------------------------------------------------------------------------------------------\\

}
