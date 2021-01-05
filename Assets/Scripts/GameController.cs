using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using Enums.BattleStates;
using Enums.Stats;
using Enums.Classes;
using AbilitySystem.Abilities;

public class GameController : MonoBehaviour {
    private GameObject playerGO;
    private GameObject enemyGO;
    private BasicAI AI;
    [SerializeField] public static Class player; //El jugador sera alguna de las clases jugables le llegara el valor de otra escena //Siempre sera de tipo clase
    [SerializeField] public static Character enemy; // El enemigo puede ser una de las clases jugables o un mob, clase que heredara de character que es el MonoBehaviour
    SimpleHealthBar playerHP;
    SimpleHealthBar enemyHP;
    public const int MAX_BOTONES = 4;
    public static EBattleStates currentState; //Current state of the game
    public static int turnos = 0; //Turnos que llevan desde que ha comenzado el combate
    //En la pantalla de creacion de personaje lo suyo es que se cree el objeto con la clase y todo ya hecho y se mantenga al cambiar la escena
    //Al acabar el combate se guardan los cambios que ha habido en el objeto
    //Usar singleton
    public List<Button> arrayButt = new List<Button>(); // 4 botones de habilidad
    private GameObject[] spells = new GameObject [MAX_BOTONES]; //4 botones de habilidad
    private SayDialog dialog;
    Writer actualWriter;
    public Text playerText; //Texto del panel del jugador
    public Text enemyText; //Texto del panel del enemigo
    public Text turnosTexto; //Texto que almacena el turno actual
    //La partida predeterminada para probar todo sera necromancer --jugador-- vs pyromancer --maquina--
    // Use this for initialization
    void Awake()
    {
        FindSpells();
        playerHP = SimpleHealthBar.GetSimpleHealthBar("Player_hp");
        enemyHP = SimpleHealthBar.GetSimpleHealthBar("Enemy_hp");
        //HARDCODED - INC
        //playerHP.barColor = CColores.ClassColorDictionary[EClasses.NECROMANCER].normalColor;
        //enemyHP.barColor = CColores.ClassColorDictionary[EClasses.PYROMANCER].normalColor;
        playerHP.UpdateColor(CColores.ClassColorDictionary[EClasses.NECROMANCER].normalColor);
        enemyHP.UpdateColor(CColores.ClassColorDictionary[EClasses.PYROMANCER].normalColor);

        //HARCODED -END
        dialog = FindObjectOfType<SayDialog>(); //Al que llevo el mensaje que quiero escribir
        actualWriter = FindObjectOfType<Writer>(); //Se encarga de escribir en el log
        playerGO = GameObject.Find("Player"); //Obtiene la referencia al GameObject del jugador
        enemyGO = GameObject.Find("Enemy"); //Obtiene la referencia al GameObject del enemigo
        player = playerGO.GetComponent<Class>(); //Obtiene el componente jugador
        enemy = enemyGO.GetComponent<Class>();  //Obtiene el componente enemigo

        playerGO.GetComponent<Image>().sprite = player.ActualImage;
        enemyGO.GetComponent<Image>().sprite = enemy.ActualImage;

        //Lo mas seguro es que en el futuro habra que cambiar esto si metemos mas botones o tener mas cuidado
        if (player != null && enemy != null)
        {
            player.CurrentEnemy = enemy;
            enemy.CurrentEnemy = player;
        }
        else Debug.LogError("player or enemy not found");

        turnosTexto = GameObject.FindGameObjectWithTag("turnos").GetComponent<Text>();
    }

    void Start () {
        currentState = EBattleStates.START; //The combat Starts
        SetPlayerButtons();
        turnos = 0;
        
    }

