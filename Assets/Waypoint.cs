using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    //Lista de todos os pontos
    public GameObject[] waypoints;
    int currentWP = 0;//definir o ponto que est� agora
    //configura��es da movimenta��o
    float speed = 1.0f;
    float accuracy = 1.0f;
    float rotSpeed = 0.4f;
    
    void Start()
    {
        //procura na cena todos os objetos que tem a tag "WAYPOINT", e ele coloca esses objetos dentro do array.
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }
   
    void LateUpdate()
    {
        //Se o array estiver vazio, ele ir� ignorar todo o resto do m�todo.
        if (waypoints.Length == 0) return;
        //Ele pega a posi��o do waypoint onde estou indo, ignorando o y.
        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x,
        this.transform.position.y,
        waypoints[currentWP].transform.position.z);
        //Ele pega a dire��o e depois ir� rotacionar o objeto para apontar pro pr�ximo waypoint.
        Vector3 direction = lookAtGoal - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
        Quaternion.LookRotation(direction),
        Time.deltaTime * rotSpeed);
        //Ele est� verificando se o objeto j� chegou no waypoint atual
        if (direction.magnitude < accuracy)
        {
            //O pr�ximo waypoint da lista passa a ser o atual
            currentWP++;
            //Verificando se j� chegou no �ltimo waypoint da lista. 
            if (currentWP >= waypoints.Length)
            {
                //Se sim, ele volta para o primeiro.
                currentWP = 0;
            }
        }
        //Fazendo a movimenta��o no eixo z local do objeto. 
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
    
