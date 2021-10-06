using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class NetworkManager : MonoBehaviour
{
    [SerializeField] private LaunchesResponse launchesResponse;
    [SerializeField] private ShipsResponse shipsResponse;
    [SerializeField] private RocketResponse rocketResponse;
    [SerializeField] private string loadedRocketName;
    
    public Sprite[] sprites;
    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollItemPrefab;
         
    public static IEnumerator SendRequest(string url, Action<string> callback)
            {
                UnityWebRequest request = UnityWebRequest.Get(url);
                yield return request.SendWebRequest();
                if (request.isNetworkError)
                {
                    Debug.Log("Network Error: " + request.error);
                    yield break;
                }
                        
                if (request.isHttpError)
                {
                    Debug.Log("HTTP Error: " + request.error);
                    yield break;
                }
                string text = "{\"posts\":" + request.downloadHandler.text + "}";
                callback(text);
            }


    private void Awake()
    {
        StartCoroutine(SendRequest("https://api.spacexdata.com/v3/launches", launchesJson =>
        {
            launchesResponse = JsonUtility.FromJson<LaunchesResponse>(launchesJson);

            StartCoroutine(SendRequest("https://api.spacexdata.com/v3/rockets", rocketsJson =>
            {
                rocketResponse = JsonUtility.FromJson<RocketResponse>(rocketsJson);

                for (int i = 0; i < launchesResponse.posts.Count; i++)
                {
                    loadedRocketName = launchesResponse.posts[i].rocket.rocket_name;
                    var elem = rocketResponse.posts.Find(p => p.rocket_name == loadedRocketName);


                    GameObject scrollItemObj = Instantiate(scrollItemPrefab);
                    scrollItemObj.transform.SetParent(scrollContent.transform, false);

                    var texts = scrollItemObj.transform.GetComponentsInChildren<Text>();
                    texts[0].text = "<b> Missions: </b>" + launchesResponse.posts[i].mission_name;
                    texts[1].text = "<b> Rocket: </b>" + launchesResponse.posts[i].rocket.rocket_name;
                    texts[2].text = "<b> Number of payloads: </b>" + launchesResponse.posts[i].rocket.second_stage.payloads.Count;
                    texts[3].text = "<b> Launches from: </b>" + elem.country;

                    var image = scrollItemObj.transform.GetChild(4).GetComponent<Image>();
                    if (launchesResponse.posts[i].upcoming == false)
                    {
                        image.sprite = sprites[0];
                    }
                    else
                    {
                        image.sprite = sprites[1];
                    }
                }
            }));
        }));

        StartCoroutine(SendRequest("https://api.spacexdata.com/v3/ships", shipsJson =>
        {
            shipsResponse = JsonUtility.FromJson<ShipsResponse>(shipsJson);
        }));
    }

}

























