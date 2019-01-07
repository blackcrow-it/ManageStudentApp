using ManageStudentApp.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ManageStudentApp.Service
{
    class Handle
    {
        public async static Task Login(string username, string password)
        {
            var tokenJson = await APIHandle.Sign_In(username, password).Result.Content.ReadAsStringAsync();
            Credential tokenResponse = JsonConvert.DeserializeObject<Credential>(tokenJson);
            string jsonUser = JsonConvert.SerializeObject(tokenResponse);
            try
            {
                await WriteFile("credential.txt", jsonUser);
                Debug.WriteLine("Success: " + jsonUser);
            }
            catch
            {
                Debug.WriteLine("Fail: " + jsonUser);
            }
        }

        public async static Task<string> ReadFile(string file_name)
        {
            Windows.Storage.StorageFolder storageFolder =
                        Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile =
                await storageFolder.GetFileAsync(file_name);
            string text = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
            return text;
        }

        public async static Task WriteFile(string file_name, string text)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile =
                await storageFolder.CreateFileAsync(file_name,
                    Windows.Storage.CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(sampleFile, text);
        }

        public async static Task<string> CheckCredential()
        {
            string text;
            StorageFolder localfolder = ApplicationData.Current.LocalFolder;
            if (await localfolder.TryGetItemAsync("credential.txt") != null)
            {
                text = await ReadFile("credential.txt");
            }
            else
            {
                text = "";
            }
            return text;
        }

        public async static Task<string> GetToken()
        {
            string token;
            StorageFolder localfolder = ApplicationData.Current.LocalFolder;
            if (await localfolder.TryGetItemAsync("credential.txt") != null)
            {
                string text = await ReadFile("credential.txt");
                if(text == "")
                {
                    token = "";
                }
                else
                {
                    Credential credential = JsonConvert.DeserializeObject<Credential>(text);
                    token = credential.accessToken;
                }
            }
            else
            {
                token = "";
            }

            return token;
        }
    }
}
