using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiHelper
{
    public class DogApiProcessor
    {

        public static async Task<List<DogModel>> LoadBreedList()
        {
            ///TODO : À compléter LoadBreedList
            /// Attention le type de retour n'est pas nécessairement bon
            /// J'ai mis quelque chose pour avoir une base
            /// TODO : Compléter le modèle manquant
            /// 


            string url = "https://dog.ceo/api/breeds/list/all";
            //string result = " ";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                List<DogModel> dogs = new List<DogModel>();
                if (response.IsSuccessStatusCode)
                {

                   //string result2 = await response.Content.ReadAsStringAsync();

                    BreedModel result = await response.Content.ReadAsAsync<BreedModel>();
                    var breed = result.Breeds.Keys.ToList();

                    foreach (var race in breed) {
                        DogModel Race = new DogModel();
                        Race.race = race;
                        dogs.Add(Race);
                        
                    }

                   

                    return dogs;
                }
                else
                {


                    throw new Exception(response.ReasonPhrase);
                }
            }

        }

        public static async Task<DogModel> GetImageUrl(string breed)
        {
            /// TODO : GetImageUrl()
            /// TODO : Compléter le modèle manquant
            /// 
            string url;

            url = $"https://dog.ceo/api/breed/{breed}/images/random";


            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {

                    DogModel result = await response.Content.ReadAsAsync<DogModel>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
           
        }
    }
}
