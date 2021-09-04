using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Xamarin.Forms;
using LBot.Models;
using LBot;

namespace LBot.Worker {
    public class SocketIOWorker {

        public async void Start() {
            getEvents();
            getJnrDynamic();
            getsnrDynamic();
            await client.ConnectAsync();
        }

        public SocketIO client = new SocketIO("http://192.168.137.1:2910/", new SocketIOOptions {
            EIO = 4
        });

        public void getEvents() {
            List<Event> events = new List<Event>();
            client.On("events", response => {
                string text = response.ToString();
                text= text.Substring(1, text.Length-2);
                events = JsonSerializer.Deserialize<List<Event>>(text);
                (Application.Current as App).eventslist = events;
                MessagingCenter.Send<object>(this, "Event");
            });
        }

        public void getJnrDynamic() {
            client.On("jnrRemaining", response => {
                char[] charsToTrim = { '[', ']' };
                int remaining = int.Parse(response.ToString().Trim(charsToTrim));
                MessagingCenter.Send<object, int>(this, "jnrRemaining", remaining);
            });

            client.On("jnrAlert", resonse => {
                char[] charsToTrim = { '[', ']','"' };
                string alert = resonse.ToString().Trim(charsToTrim);
                MessagingCenter.Send<object, string>(this, "jnrAlert", alert);
            });

            client.On("jnrFullness", resonse => {
                char[] charsToTrim = { '[', ']', '"' };
                string alert = resonse.ToString().Trim(charsToTrim);
                MessagingCenter.Send<object, string>(this, "jnrFullness", alert);
            });
            
            client.On("jnrPeriods", response => {
                char[] charsToTrim = { '[', ']', '"' };
                string text = response.ToString();
                List<List<string>> periods = new List<List<string>>() {new List<string>(){ "", "" },new List<string>(){ "", "" },new List<string>(){ "", "" },new List<string>(){ "", "" },new List<string>(){ "", "" },new List<string>(){ "", "" },new List<string>(){ "", "" },new List<string>(){ "", "" } };
                
                int row = 0;
                int datapoint = 0;
                for (int i=4; i<text.Length; i++) {
                    char a = text[i];
                    if (text[i]==']'||text[i]=='\\'||text[i]=='\"') {
                        continue;
                    }
                    else if (text[i]=='[') {
                        row++;
                        datapoint =0;
                    }
                    else if (text[i]==',') {
                        datapoint++;
                    } else {
                        periods[row][datapoint]+=text[i];
                    }
                }
                MessagingCenter.Send<object, List<List<string>>>(this, "jnrPeriods", periods);
            });

            client.On("jnrTrends", response => {
                char[] charsToTrim = { '[', ']', '"' };
                string text = response.ToString().Trim(charsToTrim);
                Trends trend = JsonSerializer.Deserialize<Trends>(text);
                MessagingCenter.Send<object, Trends>(this, "jnrTrends", trend);
            });
        }

        public void getsnrDynamic() {
            client.On("snrRemaining", response => {
                char[] charsToTrim = { '[', ']' };
                int remaining = int.Parse(response.ToString().Trim(charsToTrim));
                MessagingCenter.Send<object, int>(this, "snrRemaining", remaining);
            });

            client.On("snrAlert", resonse => {
                char[] charsToTrim = { '[', ']', '"' };
                string alert = resonse.ToString().Trim(charsToTrim);
                MessagingCenter.Send<object, string>(this, "snrAlert", alert);
            });

            client.On("snrFullness", resonse => {
                char[] charsToTrim = { '[', ']', '"' };
                string alert = resonse.ToString().Trim(charsToTrim);
                MessagingCenter.Send<object, string>(this, "snrFullness", alert);
            });

            client.On("snrPeriods", response => {
                char[] charsToTrim = { '[', ']', '"' };
                string text = response.ToString();
                List<List<string>> periods = new List<List<string>>() { new List<string>() { "", "" }, new List<string>() { "", "" }, new List<string>() { "", "" }, new List<string>() { "", "" }, new List<string>() { "", "" }, new List<string>() { "", "" }, new List<string>() { "", "" }, new List<string>() { "", "" } };

                int row = 0;
                int datapoint = 0;
                for (int i = 4; i<text.Length; i++) {
                    char a = text[i];
                    if (text[i]==']'||text[i]=='\\'||text[i]=='\"') {
                        continue;
                    } else if (text[i]=='[') {
                        row++;
                        datapoint =0;
                    } else if (text[i]==',') {
                        datapoint++;
                    } else {
                        periods[row][datapoint]+=text[i];
                    }
                }
                MessagingCenter.Send<object, List<List<string>>>(this, "snrPeriods", periods);
            });

            client.On("snrTrends", response => {
                char[] charsToTrim = { '[', ']', '"' };
                string text = response.ToString().Trim(charsToTrim);
                Trends trend = JsonSerializer.Deserialize<Trends>(text);
                MessagingCenter.Send<object, Trends>(this, "snrTrends", trend);
            });
        }
    }
}
