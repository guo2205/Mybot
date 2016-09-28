using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System.Threading.Tasks;
using Mybot;
using System.Runtime.InteropServices;

namespace Mybot.SHDbot
{
    [LuisModel("b2317a11-6d3c-4882-84bf-2b60c7578717", "177134f2b924409697183f32d513055a")]
    [Serializable]
    public class SHD : LuisDialog<object>
    {
        public const string Entity_location = "Location";
        //[LuisIntent("")]
        //public async Task None(IDialogContext context, LuisResult result)
        //{
        //    string message = $"Sorry I did not understand: "
        //        + string.Join(", ", result.Intents.Select(i => i.Intent));
        //    await context.PostAsync(message);
        //    context.Wait(MessageReceived);
        //}

        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string tulingStr = result.Query;
            string tulingRequst = TuLingTool.postdata(tulingStr, "0", 0);
            await context.PostAsync(tulingRequst);
            context.Wait(MessageReceived);
            /*
            string message = $"Sorry I did not understand: "
                + string.Join(", ", result.Intents.Select(i => i.Intent));
            await context.PostAsync(message);
            context.Wait(MessageReceived);
            */
        }

        [System.Runtime.InteropServices.DllImport("C:\\FilterDll.dll", EntryPoint = "FilterCity")]
        private static extern IntPtr FilterCity(IntPtr a);


        [System.Runtime.InteropServices.DllImport("C:\\FilterDll.dll", EntryPoint = "ReleaseString")]
        private static extern void ReleaseString(IntPtr a);

        public string City = "";

        [LuisIntent("QueryWeather")]
        public async Task GetWeather(IDialogContext context, LuisResult result)
        {
            //var cities = (IEnumerable<City>)Enum.GetValues(typeof(City));
            //EntityRecommendation location;
            //
            //if (!result.TryFindEntity(Entity_location, out location))
            //{
            //    PromptDialog.Choice(context,
            //                        SelectCity,
            //                        cities,
            //                        "In which city do you want to know the weather forecast?");
            //}
            //else
            //{
            //    //Add code to retrieve the weather
            //    await context.PostAsync($"The weather in {location} is ");
            //    context.Wait(MessageReceived);
            //}
            string[] citys = QueryIsHasCity(result.Query);
            if (citys.Length>0)
            {
                await context.PostAsync(string.Format("{0}的天气很好", citys[0]));
            }
            else
            {
                await context.PostAsync(string.Format("你在哪个城市啊"));
            }
            context.Wait(MessageReceived);
        }

        private string[] QueryIsHasCity(string _query)
        {
            //调用DLL分配内存
            IntPtr charPtr = Marshal.StringToHGlobalAnsi(_query);
            IntPtr charOutPtr = FilterCity(charPtr);
            string cityStr = Marshal.PtrToStringAnsi(charOutPtr);
            string[] City = null;
            if (!string.IsNullOrEmpty(cityStr))
            {
                City = cityStr.Split(' ');
            }
            else
            {
                City = new string[] { };
            }
            //释放资源
            Marshal.FreeHGlobal(charPtr);
            //调用DLL释放内存
            ReleaseString(charOutPtr);
            return City;
        }
    }
}