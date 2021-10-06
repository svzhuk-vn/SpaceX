using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;





    [System.Serializable]
    public class PostStructLaunches
    {

        public string mission_name;
        public bool upcoming;
        public Rocket rocket;
        public List<string> ships;

    }

    [System.Serializable]
    public class Rocket

    {
        public string rocket_name;
        public SecondStage second_stage;

    }


    [System.Serializable]
    public class SecondStage
    {
        
        public List<Payload> payloads;

    }


    [System.Serializable]
    public class Payload
    {
        public string payload_id;
        public string nationality;
        public double payload_mass_kg;

    }


