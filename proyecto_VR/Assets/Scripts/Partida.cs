using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partida
{
    int minutos = 0;
    int segundos = 0;
    int vencedor = 2; // Empate
    bool iniciada;
    int puntos;

    public Partida() { }

   
    public Partida(int minutos, int segundos, int vencedor)
    {
        this.Minutos = minutos;
        this.Segundos = segundos;
        this.Vencedor = vencedor;
        this.puntos = 0;
        this.iniciada = false;
    }

    public void ReiniciarPuntuacion()
    {
        this.puntos = 0;

    }

    public bool Iniciada
    {
        get { return iniciada; }
        set { iniciada = value; }
    }

   
    public int Minutos
    {
        get { return minutos; }
        set { minutos = Mathf.Max(0, value); } // Asegura que los minutos no sean negativos
    }


    public int Segundos
    {
        get { return segundos; }
        set
        {
            if (value >= 60)
            {
                minutos += value / 60;
                segundos = value % 60;
            }
            else if (value < 0)
            {
                if (minutos > 0)
                {
                    minutos--;
                    segundos = 60 + value;
                }
                else
                {
                    segundos = 0;
                }
            }
            else
            {
                segundos = value;
            }
        }
    }
    

    public int Vencedor
    {
        get { return vencedor; }
        set { vencedor = value; }
    }

        
    public string ObtenerTiempo()
    {
        return string.Format("{0:00}:{1:00}", Minutos, Segundos);
    }

    
    public void DecrementarTiempo()
    {
        Segundos -= 1;
    }

    public Partida Copy()
    {
        return new Partida(this.Minutos, this.Segundos, this.Vencedor)
        {
            Iniciada = this.Iniciada
        };
    }

    public void SumarPuntosSegunPieza(string tipoPieza)
    {
    
        switch (tipoPieza)
        {
            case "Peon":
                puntos += 1;
                break;
            case "Caballo":
            case "Alfil":
                puntos += 3;
                break;
            case "Torre":
                puntos += 5;
                break;
            case "Reina":
                puntos += 9;
                break;
            case "Rey":
                puntos += 10;
                break;
            default:
                // Si no coincide con ningún tipo de pieza conocido, no suma puntos
                break;
        }
    }

    public int ObtenerPuntos()
    {
        return puntos;
    }


}