    // Update is called once per frame
    void Update () {
        /* CONDICION DE VICTORIA,
         *MOSTRAR EFECTOS DE ESTADO
          HECHIZOS QUE PUEDAN APLICAR EFECTOS DE ESTADO
          HABILIDADES QUE PUEDAN CURAR
          DOTS-HOTS
          CREACION DE INTERFAZ JUGADOR?
          CREACION DE INTERFAZ IA?   
          OCULTAR LOS BOTONES EN EL TURNO ENEMIGO
          USAR LA VARIABLE TURNOS PARA CONTAR LOS TURNOS*/
        SetPlayerStatus(player, playerText); //Actualiza la vida y el mana del jugador
        SetPlayerStatus(enemy, enemyText); // Actualiza la vida y el mana del enemigo   
        turnosTexto.text = "Turno actual: " + turnos.ToString();
        Debug.Log(currentState);
        if (!player.IsAlive())
            currentState = EBattleStates.LOSE;
        else if (!enemy.IsAlive())
            currentState = EBattleStates.WIN;

        switch (currentState)
        {
            case EBattleStates.START:
                //functions start
                if (player.Stats[EStats.DEXTERITY].Value > enemy.Stats[EStats.DEXTERITY].Value)
                {
                    //IF the player has more dex than the enemy the player starts the fight
                    currentState = EBattleStates.PLAYER_CHOICE;
                    player.MyTurn = true;
                }
                else
                {
                    //Else the enemy starts the fight
                    currentState = EBattleStates.ENEMY_CHOICE;
                    enemy.MyTurn = true;
                }
                break;

            case EBattleStates.PLAYER_CHOICE:
                //functions player choice
                /*Is the player turn, he must use a spell in order to finish the turn, he can also use an object, just one
                 once he cast the spell the console panel should display if the ability failed or not, the amount, if it was
                 a critical strike and if it has applied an effect state like burning or poisoning*/
                if (!player.MyTurn)
                {
                    DisableAllButtons(); //Deshabilita los botones
                    if (!actualWriter.IsWriting)
                    {
                        TurnActionsRegister(player, player.LastClicked, enemy);
                        currentState = EBattleStates.ENEMY_CHOICE; //Paso turno
                        turnos++;
                        enemy.MyTurn = true; //Turno del enemigo
                    }
                }
                break;


            case EBattleStates.ENEMY_CHOICE:
                //functions enemy choice
                /*Create a IA play and ends turn*/
                /*During enemy turn the player's button abilities shoul dissapear and show only the canvas
                 the canvas will hold an image with the player's class color theme and the icon in the middle which
                 represents the player's race*/
              
                if (!actualWriter.IsWriting)
                {
                    Ability abUsed = enemy.Abilities[(int)Random.value];
                    abUsed.UseAbility();
                    TurnActionsRegister(enemy, abUsed, player);
                }
                if (!enemy.MyTurn) 
                {
                    turnos++;
                    EnableAllButtons();//Vuelvo a habilitar los botones
                    currentState = EBattleStates.PLAYER_CHOICE; //Paso turno
                    player.MyTurn = true; //Turno del jugador
                }
                break;

            case EBattleStates.LOSE:
                //functions lose
                /*You die, you start again be4 the combat?
                 Or start from the begining
                 Open to new features*/
                break;

            case EBattleStates.WIN:
                //functions win
                /*You win, you get rewarded, exp? or items? idk*/
                //Enemy get's destroyed
                Destroy(enemyGO);
                DisableAllButtons();//De momento deshabilita los botones pero llevara la transicion de escena
                StopAllCoroutines();//Para todas las coroutinas
                break;
        }

        

    }

    private void SetPlayerStatus(Character character, Text textPanelDisplay) //Update the status of a player & enemy
    {
        string currentFormat = string.Format("{0}"+"\n"+
            "{1}" + "\n" + "\n"+
            "{2}", character.CharacterName, character.HealthPoints, character.ManaPoints);
        textPanelDisplay.text = currentFormat;
        //SimpleHealthBar.UpdateBar(character.name+"_hp", character.HealthPoints, character.MaxHP);
        SimpleHealthBar.UpdateBar(character.name+"_mana", character.ManaPoints, character.MaxMana);
    }

