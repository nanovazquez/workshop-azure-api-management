using System;
using System.Collections.Generic;

namespace NETConfAPI.Models
{
    public static class Utils
    {
        public static void InitContext(NETConfContext context)
        {
            var speaker1 = new Speaker() { Id = 1, Name = "Carlos Dieguez" };
            var speaker2 = new Speaker() { Id = 2, Name = "Mariano Vazquez" };
            var speaker3 = new Speaker() { Id = 3, Name = "Sebastián Pérez" };

            var talk1 = new Talk() { Id = 1, Title = ".NET Standard: One Library to Rule Them All", Speaker = speaker1 };
            var talk2 = new Talk() { Id = 2, Title = "APIfiquemos el mundo con Azure API Management", Speaker = speaker2 };
            var talk3 = new Talk() { Id = 3, Title = "Mis primeros pasos en Xamarin", Speaker = speaker3 };

            var conference = new Conference()
            {
                Id = 1,
                Year = "2017",
                Name = "NETConfUY",
                Talks = new List<Talk>() { talk1, talk2, talk3 }
            };

            context.Conferences.Add(conference);
            context.SaveChanges();
        }
    }
}
