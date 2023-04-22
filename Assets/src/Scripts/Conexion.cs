using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Text;
using System.IO;
using SimpleJSON;

public class Conexion : MonoBehaviour
{

    public TMP_InputField EntradaBuscador;
    public TMP_Text TextoRespuesta;

    public GameObject panelInput;
    public GameObject panelTexto;
    public static GameObject scriptMouse;
    public Rotate scriptRotar;

    public int contador = 0;

    void Start(){
        scriptRotar = scriptMouse.GetComponent<Rotate>();
        scriptRotar.enabled = true;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.T) && contador == 0){
            panelInput.SetActive(true);
            panelTexto.SetActive(false);
            scriptRotar.enabled = false;
            contador++;
        }
        if(Input.GetKeyDown(KeyCode.T) && contador == 1){
            panelInput.SetActive(false);
            panelTexto.SetActive(false);
            scriptRotar.enabled = true;
            contador--;
        }
    }



    public void hablarIA(){
        if(EntradaBuscador.text == ""){
            StartCoroutine(ObtenerDatos("Juez"));
        }else{
            StartCoroutine(ObtenerDatos(EntradaBuscador.text));
        }
    }

    [System.Serializable]
    public class DatosAPI   
    {
        public string nombre;
        public string dialogo;
    }
    [System.Serializable]
    public class PeticionAPI
    {
        public List<DatosAPI> dataAPI;
    }

    public PeticionAPI nuevaDataAPI;
    public DatosAPI data;

    private IEnumerator cambiarPanel(){
        yield return new WaitForSeconds(20);
        panelInput.SetActive(true);
        panelTexto.SetActive(false);
        scriptRotar.enabled = false;
    }

    private IEnumerator ObtenerDatos(string inputTexto){
        string url = "http://148.220.60.24:3000/hackathon/api/caso";

        UnityWebRequest Peticion = UnityWebRequest.Post(url, "POST");

        JSONObject json = new JSONObject();
        json["texto"] = inputTexto;

        byte[] bodyRaw = Encoding.UTF8.GetBytes(json.ToString());
        UploadHandlerRaw uploadHandler = new UploadHandlerRaw(bodyRaw);
        uploadHandler.contentType =  "application/json";
        Peticion.uploadHandler = uploadHandler;
        yield return Peticion.SendWebRequest();

        if(!Peticion.isNetworkError && !Peticion.isHttpError){
            nuevaDataAPI = JsonUtility.FromJson<PeticionAPI>("{\"dataAPI\":" + Peticion.downloadHandler.text + "}");
            Debug.Log(Peticion.downloadHandler.text);
            Debug.Log(nuevaDataAPI.dataAPI[0].dialogo);

            string txt = "";
            for(int i = 0; i < nuevaDataAPI.dataAPI.Count; i++){
                txt = txt + nuevaDataAPI.dataAPI[i].nombre + ": " + nuevaDataAPI.dataAPI[i].dialogo + "\n"; 
            }
            TextoRespuesta.text = txt;
            
            panelInput.SetActive(false);
            panelTexto.SetActive(true);
            scriptRotar.enabled = false;
            StartCoroutine(cambiarPanel());
        }else{
            Debug.Log(Peticion.error);
        }
    }

}