    private void SetPlayerButtons()//Rellena los botones con el nombre de las habilidades y los ActionListener
    {
        for(int i = 0; i < arrayButt.Capacity; i++)
        {
            arrayButt[i].GetComponentInChildren<Text>().text = player.Abilities[i].InformationObject.Name;//player abilities
            //Cuando pulsa el jugador el boton debe escribir en consola el suceso y pasar turno al rival
            SetButtonColors(player, arrayButt[i]);
        }
        //Aparentemente no puedo automatizarlo, peta, solo se introduce la ultima habilidad en todos los botones
        /* for(int i = 0; i < arrayButt.Length; i++) //RIDICULO SALTA EXCEPCION
        {
            arrayButt[i].onClick.AddListener(() => player.ExecuteAsignedAbility(i));
        }*/
        arrayButt[0].onClick.AddListener(() => player.ExecuteAsignedAbility(0));
        arrayButt[1].onClick.AddListener(() => player.ExecuteAsignedAbility(1));
        arrayButt[2].onClick.AddListener(() => player.ExecuteAsignedAbility(2)); 
        arrayButt[3].onClick.AddListener(() => player.ExecuteAsignedAbility(3)); 
    }

    public void TurnActionsRegister(Character source, Ability abilityCasted, Character enemy) //Rellena el registro de combate al final de cada turno
    {
        /*Quiero que sea del estilo */
        /*_nombre_jugador USO _habilidad_ , _habilidad_ ALCANZO A _nombre_oponente POR _amount_ +
         FUE MUY EFECTIVO,
         NO FUE MUY EFECTIVO
         GOLPE CRITICO,
         PROVOCO + EFECTO DE ESTADO*/

        string phrase = "";
        phrase += string.Format("{0} used {1}. "+"\n"+
              "{2} reached {3} " + " dealing {4} damage points." + "\n", 
        source.name, abilityCasted.InformationObject.Name, abilityCasted.InformationObject.Name, enemy.name,abilityCasted.Amount);
        StartCoroutine(dialog.DoSay(phrase, true, false, false, true, false, null, null));
    }

    private void SetDisplayableImage()
    {
        //Pillar la referencia del GameObject y rellenar el sprite con la imagen guardada en la clase del jugador
        playerGO.GetComponent<Image>().sprite = player.ActualImage;
        enemyGO.GetComponent<Image>().sprite = enemy.ActualImage;

    }

    private void SetButtonColors(Class clase, Button button)
    {
        switch (clase.ClassName)
        {
            case EClasses.NONE:
                Debug.LogWarning("No class");
                break;
            case EClasses.NECROMANCER:
                button.colors = CColores.ClassColorDictionary[EClasses.NECROMANCER];
                break;
            case EClasses.WITHERED_CHAMPION:
                button.colors = CColores.ClassColorDictionary[EClasses.WITHERED_CHAMPION];
                break;
            case EClasses.HUNTER:
                button.colors = CColores.ClassColorDictionary[EClasses.HUNTER];
                break;
            case EClasses.BERSERKER:
                button.colors = CColores.ClassColorDictionary[EClasses.BERSERKER];
                break;
            case EClasses.SHAMAN:
                button.colors = CColores.ClassColorDictionary[EClasses.SHAMAN];
                break;
            case EClasses.ASH_KNIGHT:
                button.colors = CColores.ClassColorDictionary[EClasses.ASH_KNIGHT];
                break;
            case EClasses.PYROMANCER:
                button.colors = CColores.ClassColorDictionary[EClasses.PYROMANCER];
                break;
            case EClasses.WIZARD:
                button.colors = CColores.ClassColorDictionary[EClasses.WIZARD];
                break;
            case EClasses.SNOOPER:
                button.colors = CColores.ClassColorDictionary[EClasses.SNOOPER];
                break;
            case EClasses.PALADIN:
                button.colors = CColores.ClassColorDictionary[EClasses.PALADIN];
                break;
        }
    }

    private void FindSpells() //Find the spells-buttons in order to not to interfiere with other buttons created
    {
        spells = GameObject.FindGameObjectsWithTag("spells"); //Rellena el array de botones con los botones existentes
        foreach(GameObject spell in spells)
        {
            arrayButt.Add(spell.GetComponent<Button>());
        }
    }

   private void DisableAllButtons()
    {
        foreach (Button b in arrayButt)
        {
            b.gameObject.SetActive(false); //Desactiva los botones 
        }
    }

   private void EnableAllButtons()
    {
        foreach(Button b in arrayButt)
        {
            b.gameObject.SetActive(true);
        }
    }



}
