using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    [System.Serializable]
    public class Mission
    {
        public string name;
        
    }

    [System.Serializable]
    public class PostSctructShips
    {
        public string ship_name;
        public string ship_type;
        public string home_port;
        public List<Mission> missions;
        
    }




