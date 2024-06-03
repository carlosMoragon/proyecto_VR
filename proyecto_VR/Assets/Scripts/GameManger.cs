using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManger : MonoBehaviour
{

    public TextMeshProUGUI timerText;
    private Partida partida;
    public TextMeshProUGUI timerTextB;
    public TextMeshProUGUI timerTextN;
    private Partida partidaB;
    private Partida partidaN;
    private bool turnoB;
    private float timeElapsed;
    public TextMeshProUGUI textoPuntuacionBlancas;
    public TextMeshProUGUI textoPuntuacionNegras;
    public TextMeshProUGUI textoGanador;
    public GameObject tableroPrefab;
    public TextMeshProUGUI puntuacionGafas;

    // Start is called before the first frame update
    void Start()
    {
        StartPartida();
    }

    // Update is called once per frame
    void Update()
    {
        if (partidaB != null && partidaN != null && partidaB.Iniciada && partidaN.Iniciada)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= 1f)
            {
                timeElapsed = 0f;
                if (turnoB)
                {
                    partidaB.DecrementarTiempo();
                }
                else
                {
                    partidaN.DecrementarTiempo();
                }
                ActualizarTextoEnPartida();

                // Verificar si alguna partida ha llegado a 00:00
                if (partidaB.Segundos <= 0 && partidaB.Minutos <= 0)
                {
                    finalizarPartidas();
                    partidaN.Vencedor = 1;
                    partidaB.Vencedor = 0;
                }
                else if (partidaN.Segundos <= 0 && partidaN.Minutos <= 0)
                {
                    finalizarPartidas();
                    partidaB.Vencedor = 1;
                    partidaN.Vencedor = 0;
                }
            }
        }
    }

    void StartPartida()
    {
        partida = new Partida(5, 0, 2);
        ActualizarTexto();
        
        turnoB = true;
    }

    public void ReiniciarPuntuaciones()
{
        partidaB.ReiniciarPuntuacion();
        partidaN.ReiniciarPuntuacion();
        ActualizarTextoPuntuacion();

    }   


    public void AumentarMinutos(int cantidad)
    {
        partida.Minutos += cantidad; 
        ActualizarTexto();
    }

    
    public void AumentarSegundos(int cantidad)
    {
        partida.Segundos += cantidad; 
        ActualizarTexto();
    }

    
    public void DisminuirMinutos(int cantidad)
    {
        partida.Minutos -= cantidad; 
        ActualizarTexto();
    }

    
    public void DisminuirSegundos(int cantidad)
    {
        partida.Segundos -= cantidad; 
        ActualizarTexto();
    }

    
    private void ActualizarTexto()
    {
        timerText.text = partida.ObtenerTiempo();
    }



    public void CambiarTurno()
    {
        turnoB = !turnoB;
    }


    private void ActualizarTextoEnPartida()
    {
        timerTextB.text = "b: " + partidaB.ObtenerTiempo();
        timerTextN.text = "n: " + partidaN.ObtenerTiempo();
    }

    public void iniciarPartidas()
    {

        partidaB = partida.Copy();
        partidaN = partida.Copy();

        ReiniciarPuntuaciones();


        partidaB.Iniciada = true;
        partidaN.Iniciada = true;
        ActualizarTextoEnPartida();
    }

    public void finalizarPartidas()
    {
        partidaB.Iniciada = false;
        partidaN.Iniciada = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        // Obtener el tipo de pieza de ajedrez del objeto que activó el trigger
        string tipoPieza = ObtenerTipoPieza(other.gameObject.name);

        // Determinar a qué partida sumar puntos según el color del objeto
        Partida partidaASumar;
        if (other.gameObject.name.Contains("Black"))
            partidaASumar = partidaB;
        else
            partidaASumar = partidaN;

        // Llamar al método de la partida para sumar puntos según el tipo de pieza
        partidaASumar.SumarPuntosSegunPieza(tipoPieza);

        if(!partidaB.Iniciada && !partidaN.Iniciada){
            if (other.gameObject.name.Contains("Black")){
                partidaB.Vencedor = 1;
                partidaN.Vencedor = 0;
            }else{
                partidaB.Vencedor = 0;
                partidaN.Vencedor = 1;
            }
        }

        ActualizarTextoPuntuacion();
    }

    private void ActualizarTextoPuntuacion()
    {
        
        textoPuntuacionBlancas.text = partidaN.ObtenerPuntos().ToString();

        
        textoPuntuacionNegras.text = partidaB.ObtenerPuntos().ToString();

        puntuacionGafas.text = "Blancas: " + partidaN.ObtenerPuntos().ToString() + "\nNegras: " + partidaB.ObtenerPuntos().ToString();
    }

    
    private string ObtenerTipoPieza(string nombreObjeto)
    {
   
        if (nombreObjeto.Contains("Pawn"))
            return "Peon";
        else if (nombreObjeto.Contains("Knight"))
            return "Caballo";
        else if (nombreObjeto.Contains("Bishop"))
            return "Alfil";
        else if (nombreObjeto.Contains("Rook"))
            return "Torre";
        else if (nombreObjeto.Contains("Queen"))
            return "Reina";
        else if (nombreObjeto.Contains("King")){
            finalizarPartidas();
            return "Rey";
        }     
        else
            return "";
    }

    public void DeterminarGanador()
    {
        if (partidaB.Vencedor == 1)
        {
            textoGanador.text = "¡BLANCAS!";
        }
        else if (partidaN.Vencedor == 1)
        {
            textoGanador.text = "¡NEGRAS!";
        }
        else
        {
            textoGanador.text = "¡EMPATE!";
        }
    }

    public void TerminarJuego()
    {

        Application.Quit();
    }



    
    public void ReemplazarTablero()
    {
    
        GameObject tableroActual = GameObject.Find("tablero(Clone)");

    
        if (tableroActual != null)
        {
    
            Vector3 posicion = tableroActual.transform.position;
            Quaternion rotacion = tableroActual.transform.rotation;

    
            Destroy(tableroActual);

    
            Instantiate(tableroPrefab, posicion, rotacion);
        }
        else
        {
            Debug.LogWarning("No se encontró el objeto 'tablero' en la escena.");
        }
    }


}


